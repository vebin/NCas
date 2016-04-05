using System;

namespace NCas.Core.TicketGrantings
{
    /// <summary>账号信息
    /// </summary>
    [Serializable]
    public class AccountInfo
    {
        /// <summary>系统信息
        /// </summary>
        public string  WebAppId { get; set; }

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

        public AccountInfo(string webAppId,string accountId, string code, string accountName)
        {
            WebAppId = webAppId;
            AccountId = accountId;
            Code = code;
            AccountName = accountName;
        }
    }
}
