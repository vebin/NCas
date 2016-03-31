using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommon.Components;
using ECommon.Dapper;
using NCas.Common;
using NCas.Domain.Accounts;

namespace NCas.Domain.Repositories
{
    /// <summary>账号仓储
    /// </summary>
    [Component]
    public class AccountRepository : BaseRepository, IAccountRepository
    {

        /// <summary>根据Code查询账号索引
        /// </summary>
        public AccountIndex FindAccountIndex(string code)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                return
                    connection.QueryList<AccountIndex>(new {Code = code}, ConfigSettings.AccountIndexTable)
                        .FirstOrDefault();
            }
        }

        /// <summary>添加账号索引
        /// </summary>
        public void AddIndex(AccountIndex index)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                connection.Insert(new
                {
                    AccountId=index.AccountId,
                    Code=index.Code
                },ConfigSettings.AccountIndexTable);
            }
        }

        /// <summary>根据账号Id删除索引
        /// </summary>
        public void DeleteIndex(string accountId)
        {
            using (var connection=GetConnection())
            {
                connection.Open();
                connection.Delete(new
                {
                    AccountId=accountId
                }, ConfigSettings.AccountIndexTable);
            }
        }

    }
}
