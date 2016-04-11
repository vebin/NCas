using System;
using ENode.Eventing;

namespace NCas.Domain.Accounts
{
    /// <summary>账号注册事件
    /// </summary>
    public class AccountRegistered : DomainEvent<string>
    {
        public AccountInfo Info { get; private set; }

        public AccountRegistered()
        {
            
        }

        public AccountRegistered(AccountInfo info)
        {
            Info = info;
        }
    }
}
