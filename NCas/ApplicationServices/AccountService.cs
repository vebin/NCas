using ECommon.Components;
using GUtils.Encrypt;
using NCas.QueryServices;
using NCas.QueryServices.Dtos;

namespace NCas.ApplicationServices
{
    [Component]
    public class AccountService:IAccountService
    {
        private readonly IAccountQueryService _accountQueryService;

        public AccountService()
        {
            
        }

        public AccountService(IAccountQueryService accountQueryService)
        {
            _accountQueryService = accountQueryService;
        }

        /// <summary>账号与密码是否匹配
        /// </summary>
        public AccountInfoVerifyDto AccountMatch(string accountName, string password)
        {
            var verifyAccount = _accountQueryService.FindVerifyDtoByName(accountName);
            if (verifyAccount != null)
            {
                var encryptPassword = EncryptHelper.GetDoubleMd5(password);
                if (verifyAccount.Password == encryptPassword)
                {
                    return verifyAccount;
                }
            }
            return null;
        }
    }
}
