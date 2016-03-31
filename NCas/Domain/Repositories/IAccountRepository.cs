using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCas.Domain.Accounts;

namespace NCas.Domain.Repositories
{
    /// <summary>账号仓储接口
    /// </summary>
    public interface IAccountRepository
    {
        /// <summary>根据Code查询账号索引
        /// </summary>
        AccountIndex FindAccountIndex(string code);

        /// <summary>添加账号索引
        /// </summary>
        void AddIndex(AccountIndex index);

        /// <summary>根据账号Id删除索引
        /// </summary>
        void DeleteIndex(string accountId);
    }
}
