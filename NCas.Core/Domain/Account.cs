using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCas.Core.Domain
{
    /// <summary>账号聚合根
    /// </summary>
    public class Account
    {
        /// <summary>账号Id
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>该Code需要在客户端的服务器中存储
        /// </summary>
        public string Code { get; set; }

        /// <summary>登录名
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>密码
        /// </summary>
        public string Password { get; set; }

        public Account()
        {

        }

        public Account(string accountId, string code, string accountName, string password)
        {
            AccountId = accountId;
            Code = code;
            AccountName = accountName;
            Password = password;
        }





    }
}
