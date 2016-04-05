using System;
using System.Collections.Generic;
using System.Linq;
using ECommon.Logging;
using ECommon.Scheduling;

namespace NCas.Core.Tickets
{
    /// <summary>默认票据管理
    /// </summary>
    public class DefaultTicketManager : ITicketManager
    {
        //保存所有的票据
        private readonly Dictionary<string, Ticket> _tickets = new Dictionary<string, Ticket>();
        private readonly object _lockObject=new object();
        private readonly int _timeoutSecond;
        private readonly ILogger _logger;

        public DefaultTicketManager(TicketSetting setting, IScheduleService scheduleService,
            ILoggerFactory loggerFactory)
        {
            _timeoutSecond = setting.TicketInactiveSeconds;
            _logger = loggerFactory.Create(GetType().FullName);
            scheduleService.StartTask("RemoveExpiredTickets", RemoveExpiredTickets, 1000,
                setting.ScanExpiredTicketIntervalSeconds);
        }


        /// <summary>生成票据,并保存到集合
        /// </summary>
        public Ticket CreateTicket(string accountId, string code)
        {
            var ticketId = Guid.NewGuid().ToString("N");
            var ticketValue = Guid.NewGuid().ToString("N");
            var ticket = new Ticket(ticketId, ticketValue, new TicketAccount(accountId, code));
            lock (_lockObject)
            {
                _tickets.Add(ticketId, ticket);
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
                    _tickets.Remove(ticket.TicketId);
                    //需要写入TGC到cookie
                }
                return false;
            }
        }

        /// <summary>移除过期的Ticket,并且做了日志记录
        /// </summary>
        public void RemoveExpiredTickets()
        {
            var expiredTickets = _tickets.Values.Where(x => x.IsExpired(_timeoutSecond));
            foreach (var ticket in expiredTickets)
            {
                lock (_lockObject)
                {
                    if (_tickets.Remove(ticket.TicketId))
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
