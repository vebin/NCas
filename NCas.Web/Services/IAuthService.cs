using System.Web.Mvc;
using NCas.Core.TicketGrantings;

namespace NCas.Web.Services
{
    /// <summary>验证服务接口
    /// </summary>
    public interface IAuthService
    {
        /// <summary>验证,是否已经登陆,没有登陆就重新跳转
        /// </summary>
        ActionResult Verify(AccountInfo account, string webAppKey, string callBackUrl);


    }
}
