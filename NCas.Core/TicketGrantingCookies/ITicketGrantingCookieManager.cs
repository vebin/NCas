using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCas.Core.TicketGrantingCookies
{
    /// <summary>TGC管理接口
    /// </summary>
    public interface ITicketGrantingCookieManager
    {
        /// <summary>获取TGC中的账号
        /// </summary>
        AccountInfo GetTicketGrantingCookie();

        /// <summary>生成TGC并写入cookie
        /// </summary>
        void SetTicketGrantingCookie(AccountInfo account);
    

    }
}
