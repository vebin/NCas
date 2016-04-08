using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GUtils.Utilities;
using NCas.Client.Utils;

namespace NCas.TestWeb.Controllers
{
    public class AuthController : Controller
    {

        /// <summary>验证Ticket,将拿到的加密的Ticket进行解密,然后重定向提交到Cas服务器,进行验证
        /// </summary>
        [HttpGet]
        public ActionResult VerifyTicket()
        {
            var callBackUrl = RequestUtils.GetString("CallBackUrl");
            var ticket = EncryptUtils.DecryptTicket(RequestUtils.GetString("Ticket"));
            var url = NCasServerSetting.GetVerifyTickUrl(ticket, callBackUrl);
            return Redirect(url);
        }

        /// <summary>Cas服务器将TGC的Ticket
        /// </summary>
        [HttpGet]
        public ActionResult PutAccount()
        {
            var encryptAccount = RequestUtils.GetString("Account");
            var callBackUrl = RequestUtils.GetString("CallBackUrl");
            var key = RequestUtils.GetString("key");
            var account = EncryptUtils.DecryptAccount(encryptAccount, key);
            CookieUtils.WriteCookie("Account", account.Code, null);
            return Redirect(callBackUrl);
        }

    }
}