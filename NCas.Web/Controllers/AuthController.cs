using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ECommon.Extensions;
using ECommon.IO;
using ECommon.Logging;
using ENode.Commanding;
using GUtils.Utilities;
using NCas.ApplicationServices;
using NCas.Core.TicketGrantings;
using NCas.Core.Tickets;
using NCas.Core.Utils;
using NCas.Core.WebApps;
using NCas.Web.ViewModels;

namespace NCas.Web.Controllers
{
    public class AuthController : Controller
    {

        private readonly ICommandService _commandService;
        private readonly ITicketGrantingManager _ticketGrantingManager;
        private readonly ITicketManager _ticketManager;
        private readonly IWebAppManager _webAppManager;
        private readonly IAccountService _accountService;
        private readonly ILogger _logger;
        public AuthController(ICommandService commandService, ITicketGrantingManager ticketGrantingManager,
            ITicketManager ticketManager,
            IWebAppManager webAppManager, IAccountService accountService, ILoggerFactory loggerFactory)
        {
            _commandService = commandService;
            _ticketGrantingManager = ticketGrantingManager;
            _ticketManager = ticketManager;
            _webAppManager = webAppManager;
            _accountService = accountService;
            _logger = loggerFactory.Create(GetType().FullName);
        }

        /// <summary>验证页面,查看TGC是否存在,如果TGC不存在,那么就直接跳转到登陆页面,如果存在,则生成Ticket,跳转到客户端页面
        /// </summary>
        [HttpGet]
        public ActionResult Verify()
        {
            //回调地址
            var callBackUrl = RequestUtils.GetString("CallBackUrl");
            //根据CallBack地址 先拿到请求是属于哪一个客户端
            var webAppInfo = _webAppManager.GetWebAppInfoByUrl(callBackUrl);

            if (webAppInfo == null)
            {
                _logger.InfoFormat("WebAppInfo:WebAppInfo is null. CallBackUrl is {0}", callBackUrl);
                //如果该系统不存在,那么直接提示错误,或者其他信息
                return Redirect(UrlUtils.GetErrorUrl());
            }
            //生成WebAppId,并缓存
            var cacheKey = _webAppManager.GetCacheKey(webAppInfo.WebAppId);
            //获取TGC,如果TGC不存在,直接跳转到登陆页面
            var account = _ticketGrantingManager.GetTicketGranting();

            //如果账号为空,直接跳转到登陆页面
            if (account == null)
            {
                return Redirect(UrlUtils.GetLoginUrl(cacheKey, callBackUrl));
            }
            //如果账号不为空,代表登陆过,那么就生成Ticket
            var ticket = _ticketManager.CreateTicket(account.AccountId, account.Code);
            var url = UrlUtils.GetClientVerifyTicketUrl(webAppInfo, ticket,
                callBackUrl);
            return Redirect(url);

        }

        /// <summary>Cas服务器验证Ticket
        /// </summary>
        [HttpGet]
        public ActionResult VerifyTicket()
        {
            var ticket = RequestUtils.GetString("Ticket");
            var callBackUrl = RequestUtils.GetString("CallBackUrl");
            //验证Ticket
            var verifyResult = _ticketManager.VerifyTicket(ticket);
            if (verifyResult)
            {
                //如果验证成功,那么将Account信息返回给客户端
                var account = _ticketGrantingManager.GetTicketGranting();
                var key = "";
                var accountBack = _ticketGrantingManager.BackAccount(account, out key);
                //根据CallBack地址 先拿到请求是属于哪一个客户端
                var webAppInfo = _webAppManager.GetWebAppInfoByUrl(callBackUrl);
                return Redirect(UrlUtils.GetClientPutUrl(webAppInfo, accountBack, key, callBackUrl));
            }
            return Redirect(UrlUtils.GetVerifyUrl(callBackUrl));
        }


        #region 账号登陆

        /// <summary>账号登陆页面
        /// </summary>
        [HttpGet]
        public ActionResult Login()
        {
            ViewData["CacheKey"] = RequestUtils.GetString("CacheKey");
            ViewData["CallBackUrl"] = RequestUtils.GetString("CallBackUrl");
            //账号或者密码错误
            return View();
        }

        /// <summary>账号登陆,提交
        /// </summary>
        [HttpPost]
        public ActionResult Login(AccountLoginDto dto)
        {

            var accountVerifyInfoDto = _accountService.AccountMatch(dto.AccountName, dto.Password);
            if (accountVerifyInfoDto == null)
            {
                ModelState.AddModelError(string.Empty, "账号或者密码错误");
                //账号或者密码错误
                return View();
            }
            //var webAppKey = RequestUtils.GetString("CacheKey");
            //var callBackUrl = RequestUtils.GetString("CallBackUrl");

            var webAppInfo = _webAppManager.GetWebAppInfoByCacheKey(dto.CacheKey, dto.CallBackUrl);
            var accountInfo = new AccountInfo(accountVerifyInfoDto.AccountId, accountVerifyInfoDto.Code,
                accountVerifyInfoDto.AccountName);
            //写入TGC,因为已经登陆成功,所以此时需要写入,至于Ticket的验证,那是后续的事情
            _ticketGrantingManager.SetTicketGranting(accountInfo);
            //验证,跳转
            var ticket = _ticketManager.CreateTicket(accountInfo.AccountId, accountInfo.Code);
            var url = UrlUtils.GetClientVerifyTicketUrl(webAppInfo, ticket,
                dto.CallBackUrl);
            return Redirect(url);
        }

        /// <summary>退出登录
        /// </summary>
        [HttpGet]
        public ActionResult LoginOut()
        {
            var callBackUrl = RequestUtils.GetString("CallBackUrl");
            //移除TGC
            _ticketGrantingManager.RemoveTicketGranting();
            return Redirect(callBackUrl);
        }

        #endregion

        private Task<AsyncTaskResult<CommandResult>> ExecuteCommandAsync(ICommand command, int millisecondsDelay = 5000)
        {
            return _commandService.ExecuteAsync(command, CommandReturnType.EventHandled).TimeoutAfter(millisecondsDelay);
        }
    }
}