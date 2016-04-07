namespace NCas.Core.TicketGrantings
{
    /// <summary>账号回写实体
    /// </summary>
    public class AccountBack
    {
        /// <summary>本系统唯一的Id
        /// </summary>
        public string Code { get; set; }
        /// <summary>账号名
        /// </summary>
        public string Name { get; set; }

        public AccountBack(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
