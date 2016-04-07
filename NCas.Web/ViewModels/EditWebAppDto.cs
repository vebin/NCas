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

        /// <summary>通知某账号登出地址
        /// </summary>
        public string NotifyUrl { get; set; }

        public EditWebAppDto()
        {
            
        }
        public EditWebAppDto(string webAppId, string appName, string url, string verifyTicketUrl, string notifyUrl)
        {
            WebAppId = webAppId;
            AppName = appName;
            Url = url;
            VerifyTicketUrl = verifyTicketUrl;
            NotifyUrl = notifyUrl;
        }

        public static EditWebAppDto GetFromWebAppInfoDto(WebAppInfoDto dto)
        {
            return new EditWebAppDto(dto.WebAppId, dto.AppName, dto.Url, dto.VerifyTicketUrl, dto.NotifyUrl);
        }
    }

    public static class EditWebAppDtoExtension
    {
        public static UpdateWebApp ToUpdateWebApp(this EditWebAppDto dto)
        {
            return new UpdateWebApp(dto.WebAppId, dto.AppName, dto.Url, dto.VerifyTicketUrl, dto.NotifyUrl);
        }
    }


}