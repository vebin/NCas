using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCas.Core.TicketGrantingCookies
{
    /// <summary>TGC配置
    /// </summary>
    public class TicketGrantingCookieSetting
    {
        /// <summary>TGC票据过期时间
        /// </summary>
        public int TgcExpiredSeconds { get; set; }

        public TicketGrantingCookieSetting(int tgcExpiredSeconds)
        {
            TgcExpiredSeconds = tgcExpiredSeconds;
        }


    }
}
