using System.Web;
using NCas.Core.Tickets;

namespace NCas.Core.Utils
{
    /// <summary>Url地址工具
    /// </summary>
    public class UrlUtils
    {

        /// <summary>获取服务端登陆的地址
        /// </summary>
        public static string GetLoginUrl(string webAppKey, string callBackUrl)
        {
            // ReSharper disable once UseStringInterpolation
            return string.Format("/Login?WebAppKey={0}&CallBackUrl={1}", webAppKey, callBackUrl);
        }

        public static string GetErrorUrl()
        {
            return string.Format("/Error");
        }


        /// <summary>获取客户端验证票据地址
        /// </summary>
        public static string GetClientVerifyTicketUrl(WebAppInfo webApp, Ticket ticket, string callBackUrl)
        {
            // ReSharper disable once UseStringInterpolation
            return string.Format(@"{0}?Ticket={1}&CallBackUrl={2}", webApp.VerifyTicketUrl, ticket.GetEncryptTicket(),
                HttpUtility.UrlEncode(callBackUrl));
        }

        /// <summary>获取客户端设置Account的地址
        /// </summary>
        public static string GetClientPutUrl(WebAppInfo webApp, string account, string callBackUrl)
        {
            // ReSharper disable once UseStringInterpolation
            return string.Format(@"{0}?Account={1}&CallBackUrl={2}", webApp.VerifyTicketUrl, account,
                HttpUtility.UrlEncode(callBackUrl));
        }

    }
}
