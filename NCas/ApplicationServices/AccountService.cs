using GUtils.Encrypt;
using NCas.QueryServices;
using NCas.QueryServices.Dtos;

namespace NCas.ApplicationServices
{
    public class AccountService
    {
        private readonly IAccountQueryService _accountQueryService;

        public AccountService(IAccountQueryService accountQueryService)
        {
            _accountQueryService = accountQueryService;
        }

        /// <summary>账号与密码是否匹配
        /// </summary>
        public bool AccountMatch(string accountName, string password)
        {
            var verifyAccount = _accountQueryService.FindVerifyDtoByName(accountName);
            if (verifyAccount != null)
            {
                string encryptPassword = EncryptHelper.GetDoubleMd5(password);
                if (verifyAccount.Password == encryptPassword)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
