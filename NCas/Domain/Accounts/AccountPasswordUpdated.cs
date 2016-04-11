using System;
using ENode.Eventing;

namespace NCas.Domain.Accounts
{
    /// <summary>修改账号密码
    /// </summary>
    [Serializable]
    public class AccountPasswordUpdated : DomainEvent<string>
    {
        public string Password { get; private set; }

        public AccountPasswordUpdated()
        {

        }

        public AccountPasswordUpdated(string password)
        {
            Password = password;
        }
    }
}
