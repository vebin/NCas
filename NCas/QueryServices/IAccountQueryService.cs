using System.Collections.Generic;
using NCas.QueryServices.Dtos;

namespace NCas.QueryServices
{
    /// <summary>账号查询接口
    /// </summary>
    public interface IAccountQueryService
    {
        /// <summary>查询所有账号
        /// </summary>
        IEnumerable<AccountInfoDto> FindAll();

        /// <summary>根据账号的代码查询账号
        /// </summary>
        AccountInfoDto FindByCode(string code);

        /// <summary>根据账号名称查询账号信息
        /// </summary>
        AccountInfoVerifyDto FindVerifyDtoByName(string accountName);
    }
}
