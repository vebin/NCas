namespace NCas.QueryServices.Dtos
{
    /// <summary>账号详情Dto
    /// </summary>
    public class AccountInfoVerifyDto
    {
         public string AccountId { get; set; }
        public string Code { get; set; }
        public string AccountName { get; set; }

        public string Password { get; set; }
        public AccountInfoVerifyDto()
        {
            
        }

        public AccountInfoVerifyDto(string accountId, string code, string accountName, string password)
        {
            AccountId = accountId;
            Code = code;
            AccountName = accountName;
            Password = password;
        }
    }
}
