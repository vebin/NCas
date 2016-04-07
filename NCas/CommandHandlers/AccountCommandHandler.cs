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
        , ICommandHandler<UpdateAccountName>                                                      //修改账号名
        , ICommandHandler<UpdateAccountPassword>                                                  //修改账号密码
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
                var info = new AccountInfo(command.Code, command.AccountName, command.Password);
                var account=new Account(command.AggregateRootId,info);
                _accountService.RegisterAccountIndex(command.AggregateRootId, command.Code, command.AccountName);
                context.Add(account);
            });
        }

        /// <summary>修改账号名
        /// </summary>
        public void Handle(ICommandContext context, UpdateAccountName command)
        {
            context.Get<Account>(command.AggregateRootId).UpdateAccountName(command.AccountName);
        }

        /// <summary>修改账号密码
        /// </summary>
        public void Handle(ICommandContext context, UpdateAccountPassword command)
        {
            context.Get<Account>(command.AggregateRootId).UpdatePassword(command.Password);
        }

        /// <summary>删除账号
        /// </summary>
        public void Handle(ICommandContext context, ChangeAccount command)
        {
            _lockService.ExecuteInLock(typeof (Account).Name, () =>
            {
                context.Get<Account>(command.AggregateRootId).Change(command.UseFlag);
                _accountService.DeleteAccountIndex(command.AggregateRootId);
            });
        }
    }
}
