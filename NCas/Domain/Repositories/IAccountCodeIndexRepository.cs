using NCas.Domain.Accounts;

namespace NCas.Domain.Repositories
{
    /// <summary>账号代码索引仓储
    /// </summary>
    public interface IAccountCodeIndexRepository
    {
        /// <summary>根据Code查询账号索引
        /// </summary>
        AccountCodeIndex FindAccountCodeIndex(string code);

        /// <summary>添加账号索引
        /// </summary>
        void AddCodeIndex(AccountCodeIndex index);

        /// <summary>根据账号Id删除索引
        /// </summary>
        void DeleteCodeIndex(string accountId);
    }
}
