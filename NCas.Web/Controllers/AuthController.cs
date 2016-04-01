using System.Threading.Tasks;
using System.Web.Mvc;
using ECommon.Extensions;
using ECommon.IO;
using ENode.Commanding;
using GUtils.Utilities;
using NCas.ApplicationServices;
using NCas.Core.TicketGranting;
using NCas.Core.Tickets;
using GUtils.Mvc;
namespace NCas.Web.Controllers
{
    public class AuthController : Controller
    {

        private readonly ICommandService _commandService;
        private readonly ITicketManager _ticketManager;
        private readonly ITicketGrantingManager _ticketGrantingManager;

        public AuthController(ICommandService commandService,ITicketManager ticketManager, ITicketGrantingManager ticketGrantingManager)
        {
            _commandService = commandService;
            _ticketManager = ticketManager;
            _ticketGrantingManager = ticketGrantingManager;
        }


        public ActionResult Verify()
        {
            //先拿到请求是属于哪一个客户端
            var webApp = WebAppFactory.GetWebAppByUrl(RequestUtils.GetReferer().Authority);
            if (webApp == null)
            {
                return Json("");
            }
            //获取TGC
            var account = _ticketGrantingManager.GetTicketGranting();
            if (account == null)
            {
                //跳转到登陆页面
            //  return RedirectToAction()
            }
            var ticket = _ticketManager.CreateTicket();
            var r = ticket.TicketValue;



            return new JsonResult();
        }

        private Task<AsyncTaskResult<CommandResult>> ExecuteCommandAsync(ICommand command, int millisecondsDelay = 5000)
        {
            return _commandService.ExecuteAsync(command, CommandReturnType.EventHandled).TimeoutAfter(millisecondsDelay);
        }
    }
}