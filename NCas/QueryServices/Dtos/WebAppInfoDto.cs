﻿namespace NCas.QueryServices.Dtos
{
    /// <summary>网站节点信息
    /// </summary>
    public class WebAppInfoDto
    {
        /// <summary>网站节点Id
        /// </summary>
        public string WebAppId { get; set; }
        /// <summary>应用Key
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>应用名称
        /// </summary>
        public string AppName { get; set; }
        /// <summary>Url地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>验证Ticket地址
        /// </summary>
        public string VerifyTicketUrl { get; set; }
        /// <summary>设置账号Url
        /// </summary>
        public string PutAccountUrl { get; set; }

        /// <summary>通知某账号登出地址
        /// </summary>
        public string NotifyUrl { get; set; }

        public WebAppInfoDto()
        {
        }

        public WebAppInfoDto(string webAppId, string appKey, string appName, string url, string verifyTicketUrl,
            string putAccountUrl, string notifyUrl)
        {
            WebAppId = webAppId;
            AppKey = appKey;
            AppName = appName;
            Url = url;
            VerifyTicketUrl = verifyTicketUrl;
            PutAccountUrl = putAccountUrl;
            NotifyUrl = notifyUrl;
        }
    }
}
