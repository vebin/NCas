using NCas.QueryServices.Dtos;

namespace NCas.QueryServices
{
    /// <summary>账号查询接口
    /// </summary>
    public interface IAccountQueryService
    {
        /// <summary>根据账号的代码查询账号
        /// </summary>
        AccountInfoDto FindByCode(string code);
    }
}
