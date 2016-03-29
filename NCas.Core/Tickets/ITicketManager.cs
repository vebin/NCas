namespace NCas.Core.Tickets
{
    /// <summary>票据管理接口
    /// </summary>
    public interface ITicketManager
    {
        /// <summary>生成票据,并保存到集合
        /// </summary>
        Ticket CreateTicket();

        /// <summary>验证票据是否存在,如果该票据存在,则验证之后就销毁
        /// </summary>
        bool VerifyTicket(string ticketValue);


    }
}
