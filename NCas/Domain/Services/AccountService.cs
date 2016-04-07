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
        private readonly IAccountCodeIndexRepository  _accountCodeIndexRepository;
        private readonly IAccountNameIndexRepository _accountNameIndexRepository;

        public AccountService(IAccountCodeIndexRepository accountCodeIndexRepository,
            IAccountNameIndexRepository accountNameIndexRepository)
        {
            _accountCodeIndexRepository = accountCodeIndexRepository;
            _accountNameIndexRepository = accountNameIndexRepository;
        }

        /// <summary>注册账号索引
        /// </summary>
        public void RegisterAccountIndex(string accountId, string code, string accountName)
        {
            var codeIndex = _accountCodeIndexRepository.FindAccountCodeIndex(code);
            var nameIndex = _accountNameIndexRepository.FindAccountNameIndex(accountName);
            if (codeIndex != null)
            {
                throw new RepeatException("该账号Id索引已经存在");
            }
            if (nameIndex != null)
            {
                throw new RepeatException("该账号名称索引已经存在");
            }
            _accountCodeIndexRepository.AddCodeIndex(new AccountCodeIndex(accountId, code));
            _accountNameIndexRepository.AddNameIndex(new AccountNameIndex(accountId, accountName));
        }

        /// <summary>删除账号索引
        /// </summary>
        public void DeleteAccountIndex(string accountId)
        {
            _accountCodeIndexRepository.DeleteCodeIndex(accountId);
            _accountNameIndexRepository.DeleteNameIndex(accountId);
        }

    }
}
