using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace NCas.Client.Utils
{
    public class NCasServerSetting
    {
        
        static NCasServerSetting()
        {
            ServerUri = new Uri(AppSettingsUtils.GetString("ServerUrl"));
        }

        private static Uri ServerUri { get; set; }

        /// <summary>服务器端基础地址
        /// </summary>
        public static string ServerUrl
        {
            get { return ServerUri.ToString(); }
        }

        /// <summary>服务器端认证地址
        /// </summary>
        private static string ServerAuthUrl
        {
            get { return PathUtils.CombineUrl(ServerUrl, "/Auth/Verify"); }
        }

        /// <summary>验证票据地址
        /// </summary>
        public static string VerifyTicketUrl
        {
            get { return PathUtils.CombineUrl(ServerUrl, "/Auth/VerityTicket"); }
        }

        /// <summary>获取服务器端带返回地址的Url
        /// </summary>
        public static string GetServerAuthUrlWithCallBack(string callBackUrl)
        {
            return PathUtils.GetUrlWithIncreaseParam(ServerAuthUrl, "CallBackUrl", HttpUtility.UrlEncode(callBackUrl));
        }



    }
}
