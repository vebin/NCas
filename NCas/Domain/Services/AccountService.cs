using ECommon.Components;
using NCas.Common.Exceptions;
using NCas.Domain.Accounts;
using NCas.Domain.Repositories;

namespace NCas.Domain.Services
{
    /// <summary>账号服务
    /// </summary>
    [Component]
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        /// <summary>注册账号索引
        /// </summary>
        public void RegisterAccountIndex(string accountId, string code)
        {
            var index = _accountRepository.FindAccountIndex(code);
            if (index == null)
            {
                _accountRepository.AddIndex(new AccountIndex(accountId, code));
            }
            else
            {
                throw new RepeatException("该账号索引已经存在");
            }
        }

        /// <summary>删除账号索引
        /// </summary>
        public void DeleteAccountIndex(string accountId)
        {
            _accountRepository.DeleteIndex(accountId);
        }

    }
}
