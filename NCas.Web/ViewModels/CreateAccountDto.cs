using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ECommon.Utilities;
using NCas.Commands.Accounts;

namespace NCas.Web.ViewModels
{
    /// <summary>创建账号Dto
    /// </summary>
    public class CreateAccountDto
    {
        [Required(AllowEmptyStrings = false,ErrorMessage = "Code不能为空")]
        [StringLength(50,ErrorMessage = "Code在50字符以内")]
        public string Code { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "账号名不能为空")]
        [StringLength(50,ErrorMessage = "账号名在50字符以内")]
        public string AccountName { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "账号密码不能为空")]
        public string Password { get; set; }

        public CreateAccountDto()
        {
        }

        public CreateAccountDto(string code, string accountName, string password)
        {
            Code = code;
            AccountName = accountName;
            Password = password;
        }
    }

    public static class CreateAccountDtoExtension
    {
        public static RegisterAccount ToRegisterAccount(this CreateAccountDto dto)
        {
            return new RegisterAccount(ObjectId.GenerateNewStringId(), dto.Code, dto.AccountName, dto.Password);
        }
    }
}