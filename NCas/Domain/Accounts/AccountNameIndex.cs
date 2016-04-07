namespace NCas.Domain.Accounts
{
    /// <summary>账号名索引
    /// </summary>
    public class AccountNameIndex
    {
         public string AccountId { get; set; }

        public string AccountName { get; set; }

        public AccountNameIndex(string accountId, string accountName)
        {
            AccountId = accountId;
            AccountName = accountName;
        }
    }
}
