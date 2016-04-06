using System;
using NCas.Commands.WebApps;
using NCas.Utils;

namespace NCas.Web.ViewModels
{
    public class CreateWebAppDto
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

        /// <summary>通知某账号登出地址
        /// </summary>
        public string NotifyUrl { get; set; }

        public CreateWebAppDto()
        {
            
        }
        public CreateWebAppDto(string appName, string url, string verifyTicketUrl, string notifyUrl)
        {
            AppName = appName;
            Url = url;
            VerifyTicketUrl = verifyTicketUrl;
            NotifyUrl = notifyUrl;
        }
    }

    public static class CreateWebAppDtoExtension
    {
        public static CreateWebApp ToCreateWebApp(this CreateWebAppDto dto)
        {
            var id = Guid.NewGuid().ToString("N");
            var command = new CreateWebApp(id, WebAppUtils.CreateWebAppKey(), dto.AppName, dto.Url, dto.VerifyTicketUrl,
                dto.NotifyUrl);
            return command;
        }
    }
}