﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCas.Domain.WebApps
{
    /// <summary>网站应用信息
    /// </summary>
    public class WebAppInfo
    {
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
        /// <summary>通知某账号登出地址
        /// </summary>
        public string NotifyUrl { get; set; }

        public WebAppInfo(string appKey, string appName, string url, string verifyTicketUrl, string notifyUrl)
        {
            AppKey = appKey;
            AppName = appName;
            Url = url;
            VerifyTicketUrl = verifyTicketUrl;
            NotifyUrl = notifyUrl;
        }
    }
}
