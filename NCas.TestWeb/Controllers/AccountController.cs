using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NCas.Client.Utils;

namespace NCas.TestWeb.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var accountId = CookieUtils.Get("Account");
            if (string.IsNullOrEmpty(accountId))
            {
                if (HttpContext.Request.Url != null)
                {
                    var url = NCasServerSetting.GetServerAuthUrlWithCallBack(HttpContext.Request.Url.ToString());
                    return Redirect(url);
                }
            }
            ViewData["AccountId"] = accountId;
            return View();
        }
    }
}