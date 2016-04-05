namespace NCas.Core.WebApps
{
    /// <summary>WebApp缓存设置
    /// </summary>
    public class WebAppCacheSetting
    {
        /// <summary>WebApp缓存过期时间
        /// </summary>
        public int WebAppInactiveSeconds { get; set; }

        /// <summary>扫描WebApp过期时间的间隔
        /// </summary>
        public int ScanExpiredWebAppIntervalSeconds { get; set; }

        public WebAppCacheSetting(int webAppInactiveSeconds, int scanExpiredWebAppIntervalSeconds)
        {
            WebAppInactiveSeconds = webAppInactiveSeconds;
            ScanExpiredWebAppIntervalSeconds = scanExpiredWebAppIntervalSeconds;
        }
    }
}
