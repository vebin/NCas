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
        public static string GetLoginUrl(string cacheKey, string callBackUrl)
        {
            // ReSharper disable once UseStringInterpolation
            return string.Format("/Login?CacheKey={0}&CallBackUrl={1}", cacheKey, HttpUtility.UrlEncode(callBackUrl));
        }

        public static string GetErrorUrl()
        {
            return string.Format("/Error");
        }

        /// <summary>服务端验证的页面
        /// </summary>
        public static string GetVerifyUrl(string callBackUrl)
        {
            var url = string.Format(@"/Verify?CallBack={0}",callBackUrl);
            return url;
        }


        /// <summary>获取客户端验证票据地址
        /// </summary>
        public static string GetClientVerifyTicketUrl(WebAppInfo webApp, Ticket ticket, string callBackUrl)
        {
            // ReSharper disable once UseStringInterpolation
            var url = string.Format(@"{0}?Ticket={1}&CallBackUrl={2}", webApp.VerifyTicketUrl,
                HttpUtility.UrlEncode(ticket.GetEncryptTicket()),
                HttpUtility.UrlEncode(callBackUrl));
            return url;
        }

        /// <summary>获取客户端设置Account的地址
        /// </summary>
        public static string GetClientPutUrl(WebAppInfo webApp, string account, string key, string callBackUrl)
        {
            // ReSharper disable once UseStringInterpolation
            var url = string.Format(@"{0}?Account={1}&key={2}&CallBackUrl={3}", webApp.PutAccountUrl,
                HttpUtility.UrlEncode(account), key,
                HttpUtility.UrlEncode(callBackUrl));
            return url;
        }

        /// <summary>获取客户端通知的Url
        /// </summary>
        public static string GetClientNotifyUrl(WebAppInfo webApp)
        {
            var url = string.Format(@"{0}", webApp.NotifyUrl);
            return url;
        }

        /// <summary>返回
        /// </summary>
        public static string GetCallBackUrl(string baseUrl, string callBackUrl)
        {
            var url = string.Format(@"{0}/{1}", baseUrl, callBackUrl);
            return url;
        }

    }
}
