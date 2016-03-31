using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GUtils.Utilities;

namespace NCas.Core.Utils
{
    public class ClientUrlUtils
    {
        public static List<string> ClientUrList=new List<string>();
        /// <summary>客户端用来验证Ticket的地址,NCas会提交Ticket到该地址
        /// </summary>
        public static string VerifyTicketUrl;

        static ClientUrlUtils()
        {
            VerifyTicketUrl = AppSettingsUtils.GetString("VerifyTicketUrl", @"/NCas/VerifyTicket");
        }

        public static string GetLinkUrl()
        {
            //获取跳转的真实链接
            return "";
        }



    }
}
