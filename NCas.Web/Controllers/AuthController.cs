using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ECommon.Extensions;
using ECommon.IO;
using ENode.Commanding;
using GUtils.Utilities;
using NCas.ApplicationServices;
using NCas.Core;
using NCas.Core.TicketGrantings;
using NCas.Core.Tickets;
using NCas.Core.Utils;
using NCas.Core.WebApps;
using NCas.Web.Services;
using NCas.Web.ViewModels;

namespace NCas.Web.Controllers
{
    public class AuthController : Controller
    {

        private readonly ICommandService _commandService;
        private readonly ITicketGrantingManager _ticketGrantingManager;
        private readonly ITicketManager _ticketManager;
        private readonly IAuthService _authService;
        private readonly IWebAppManager _webAppManager;
        private readonly IAccountService _accountService;
        private readonly WebAppFactory _webAppFactory;

        public AuthController(ICommandService commandService, ITicketGrantingManager ticketGrantingManager,
            ITicketManager ticketManager,
            IAuthService authService, IWebAppManager webAppManager, IAccountService accountService,
            WebAppFactory webAppFactory)
        {
            _commandService = commandService;
            _ticketGrantingManager = ticketGrantingManager;
            _ticketManager = ticketManager;
            _authService = authService;
            _webAppManager = webAppManager;
            _accountService = accountService;
            _webAppFactory = webAppFactory;
        }

        /// <summary>验证页面,查看TGC是否存在,如果TGC不存在,那么就直接跳转到登陆页面
        /// </summary>
        [HttpGet]
        public ActionResult Verify()
        {
            //回调地址
            var callBackUrl = RequestUtils.GetString("CallBackUrl");
            //根据CallBack地址 先拿到请求是属于哪一个客户端
            var webApp = _webAppFactory.GetWebAppByUrl(PathUtils.GetDomainUrl(RequestUtils.GetString("CallBackUrl")));
            if (webApp == null)
            {
                //如果该系统不存在,那么直接提示错误,或者其他信息
                return Redirect(UrlUtils.GetErrorUrl());
            }

            //生成WebAppId,并缓存
            var webAppKey = _webAppManager.GetCacheKey(webApp.WebAppId);
            //获取TGC,如果TGC不存在,直接跳转到登陆页面
            var account = _ticketGrantingManager.GetTicketGranting();
            return _authService.Verify(account, webAppKey, callBackUrl);
        }

        /// <summary>Cas服务器验证Ticket
        /// </summary>
        [HttpGet]
        public ActionResult VerifyTicket()
        {
            var ticket = RequestUtils.GetString("Ticket");
            var callBackUrl = RequestUtils.GetString("CallBackUrl");
            var webAppKey = RequestUtils.GetString("WebAppKey");
            //验证Ticket
            var verifyResult = _ticketManager.VerifyTicket(ticket);
            if (verifyResult)
            {
                //如果验证成功,那么将Account信息返回给客户端
                var account = _ticketGrantingManager.GetTicketGranting();
                var accountBack = _ticketGrantingManager.BackAccount(account);
                var webAppId= _webAppManager.GetWebAppId(webAppKey);
                var webApp = _webAppFactory.GetWebAppById(webAppId);
               // return Redirect(UrlUtils.GetClientPutUrl(new WebAppInfo(), accountBack, callBackUrl));
            }
            return View("Error");
        }


        #region 账号登陆

        /// <summary>账号登陆页面
        /// </summary>
        [HttpGet]
        public ActionResult Login()
        {
            var webAppKey = RequestUtils.GetString("WebAppKey");
            ViewData["WebAppKey"] = webAppKey;
            ViewData["CallBackUrl"] = HttpUtility.UrlEncode(RequestUtils.GetString("CallBackUrl"));
            //账号或者密码错误
            return View();
        }

        /// <summary>账号登陆,提交
        /// </summary>
        [HttpPost]
        public ActionResult Login(AccountLoginDto dto)
        {

            var webAppKey = RequestUtils.GetString("WebAppKey");
            var callBackUrl = RequestUtils.GetString("CallBackUrl");
            var accountVerifyInfoDto = _accountService.AccountMatch(dto.AccountName, dto.Password);
            //验证账号密码,不为null代表成功
            if (accountVerifyInfoDto != null)
            {
                var webAppId = _webAppManager.GetWebAppId(webAppKey);
                var accountInfo = new AccountInfo(webAppId, accountVerifyInfoDto.AccountId, accountVerifyInfoDto.Code,
                    accountVerifyInfoDto.AccountName);
                //写入TGC,因为已经登陆成功,所以此时需要写入,至于Ticket的验证,那是后续的事情
                _ticketGrantingManager.SetTicketGranting(accountInfo);
                //验证,进行不同页面的跳转
                return _authService.Verify(accountInfo, webAppKey, callBackUrl);
            }
            //账号或者密码错误
            return View();
        }





        #endregion

        private Task<AsyncTaskResult<CommandResult>> ExecuteCommandAsync(ICommand command, int millisecondsDelay = 5000)
        {
            return _commandService.ExecuteAsync(command, CommandReturnType.EventHandled).TimeoutAfter(millisecondsDelay);
        }
    }
}