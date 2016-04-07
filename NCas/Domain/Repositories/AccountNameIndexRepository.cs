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
    /// <summary>账号名称索引仓储
    /// </summary>
    [Component]
    public class AccountNameIndexRepository :BaseRepository, IAccountNameIndexRepository
    {
        /// <summary>根据AccountName查询账号索引
        /// </summary>
        public AccountNameIndex FindAccountNameIndex(string accountName)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                return
                    connection.QueryList<AccountNameIndex>(new { AccountName = accountName }, ConfigSettings.AccountNameIndexTable)
                        .FirstOrDefault();
            }
        }

        /// <summary>添加账号名称索引
        /// </summary>
        public void AddNameIndex(AccountNameIndex index)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                connection.Insert(new
                {
                    AccountId = index.AccountId,
                    AccountName = index.AccountName
                }, ConfigSettings.AccountNameIndexTable);
            }
        }

        /// <summary>根据账号Id删除名称索引
        /// </summary>
        public void DeleteNameIndex(string accountId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                connection.Delete(new
                {
                    AccountId = accountId
                }, ConfigSettings.AccountNameIndexTable);
            }
        }
    }
}
