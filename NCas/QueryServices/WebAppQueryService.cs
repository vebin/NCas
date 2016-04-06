using System.Collections.Generic;
using ECommon.Components;
using ECommon.Dapper;
using NCas.Common;
using NCas.Common.Enums;
using NCas.QueryServices.Dtos;

namespace NCas.QueryServices
{
    /// <summary>网站节点查询
    /// </summary>
     [Component]
    public class WebAppQueryService : BaseQueryService, IWebAppQueryService
    {
        /// <summary>查询所有的节点
        /// </summary>
        public IEnumerable<WebAppInfoDto> FindAll()
        {
            using (var connection = GetConnection())
            {
                return connection.QueryList<WebAppInfoDto>(new
                {
                    UseFlag = (int) UseFlag.Useable
                }, ConfigSettings.WebAppTable);
            }
        }



    }
}
