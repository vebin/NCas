using NCas.Core;
using NCas.QueryServices.Dtos;

namespace NCas.Web.ViewModels
{
    public static class WebAppInfoDtoExtension
    {
        public static EditWebAppDto ToEditWebAppDto(this WebAppInfoDto dto)
        {
            return new EditWebAppDto(dto.WebAppId, dto.AppName, dto.Url, dto.VerifyTicketUrl, dto.PutAccountUrl,
                dto.NotifyUrl);
        }

        public static WebAppInfo ToWebAppInfo(this WebAppInfoDto dto)
        {
            return new WebAppInfo(dto.WebAppId, dto.AppKey, dto.AppName, dto.Url, dto.VerifyTicketUrl, dto.PutAccountUrl,
                dto.NotifyUrl);
        }
    }
}