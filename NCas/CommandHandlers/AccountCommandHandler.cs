using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommon.Components;
using ENode.Commanding;
using ENode.Infrastructure;
using NCas.Commands.Accounts;
using NCas.Domain.Services;

namespace NCas.CommandHandlers
{
    [Component]
    public class AccountCommandHandler
        : ICommandHandler<RegisterAccount>                                                        //注册账号
    {
        private readonly ILockService _lockService;
        private readonly AccountService _accountService;
        public AccountCommandHandler(ILockService lockService,AccountService accountService)
        {
            _lockService = lockService;
            _accountService = accountService;
        }

        /// <summary>注册账号
        /// </summary>
        public void Handle(ICommandContext context, RegisterAccount command)
        {
            throw new NotImplementedException();
        }
    }
}
