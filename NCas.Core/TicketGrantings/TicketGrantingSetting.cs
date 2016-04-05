namespace NCas.Core.TicketGrantings
{
    /// <summary>TGC配置
    /// </summary>
    public class TicketGrantingSetting
    {
        /// <summary>TGC票据过期时间
        /// </summary>
        public int TgcExpiredSeconds { get; set; }

        public TicketGrantingSetting(int tgcExpiredSeconds)
        {
            TgcExpiredSeconds = tgcExpiredSeconds;
        }


    }
}
