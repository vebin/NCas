using ENode.Commanding;

namespace NCas.Commands.Accounts
{
    /// <summary>修改账号密码
    /// </summary>
    public class UpdateAccountPassword : Command<string>
    {
        public string Password { get; set; }

        public UpdateAccountPassword()
        {
            
        }

        public UpdateAccountPassword(string id,  string password) : base(id)
        {
            Password = password;
        }
    }
}
