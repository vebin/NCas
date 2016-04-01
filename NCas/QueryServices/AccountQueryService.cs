using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var data = connection.QueryList(new
                {
                    Code = code,
                    UseFlag = (int) UseFlag.Useable
                }, ConfigSettings.AccountTable).FirstOrDefault();

                if (data != null)
                {
                    return new AccountInfoDto(data.AccountId, data.Code, data.AccountName);
                }
                return null;
            }
        }
    }
}
