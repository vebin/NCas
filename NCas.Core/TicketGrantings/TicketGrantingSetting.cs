using ECommon.Components;

namespace NCas.Core.TicketGrantings
{
    /// <summary>TGC配置
    /// </summary>
    [Component]
    public class TicketGrantingSetting
    {
        /// <summary>TGC票据过期时间
        /// </summary>
        public int TgcExpiredSeconds { get; set; }

        public TicketGrantingSetting()
        {
        }

        public TicketGrantingSetting(int tgcExpiredSeconds)
        {
            TgcExpiredSeconds = tgcExpiredSeconds;
        }


    }
}
