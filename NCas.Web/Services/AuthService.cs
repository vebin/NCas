using System.Web;
using System.Web.Mvc;
using ECommon.Components;
using NCas.ApplicationServices;
using NCas.Core.TicketGrantings;
using NCas.Core.Tickets;
using NCas.Core.Utils;
using NCas.Core.WebApps;
using NCas.Web.ViewModels;

namespace NCas.Web.Services
{
    /// <summary>验证服务
    /// </summary>
    [Component]
    public class AuthService : IAuthService
    {
        private readonly ITicketManager _ticketManager;
        private readonly ITicketGrantingManager _ticketGrantingManager;
        private readonly IWebAppManager _webAppManager;
        public AuthService()
        {
        }

        public AuthService(ITicketManager ticketManager, ITicketGrantingManager ticketGrantingManager,
            IWebAppManager webAppManager)
        {
            _ticketManager = ticketManager;
            _ticketGrantingManager = ticketGrantingManager;
            _webAppManager = webAppManager;
        }

        /// <summary>验证,是否已经登陆,没有登陆就重新跳转
        /// </summary>
        public ActionResult Verify(AccountInfo account, string webAppKey, string callBackUrl)
        {

            //账号为null,重定向到登陆页面
            if (account == null)
            {
                return new RedirectResult(UrlUtils.GetLoginUrl(webAppKey, callBackUrl));
            }

            //判断是否已经
            var webAppDto = _webAppManager.GetWebAppInfoByUrl();

            //产生票据,跳转验证地址
            var ticket = _ticketManager.CreateTicket(account.AccountId, account.Code);
            //跳转到客户端验证页面
            var url = UrlUtils.GetClientVerifyTicketUrl(webAppDto.ToWebAppInfo(), ticket,
                callBackUrl);

            return new RedirectResult(url);
        }
    }
}