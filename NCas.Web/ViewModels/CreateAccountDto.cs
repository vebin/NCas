using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NCas.Commands.Accounts;

namespace NCas.Web.ViewModels
{
    /// <summary>创建账号Dto
    /// </summary>
    public class CreateAccountDto
    {
        public string Code { get; set; }
        public string AccountName { get; set; }
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
            var id = Guid.NewGuid().ToString("N");
            return new RegisterAccount(id, dto.Code, dto.AccountName, dto.Password);
        }
    }
}