using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NCas.Web.Controllers
{
    public class HomeController : Controller
    {
       
       

        /// <summary>登陆页面
        /// </summary>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>验证单点登录
        /// </summary>
        //[HttpGet]
        //public async Task<ActionResult> Index()
        //{
        //    var account=
        //}

    }
}