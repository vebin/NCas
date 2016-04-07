namespace NCas.Domain.Accounts
{
    /// <summary>账号索引
    /// </summary>
    public class AccountCodeIndex
    {
        public string AccountId { get; set; }

        public string Code { get; set; }

        public AccountCodeIndex(string accountId, string code)
        {
            AccountId = accountId;
            Code = code;
        }
    }
}
