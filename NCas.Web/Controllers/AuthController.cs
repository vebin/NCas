using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ECommon.Extensions;
using ECommon.IO;
using ENode.Commanding;
using GUtils.Utilities;
using NCas.ApplicationServices;
using NCas.Core.TicketGrantings;
using NCas.Core.Tickets;
using NCas.Core.Utils;
using NCas.Web.Services;

namespace NCas.Web.Controllers
{
    public class AuthController : Controller
    {

        private readonly ICommandService _commandService;
        private readonly ITicketGrantingManager _ticketGrantingManager;
        private readonly IAuthService _authService;
        private readonly IAccountService _accountService;

        public AuthController(ICommandService commandService, ITicketGrantingManager ticketGrantingManager,
            IAuthService authService, IAccountService accountService)
        {
            _commandService = commandService;
            _ticketGrantingManager = ticketGrantingManager;
            _authService = authService;
            _accountService = accountService;
        }

        /// <summary>验证页面,查看TGC是否存在,如果TGC不存在,那么就直接跳转到登陆页面
        /// </summary>
        [HttpGet]
        public ActionResult Verify()
        {
            //先拿到请求是属于哪一个客户端
            var webApp = WebAppFactory.GetWebAppByUrl(RequestUtils.GetReferer().Authority);
            if (webApp == null)
            {
                //如果该系统不存在,那么直接提示错误,或者其他信息
                return Redirect(UrlUtils.GetErrorUrl());
            }
            //回调地址
            var callBackUrl = RequestUtils.GetString("CallBackUrl");
            //获取TGC,如果TGC不存在,直接跳转到登陆页面
            var account = _ticketGrantingManager.GetTicketGranting();
            return _authService.Verify(account, callBackUrl);
        }

        #region 账号登陆

        /// <summary>账号登陆页面
        /// </summary>
        [HttpGet]
        public ActionResult AccountLogin()
        {
            var accountName = RequestUtils.GetString("accountName");
            var password = RequestUtils.GetString("password");
            if (_accountService.AccountMatch(accountName, password))
            {
                //验证成功
            }
            return View();
        }

        /// <summary>账号登陆,提交
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AccountLoginPost()
        {
            //先查询出账号密码,进行匹配
            

            //如果匹配成功,则跳转回客户端的验证地址(会附带CallBackUrl),并且携带Ticket
            return Redirect("");
        }

        #endregion

        private Task<AsyncTaskResult<CommandResult>> ExecuteCommandAsync(ICommand command, int millisecondsDelay = 5000)
        {
            return _commandService.ExecuteAsync(command, CommandReturnType.EventHandled).TimeoutAfter(millisecondsDelay);
        }
    }
}