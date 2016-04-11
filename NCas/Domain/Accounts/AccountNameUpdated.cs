using System;
using ENode.Eventing;

namespace NCas.Domain.Accounts
{
    /// <summary>修改账号名
    /// </summary>
    public class AccountNameUpdated : DomainEvent<string>
    {
        public string AccountName { get; private set; }

        public AccountNameUpdated()
        {
            
        }

        public AccountNameUpdated(string accountName)
        {
            AccountName = accountName;
        }
    }
}
