using ENode.Commanding;
using GUtils.Encrypt;
using NCas.Utils;

namespace NCas.Commands.Accounts
{
    /// <summary>注册账号
    /// </summary>
    public class RegisterAccount : Command<string>
    {
        public string Code { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }

        public RegisterAccount()
        {
            
        }

        public RegisterAccount(string id, string code, string accountName, string password) : base(id)
        {
            Code = code;
            AccountName = accountName;
            Password = EncryptUtils.EncryptAccountPassword(password);
        }
    }
}
