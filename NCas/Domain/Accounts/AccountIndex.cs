namespace NCas.Domain.Accounts
{
    /// <summary>账号索引
    /// </summary>
    public class AccountIndex
    {
        public string AccountId { get; set; }

        public string Code { get; set; }

        public AccountIndex(string accountId, string code)
        {
            AccountId = accountId;
            Code = code;
        }
    }
}
