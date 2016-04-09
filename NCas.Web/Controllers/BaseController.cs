using System.Threading.Tasks;
using System.Web.Mvc;
using ECommon.Extensions;
using ECommon.IO;
using ENode.Commanding;

namespace NCas.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ICommandService _commandService;

        public BaseController(ICommandService commandService)
        {
            _commandService = commandService;
        }

        protected Task<AsyncTaskResult<CommandResult>> ExecuteCommandAsync(ICommand command, int millisecondsDelay = 5000)
        {
            return _commandService.ExecuteAsync(command, CommandReturnType.EventHandled).TimeoutAfter(millisecondsDelay);
        }
    }
}