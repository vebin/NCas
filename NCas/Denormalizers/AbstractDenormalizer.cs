using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ECommon.IO;
using NCas.Common;

namespace NCas.Denormalizers
{
    /// <summary>增删查改抽象基类
    /// </summary>
    public abstract class AbstractDenormalizer
    {
        /// <summary>异步插入数据
        /// </summary>
        protected async Task<AsyncTaskResult> TryInsertRecordAsync(Func<IDbConnection, Task<long>> action)
        {
            try
            {
                using (var connection = GetConnection())
                {
                    await action(connection);
                    return AsyncTaskResult.Success;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)  //主键冲突，忽略即可；出现这种情况，是因为同一个消息的重复处理
                {
                    return AsyncTaskResult.Success;
                }
                throw;
            }
        }
        /// <summary> 异步更新数据
        /// </summary>
        protected async Task<AsyncTaskResult> TryUpdateRecordAsync(Func<IDbConnection, Task<int>> action)
        {
            using (var connection = GetConnection())
            {
                await action(connection);
                return AsyncTaskResult.Success;
            }
        }
        /// <summary>使用事物进行操作
        /// </summary>
        protected async Task<AsyncTaskResult> TryTransactionAsync(Func<IDbConnection, IDbTransaction, Task> action)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync().ConfigureAwait(false);
                var transaction = await Task.Run<SqlTransaction>(() => connection.BeginTransaction()).ConfigureAwait(false);
                try
                {
                    await action(connection, transaction).ConfigureAwait(false);
                    await Task.Run(() => transaction.Commit()).ConfigureAwait(false);
                    return AsyncTaskResult.Success;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>使用事物进行操作
        /// </summary>
        protected async Task<AsyncTaskResult> TryTransactionAsync(Func<IDbConnection, IDbTransaction, IEnumerable<Task>> actions)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync().ConfigureAwait(false);
                var transaction = await Task.Run<SqlTransaction>(() => connection.BeginTransaction()).ConfigureAwait(false);
                try
                {
                    await Task.WhenAll(actions(connection, transaction)).ConfigureAwait(false);
                    await Task.Run(() => transaction.Commit()).ConfigureAwait(false);
                    return AsyncTaskResult.Success;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(ConfigSettings.CasConnectionString);
        }
    }
}
