using ENode.Commanding;

namespace NCas.Commands.WebApps
{
    public class UpdateWebApp : Command<string>
    {
        /// <summary>应用名称
        /// </summary>
        public string AppName { get; set; }

        /// <summary>Url地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>验证Ticket地址
        /// </summary>
        public string VerifyTicketUrl { get; set; }
        /// <summary>设置账号信息Url
        ///  </summary>
        public string PutAccountUrl { get; set; }

        /// <summary>通知某账号登出地址
        /// </summary>
        public string NotifyUrl { get; set; }

        public UpdateWebApp()
        {

        }

        public UpdateWebApp(string id, string appName, string url, string verifyTicketUrl,
            string putAccountUrl, string notifyUrl) : base(id)
        {
            AppName = appName;
            Url = url;
            VerifyTicketUrl = verifyTicketUrl;
            PutAccountUrl = putAccountUrl;
            NotifyUrl = notifyUrl;
        }
    }
}
