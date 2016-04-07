using System.Web.Mvc;
using GUtils.Utilities;
using NCas.Client.Utils;

namespace NCas.TestWeb.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>展示页面,如果用户不存在,则跳转到认证页面
        /// </summary>
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