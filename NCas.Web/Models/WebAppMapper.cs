using NCas.Core;
using NCas.QueryServices.Dtos;

namespace NCas.Web.Models
{
    public class WebAppMapper
    {
        /// <summary>Webapp映射
        /// </summary>
        public static WebAppInfo ToWebApp(WebAppInfoDto dto)
        {
            return new WebAppInfo(dto.WebAppId, dto.AppKey, dto.AppName, dto.Url, dto.VerifyTicketUrl, dto.NotifyUrl);
        }
    }
}