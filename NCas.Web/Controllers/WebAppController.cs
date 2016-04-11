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
    public class WebAppController : BaseController
    {
        private readonly IWebAppQueryService _webAppQueryService;

        public WebAppController(ICommandService commandService, IWebAppQueryService webAppQueryService)
            : base(commandService)
        {
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

        [HttpPost]
        public async Task<ActionResult> Create(CreateWebAppDto dto)
        {
            var command = dto.ToCommand();
            var result = await ExecuteCommandAsync(command);
            if (!result.IsSuccess())
            {
                ModelState.AddModelError(string.Empty, result.GetErrorMessage());
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(string webAppId)
        {
            var webAppInfoDto = _webAppQueryService.FindById(webAppId);
            ViewData.Model = webAppInfoDto.ToEditWebAppDto();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditWebAppDto dto)
        {
            var command = dto.ToCommand();
            var result = await ExecuteCommandAsync(command);
            if (!result.IsSuccess())
            {
                ModelState.AddModelError(string.Empty, result.GetErrorMessage());
                var model = _webAppQueryService.FindById(dto.WebAppId).ToEditWebAppDto();
                return View(model);
            }
            return RedirectToAction("Index");
        }


    }
}