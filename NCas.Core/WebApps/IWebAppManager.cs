using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCas.Core.WebApps
{
    public interface IWebAppManager
    {
        /// <summary>生成WebApp对应的缓存Id,同时进行缓存
        /// </summary>
        string GetCacheKey(string webAppId);

        /// <summary>根据缓存Id获取AppId
        /// </summary>
        string GetWebAppId(string key);

    }
}
