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

        /// <summary>根据WebAppId查询WebApp信息
        /// </summary>
        WebAppInfoDto FindById(string webAppId);

        /// <summary>根据WebAppKey查询WebApp信息
        /// </summary>
        WebAppInfoDto FindByKey(string webAppKey);
    }
}
