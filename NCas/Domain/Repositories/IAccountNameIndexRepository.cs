using NCas.Domain.Accounts;

namespace NCas.Domain.Repositories
{
    /// <summary>账号名称索引仓储接口
    /// </summary>
    public interface IAccountNameIndexRepository
    {
        /// <summary>根据AccountName查询账号索引
        /// </summary>
        AccountNameIndex FindAccountNameIndex(string accountName);

        /// <summary>添加账号索引
        /// </summary>
        void AddNameIndex(AccountNameIndex index);

        /// <summary>根据账号Id删除名称索引
        /// </summary>
        void DeleteNameIndex(string accountId);
    }
}
