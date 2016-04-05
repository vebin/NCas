using System.Linq;
using ECommon.Dapper;
using NCas.Common;
using NCas.Common.Enums;
using NCas.QueryServices.Dtos;

namespace NCas.QueryServices
{
    /// <summary>账号查询
    /// </summary>
    public class AccountQueryService : BaseQueryService, IAccountQueryService
    {
        /// <summary>根据账号的代码查询账号
        /// </summary>
        public AccountInfoDto FindByCode(string code)
        {
            using (var connection = GetConnection())
            {
                return connection.QueryList<AccountInfoDto>(new
                {
                    Code = code,
                    UseFlag = (int) UseFlag.Useable
                }, ConfigSettings.AccountTable).FirstOrDefault();
            }
        }

        /// <summary>根据账号名称查询账号信息
        /// </summary>
        public AccountInfoVerifyDto FindVerifyDtoByName(string accountName)
        {
            using (var connection = GetConnection())
            {
                return connection.QueryList<AccountInfoVerifyDto>(new
                {
                    AccountName = accountName,
                    UseFlag = (int) UseFlag.Useable
                }, ConfigSettings.AccountTable).FirstOrDefault();
            }
        }
    }
}
