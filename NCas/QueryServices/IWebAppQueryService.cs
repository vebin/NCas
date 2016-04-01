using System.Collections.Generic;
using NCas.QueryServices.Dtos;

namespace NCas.QueryServices
{
    /// <summary>网站节点查询接口
    /// </summary>
    public interface IWebAppQueryService
    {
        /// <summary>查询所有的节点
        /// </summary>
        IEnumerable<WebAppInfoDto> FindAll();
    }
}
