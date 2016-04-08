using System.Security.Policy;
using NCas.Commands.WebApps;
using NCas.QueryServices.Dtos;

namespace NCas.Web.ViewModels
{
    public class EditWebAppDto
    {

        public string WebAppId { get; set; }

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

        public EditWebAppDto()
        {

        }

        public EditWebAppDto(string webAppId, string appName, string url, string verifyTicketUrl, string putAccountUrl,
            string notifyUrl)
        {
            WebAppId = webAppId;
            AppName = appName;
            Url = url;
            VerifyTicketUrl = verifyTicketUrl;
            PutAccountUrl = putAccountUrl;
            NotifyUrl = notifyUrl;
        }

        public UpdateWebApp ToCommand()
        {
            return new UpdateWebApp(WebAppId, AppName, Url, VerifyTicketUrl, PutAccountUrl, NotifyUrl);
        }

    }



}