using System.Linq;
using ECommon.Components;
using ECommon.Dapper;
using NCas.Common;
using NCas.Domain.Accounts;

namespace NCas.Domain.Repositories
{
    /// <summary>账号代码索引仓储
    /// </summary>
    [Component]
    public class AccountCodeIndexRepository : BaseRepository,IAccountCodeIndexRepository
    {
        /// <summary>根据Code查询账号索引
        /// </summary>
        public AccountCodeIndex FindAccountCodeIndex(string code)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                return
                    connection.QueryList<AccountCodeIndex>(new { Code = code }, ConfigSettings.AccountCodeIndexTable)
                        .FirstOrDefault();
            }
        }

        /// <summary>添加账号索引
        /// </summary>
        public void AddCodeIndex(AccountCodeIndex index)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                connection.Insert(new
                {
                    AccountId = index.AccountId,
                    Code = index.Code
                }, ConfigSettings.AccountCodeIndexTable);
            }
        }

        /// <summary>根据账号Id删除索引
        /// </summary>
        public void DeleteCodeIndex(string accountId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                connection.Delete(new
                {
                    AccountId = accountId
                }, ConfigSettings.AccountCodeIndexTable);
            }
        }
    }
}
