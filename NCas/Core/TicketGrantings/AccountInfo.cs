using System;

namespace NCas.Core.TicketGrantings
{
    /// <summary>账号信息
    /// </summary>
    [Serializable]
    public class AccountInfo
    {

        /// <summary>账号Id
        /// </summary>
        public string AccountId { get; set; }
        /// <summary>唯一代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>账号名称
        /// </summary>
        public string AccountName { get; set; }

        

        public AccountInfo()
        {
            
        }

        public AccountInfo(string accountId, string code, string accountName)
        {
            AccountId = accountId;
            Code = code;
            AccountName = accountName;
        }
    }
}
