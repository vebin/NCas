using System.Collections.Concurrent;

namespace NCas.Core.Tickets
{
    /// <summary>默认票据管理
    /// </summary>
    public class DefaultTicketManager : ITicketManager
    {
        //保存所有的票据
        private readonly ConcurrentDictionary<string, Ticket> Tickets = new ConcurrentDictionary<string, Ticket>();


    }
}
