using System;
using ENode.Eventing;

namespace NCas.Domain.Accounts
{
    /// <summary>账号注册事件
    /// </summary>
    [Serializable]
    public class AccountRegistered : DomainEvent<string>
    {
        public AccountInfo Info { get; private set; }

        public AccountRegistered()
        {
            
        }

        public AccountRegistered(Account account, AccountInfo info) : base()
        {
            Info = info;
        }
    }
}
