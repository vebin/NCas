using System.Threading.Tasks;
using System.Web.Mvc;
using ENode.Commanding;
using NCas.Commands.Accounts;
using NCas.Common.Enums;
using NCas.QueryServices;
using NCas.Web.Extensions;
using NCas.Web.ViewModels;

namespace NCas.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountQueryService _accountQueryService;

        public AccountController(ICommandService commandService, IAccountQueryService accountQueryService)
            : base(commandService)
        {
            _accountQueryService = accountQueryService;
        }

        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            ViewData.Model = _accountQueryService.FindAll();
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateAccountDto dto)
        {
            var command = dto.ToRegisterAccount();
            var result = await ExecuteCommandAsync(command);
            if (!result.IsSuccess())
            {
                ModelState.AddModelError(string.Empty,result.GetErrorMessage());
                return View();
            }
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Delete(string accountId)
        {
            var command = new ChangeAccount(accountId, (int) UseFlag.Disabled);
            var result = await ExecuteCommandAsync(command);
            if (!result.IsSuccess())
            {
                ModelState.AddModelError(string.Empty, result.GetErrorMessage());
                return View("Index");
            }
            return RedirectToAction("Index");
        }

    }
}