namespace NCas.Core.Tickets
{
    /// <summary>票据账号关联
    /// </summary>
    public class TicketAccount
    {
        /// <summary>账号Id
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>账号代码
        /// </summary>
        public string Code { get; set; }

        public TicketAccount(string accountId, string code)
        {
            AccountId = accountId;
            Code = code;
        }
    }
}
