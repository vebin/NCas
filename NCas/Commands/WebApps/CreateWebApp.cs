using ENode.Commanding;

namespace NCas.Commands.WebApps
{
    /// <summary>创建WebApp命令
    /// </summary>
    public class CreateWebApp : Command<string>
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
        /// <summary>设置账号Url
        /// </summary>
        public string PutAccountUrl { get; set; }

        /// <summary>通知某账号登出地址
        /// </summary>
        public string NotifyUrl { get; set; }

        public CreateWebApp()
        {
            
        }

        public CreateWebApp(string id, string appKey, string appName, string url, string putAccountUrl,
            string verifyTicketUrl,
            string notifyUrl) : base(id)
        {
            AppKey = appKey;
            AppName = appName;
            Url = url;
            PutAccountUrl = putAccountUrl;
            VerifyTicketUrl = verifyTicketUrl;
            NotifyUrl = notifyUrl;
        }
    }
}
