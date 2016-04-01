﻿namespace NCas.QueryServices.Dtos
{
    /// <summary>账号信息Dto
    /// </summary>
    public class AccountInfoDto
    {
        public string AccountId { get; set; }
        public string Code { get; set; }
        public string AccountName { get; set; }

        public AccountInfoDto()
        {
            
        }

        public AccountInfoDto(string accountId, string code, string accountName)
        {
            AccountId = accountId;
            Code = code;
            AccountName = accountName;
        }
    }
}
