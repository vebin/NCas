using System;
using ENode.Eventing;

namespace NCas.Domain.Accounts
{
    /// <summary>修改账号名
    /// </summary>
    [Serializable]
    public class AccountNameUpdated : DomainEvent<string>
    {
        public string AccountName { get; private set; }

        public AccountNameUpdated()
        {
            
        }

        public AccountNameUpdated(Account account, string accountName):base()
        {
            AccountName = accountName;
        }
    }
}
