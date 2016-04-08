using System;
using System.Collections.Generic;
using System.Web;
using GUtils.Utilities;

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
            get { return PathUtils.CombineUrl(ServerUrl, "/Verify"); }
        }

        /// <summary>验证票据地址
        /// </summary>
        public static string VerifyTicketBaseUrl
        {
            get { return PathUtils.CombineUrl(ServerUrl, "/VerifyTicket"); }
        }

        /// <summary>获取服务器端带返回地址的Url
        /// </summary>
        public static string GetServerAuthUrl(string callBackUrl)
        {
            return PathUtils.GetUrlWithIncreaseParam(ServerAuthUrl, "CallBackUrl", HttpUtility.UrlEncode(callBackUrl));
        }


        /// <summary>生成Cas验证的真实地址
        /// </summary>
        public static string GetVerifyTickUrl(string ticket, string callBackUrl)
        {
            var dict = new Dictionary<string, string>()
            {
                {"Ticket", ticket},
                {"CallBackUrl", HttpUtility.UrlEncode(callBackUrl)}
            };
            return PathUtils.GetUrlWithIncreaseParams(VerifyTicketBaseUrl, dict);
        }

       


    }
}
