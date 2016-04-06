using System.Threading.Tasks;
using System.Web.Mvc;
using ECommon.Extensions;
using ECommon.IO;
using ENode.Commanding;
using NCas.QueryServices;
using NCas.Web.Extensions;
using NCas.Web.ViewModels;

namespace NCas.Web.Controllers
{
    public class WebAppController : Controller
    {
        private readonly ICommandService _commandService;
        private readonly IWebAppQueryService _webAppQueryService;

        public WebAppController(ICommandService commandService, IWebAppQueryService webAppQueryService)
        {
            _commandService = commandService;
            _webAppQueryService = webAppQueryService;
        }

        //显示
        [HttpGet]
        public ActionResult Index()
        {
            ViewData.Model = _webAppQueryService.FindAll();
            return View();
        }

        //创建WebApp
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Create(CreateWebAppDto dto)
        {
            var command = dto.ToCreateWebApp();
            var result = await ExecuteCommandAsync(command);
            if (!result.IsSuccess())
            {
                ModelState.AddModelError("Error", result.GetErrorMessage());
                return View();
            }
            return RedirectToAction("Index");
        }
        



        private Task<AsyncTaskResult<CommandResult>> ExecuteCommandAsync(ICommand command, int millisecondsDelay = 5000)
        {
            return _commandService.ExecuteAsync(command, CommandReturnType.EventHandled).TimeoutAfter(millisecondsDelay);
        }
    }
}