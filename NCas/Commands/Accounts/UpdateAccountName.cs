using ENode.Commanding;

namespace NCas.Commands.Accounts
{
    /// <summary>修改账号密码
    /// </summary>
    public class UpdateAccountName:Command<string>
    {
        public string AccountName { get; set; }

        public UpdateAccountName()
        {
            
        }

        public UpdateAccountName(string id, string accountName) : base(id)
        {
            AccountName = accountName;
        }
    }
}
