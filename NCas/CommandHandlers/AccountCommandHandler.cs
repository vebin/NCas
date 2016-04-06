using ECommon.Components;
using ENode.Commanding;
using ENode.Infrastructure;
using NCas.Commands.Accounts;
using NCas.Domain.Accounts;
using NCas.Domain.Services;

namespace NCas.CommandHandlers
{
    [Component]
    public class AccountCommandHandler
        : ICommandHandler<RegisterAccount>                                                        //注册账号
        , ICommandHandler<ChangeAccount>                                                          //删除账号
    {
        private readonly ILockService _lockService;
        private readonly AccountService _accountService;
        public AccountCommandHandler()
        {
        }

        public AccountCommandHandler(ILockService lockService,AccountService accountService)
        {
            _lockService = lockService;
            _accountService = accountService;
        }

        /// <summary>注册账号
        /// </summary>
        public void Handle(ICommandContext context, RegisterAccount command)
        {
            _lockService.ExecuteInLock(typeof (Account).Name, () =>
            {
                _accountService.RegisterAccountIndex(command.AggregateRootId, command.Code);
                var info = new AccountInfo(command.Code, command.AccountName, command.Password);
                var account=new Account(command.AggregateRootId,info);
                context.Add(account);
            });
        }

        /// <summary>删除账号
        /// </summary>
        public void Handle(ICommandContext context, ChangeAccount command)
        {
            _lockService.ExecuteInLock(typeof (Account).Name, () =>
            {
                _accountService.DeleteAccountIndex(command.AggregateRootId);
                context.Get<Account>(command.AggregateRootId).Change(command.UseFlag);
            });
        }
    }
}
