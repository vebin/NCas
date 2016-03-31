using System;
using ENode.Eventing;

namespace NCas.Domain.Accounts
{
    /// <summary>删除账号事件
    /// </summary>
    [Serializable]
    public class AccountChanged : DomainEvent<string>
    {
        public int UseFlag { get; private set; }

        public AccountChanged()
        {
            
        }

        public AccountChanged(Account account, int useFlag) : base()
        {
            UseFlag = useFlag;
        }
    }
}
