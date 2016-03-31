namespace NCas.Domain.Accounts
{
    public class AccountInfo
    {
        /// <summary>编码,唯一
        /// </summary>
        public string Code { get; set; }
        /// <summary>账号名
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>密码
        /// </summary>
        public string Password { get; set; }


        public AccountInfo(string code, string accountName, string password)
        {
            Code = code;
            AccountName = accountName;
            Password = password;
        }
    }
}
