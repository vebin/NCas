using System;
using System.Collections.Concurrent;
using System.Linq;
using ECommon.Logging;
using ECommon.Scheduling;
using GUtils.Algorithm;
namespace NCas.Core.Tickets
{
    /// <summary>默认票据管理
    /// </summary>
    public class DefaultTicketManager : ITicketManager
    {
        //保存所有的票据
        private readonly ConcurrentDictionary<string, Ticket> _tickets = new ConcurrentDictionary<string, Ticket>();
        private readonly object _lockObject=new object();
        private readonly IScheduleService _scheduleService;
        private readonly int _timeoutSecond;
        private readonly ILogger _logger;

        public DefaultTicketManager(TicketSetting setting, IScheduleService scheduleService,
            ILoggerFactory loggerFactory)
        {
            _timeoutSecond = setting.TicketInactiveSeconds;
            _logger = loggerFactory.Create(GetType().FullName);
            _scheduleService = scheduleService;
            _scheduleService.StartTask("RemoveExpiredAggregates", RemoveExpiredTickets, 1000,
                setting.ScanExpiredTicketIntervalSeconds);
        }


        /// <summary>生成票据,并保存到集合
        /// </summary>
        public Ticket CreateTicket()
        {
            var ticketId = GuidUtils.NewStringGuidN();
            var ticketValue = GuidUtils.NewStringGuidN();
            var ticket = new Ticket(ticketId, ticketValue);
            lock (_lockObject)
            {
                _tickets.TryAdd(ticketId, ticket);
            }
            return ticket;
        }

        /// <summary>验证票据是否存在,如果该票据存在,则验证之后就销毁
        /// </summary>
        public bool VerifyTicket(string ticketValue)
        {
            lock (_lockObject)
            {
                var ticket = _tickets.Values.FirstOrDefault(x => x.TicketValue == ticketValue);
                if (ticket != null)
                {
                    Ticket remove;
                    _tickets.TryRemove(ticket.TicketId, out remove);
                    if (remove != null)
                    {
                        //需要写入TGC到cookie
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>移除过期的Ticket
        /// </summary>
        public void RemoveExpiredTickets()
        {
            var expiredTickets = _tickets.Values.Where(x => x.IsExpired(_timeoutSecond));
            foreach (var ticket in expiredTickets)
            {
                lock (_lockObject)
                {
                    Ticket remove;
                    if (_tickets.TryRemove(ticket.TicketId, out remove))
                    {
                        if (_logger.IsDebugEnabled)
                        {
                            _logger.DebugFormat(
                                "Removed expired ticket from cache,ticketId:{0},ticketValue:{1},removeTime:{2}",
                                ticket.TicketId, ticket.TicketValue, DateTime.Now);
                        }
                    }
                }
            }
        }


    }
}
