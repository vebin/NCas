namespace NCas.Core.TicketGrantings
{
    /// <summary>TGC管理接口
    /// </summary>
    public interface ITicketGrantingManager
    {
        /// <summary>获取TGC中的账号
        /// </summary>
        AccountInfo GetTicketGranting();

        /// <summary>生成TGC并写入cookie
        /// </summary>
        void SetTicketGranting(AccountInfo account);

        /// <summary>加密生成返回的Account
        /// </summary>
        string BackAccount(AccountInfo account);
    }
}
