using System.Web.Mvc;
using NCas.ApplicationServices;
using NCas.Core.TicketGrantings;
using NCas.Core.Tickets;
using NCas.Core.Utils;
using NCas.Web.Models;

namespace NCas.Web.Services
{
    /// <summary>验证服务
    /// </summary>
    public class AuthService
    {
        private readonly ITicketManager _ticketManager;
        private readonly ITicketGrantingManager _ticketGrantingManager;

        public AuthService(ITicketManager ticketManager, ITicketGrantingManager ticketGrantingManager)
        {
            _ticketManager = ticketManager;
            _ticketGrantingManager = ticketGrantingManager;
        }

        /// <summary>验证,是否已经登陆,没有登陆就重新跳转
        /// </summary>
        public ActionResult Verify(AccountInfo account, string callBackUrl)
        {
            //账号为null,重定向到登陆页面
            if (account == null)
            {
                return new RedirectResult(UrlUtils.GetLoginUrl(callBackUrl));
            }
            //查询出WebApp
            var webAppDto = WebAppFactory.GetWebAppById(account.WebAppId);

            //产生票据,跳转验证地址
            var ticket = _ticketManager.CreateTicket(account.AccountId, account.Code);
            //跳转到客户端验证页面
            return
                new RedirectResult(UrlUtils.GetClientVerifyTicketUrl(WebAppMapper.ToWebApp(webAppDto), ticket,
                    callBackUrl));

        }




    }
}