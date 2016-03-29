using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCas.Core.Tickets
{
    /// <summary>票据配置
    /// </summary>
    public class TicketSetting
    {
        /// <summary>票据过期时间
        /// </summary>
        public int TicketInactiveSeconds { get; set; }
        /// <summary>扫描票据过期时间的间隔
        /// </summary>
        public int ScanExpiredTicketIntervalSeconds { get; set; }

        public TicketSetting(int ticketInactiveSeconds = 60*60*100, int scanExpiredTicketIntervalSeconds = 5)
        {
            TicketInactiveSeconds = ticketInactiveSeconds;
            ScanExpiredTicketIntervalSeconds = scanExpiredTicketIntervalSeconds;
        }
    }
}
