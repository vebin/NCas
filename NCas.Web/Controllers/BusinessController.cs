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
    public class BusinessController : BaseController
    {
        private readonly IAccountQueryService _accountQueryService;
        private readonly IWebAppQueryService _webAppQueryService;

        public BusinessController(ICommandService commandService, IAccountQueryService accountQueryService,
            IWebAppQueryService webAppQueryService)
            : base(commandService)
        {
            _accountQueryService = accountQueryService;
            _webAppQueryService = webAppQueryService;
        }

        /// <summary>注册账号
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> RegisterAccount(CreateAccountDto dto)
        {
             
            if (!ModelState.IsValid)
            {
                return Json(new {Success = false, Message = ModelState.GetFirstError()});
            }
            var command = dto.ToRegisterAccount();
            var result = await ExecuteCommandAsync(command);
            if (!result.IsSuccess())
            {
                return Json(new {Success = false, Message = result.GetErrorMessage()});
            }
            return Json(new {Success = true, Message = "注册成功"});
        }

        /// <summary>删除已经注册的某个账号
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> DeleteAccount(string code)
        {
            var account = _accountQueryService.FindByCode(code);
            if (account == null)
            {
                return Json(new { Success = false, Message = "账号不存在" });
            }
            var command = new ChangeAccount(account.AccountId, (int) UseFlag.Disabled);
            var result = await ExecuteCommandAsync(command);
            if (!result.IsSuccess())
            {
                return Json(new {Success = false, Message = result.GetErrorMessage()});
            }
            return Json(new {Success = true, Message = "删除成功"});
        }

        /// <summary>修改账号密码
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> UpdateAccountPassword(UpdateAccountPasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new {Success = false, Message = ModelState.GetFirstError()});
            }
            var account = _accountQueryService.FindByCode(dto.Code);
            if (account == null)
            {
                return Json(new { Success = false, Message = "账号不存在" });
            }
            var command = new UpdateAccountPassword(account.AccountId, dto.Password);
            var result = await ExecuteCommandAsync(command);
            if (!result.IsSuccess())
            {
                return Json(new {Success = false, Message = result.GetErrorMessage()});
            }
            return Json(new {Success = true, Message = "修改成功"});
        }

        /// <summary>创建WebApp
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> CreateWebApp(CreateWebAppDto dto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new {Success = false, Message = ModelState.GetFirstError()});
            }
            var command = dto.ToCommand();
            var result = await ExecuteCommandAsync(command);
            if (!result.IsSuccess())
            {
                return Json(new {Success = false, Message = result.GetErrorMessage()});
            }
            return Json(new {Success = true, Message = "系统注册成功"});
        }

        /// <summary>修改WebApp
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> UpdateWebApp(UpdateWebAppDto dto)
        {
            var webAppInfoDto = _webAppQueryService.FindByKey(dto.WebAppKey);
            if (webAppInfoDto == null)
            {
                return Json(new {Success = false, Message = "该系统不存在"});
            }
            if (!ModelState.IsValid)
            {
                return Json(new {Success = false, Message = ModelState.GetFirstError()});
            }
            dto.SetWebAppId(webAppInfoDto.WebAppId);
            var command = dto.ToCommand();
            var result = await ExecuteCommandAsync(command);
            if (!result.IsSuccess())
            {
                return Json(new {Success = false, Message = result.GetErrorMessage()});
            }

            return Json(new {Success = true, Message = "修改成功"});
        }


        /// <summary>删除WebApp
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> DeleteWebApp(string webAppKey)
        {
            var webAppInfoDto = _webAppQueryService.FindByKey(webAppKey);
            if (webAppInfoDto == null)
            {
                return Json(new {Success = false, Message = "该系统不存在"});
            }
            var command = new ChangeAccount(webAppInfoDto.WebAppId, (int) UseFlag.Disabled);
            var result = await ExecuteCommandAsync(command);
            if (!result.IsSuccess())
            {
                return Json(new {Success = false, Message = result.GetErrorMessage()});
            }
            return Json(new {Success = true, Message = "删除成功"});
        }

    }
}