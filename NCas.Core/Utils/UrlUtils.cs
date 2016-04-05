using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCas.Core.Tickets;

namespace NCas.Core.Utils
{
    /// <summary>Url地址工具
    /// </summary>
    public class UrlUtils
    {

        /// <summary>获取服务端登陆的地址
        /// </summary>
        public static string GetLoginUrl(string callBackUrl)
        {
            // ReSharper disable once UseStringInterpolation
            return string.Format("/AccountLogin?CallBackUrl={0}", callBackUrl);
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
            return string.Format(@"{0}?Ticket={1}&CallBackUrl={2}", webApp.VerifyTicketUrl, ticket.TicketValue,
                callBackUrl);
        }

    }
}
