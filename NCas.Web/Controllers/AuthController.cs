using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ECommon.Extensions;
using ECommon.IO;
using ENode.Commanding;
using GUtils.Utilities;
using NCas.ApplicationServices;
using NCas.Core.TicketGrantings;
using NCas.Core.Utils;
using NCas.Core.WebApps;
using NCas.Web.Services;

namespace NCas.Web.Controllers
{
    public class AuthController : Controller
    {

        private readonly ICommandService _commandService;
        private readonly ITicketGrantingManager _ticketGrantingManager;
        private readonly IAuthService _authService;
        private readonly IWebAppManager _webAppManager;
        private readonly IAccountService _accountService;
        private readonly WebAppFactory _webAppFactory;

        public AuthController(ICommandService commandService, ITicketGrantingManager ticketGrantingManager,
            IAuthService authService, IWebAppManager webAppManager, IAccountService accountService,WebAppFactory webAppFactory)
        {
            _commandService = commandService;
            _ticketGrantingManager = ticketGrantingManager;
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
            //根据CallBack地址
            //先拿到请求是属于哪一个客户端
            var a = PathUtils.GetDomainUrl(RequestUtils.GetString("CallBackUrl"));
           
            var webApp = _webAppFactory.GetWebAppByUrl(a);
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

        #region 账号登陆

        /// <summary>账号登陆页面
        /// </summary>
        [HttpGet]
        public ActionResult AccountLogin()
        {
            var accountName = RequestUtils.GetString("AccountName");
            var password = RequestUtils.GetString("Password");
            var webAppKey = RequestUtils.GetString("WebAppKey");
            var callBackUrl = RequestUtils.GetString("CallBackUrl");
            var accountVerifyInfoDto = _accountService.AccountMatch(accountName, password);
            //验证账号密码,不为null代表成功
            if (accountVerifyInfoDto != null)
            {
                var webAppId = _webAppManager.GetWebAppId(webAppKey);
                var accountInfo = new AccountInfo(webAppId, accountVerifyInfoDto.AccountId, accountVerifyInfoDto.Code,
                    accountVerifyInfoDto.AccountName);
                //生成TGC,已经登陆的标志
                _ticketGrantingManager.SetTicketGranting(accountInfo);
                //验证,进行不同页面的跳转
                _authService.Verify(accountInfo, webAppKey, callBackUrl);
            }
            //账号或者密码错误
            return Redirect("");
        }

        /// <summary>账号登陆,提交
        /// </summary>
        [HttpPost]
        public ActionResult AccountLoginPost()
        {
            var accountName = RequestUtils.GetString("AccountName");
            var password = RequestUtils.GetString("Password");
            var webAppKey = RequestUtils.GetString("WebAppKey");
            var callBackUrl = RequestUtils.GetString("CallBackUrl");
            var accountVerifyInfoDto = _accountService.AccountMatch(accountName, password);
            //验证账号密码,不为null代表成功
            if (accountVerifyInfoDto != null)
            {
                var webAppId = _webAppManager.GetWebAppId(webAppKey);
                var accountInfo = new AccountInfo(webAppId, accountVerifyInfoDto.AccountId, accountVerifyInfoDto.Code,
                    accountVerifyInfoDto.AccountName);
                //生成TGC,已经登陆的标志
                _ticketGrantingManager.SetTicketGranting(accountInfo);
                //验证,进行不同页面的跳转
                _authService.Verify(accountInfo,webAppKey, callBackUrl);
            }
            //账号或者密码错误
            return Redirect("");
        }

        #endregion

        private Task<AsyncTaskResult<CommandResult>> ExecuteCommandAsync(ICommand command, int millisecondsDelay = 5000)
        {
            return _commandService.ExecuteAsync(command, CommandReturnType.EventHandled).TimeoutAfter(millisecondsDelay);
        }
    }
}