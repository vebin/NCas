using System.Collections.Generic;

namespace NCas.Core.WebApps
{
    public interface IWebAppManager
    {
        /// <summary>生成WebApp对应的缓存Id,同时进行缓存
        /// </summary>
        string GetCacheKey(string webAppId);

        /// <summary>设置为登陆
        /// </summary>
        void SetLogin(string webAppId);

        /// <summary>根据WebApp缓存Key获取缓存
        /// </summary>
        WebAppInfo GetWebAppInfoByCacheKey(string key, string callBackUrl);

        /// <summary>根据WebAppId获取系统中的WebApp
        /// </summary>
        WebAppInfo GetWebAppInfoById(string webAppId);

        /// <summary>根据Url获取唯一的WebApp
        /// </summary>
        WebAppInfo GetWebAppInfoByUrl(string url);

        /// <summary>获取全部的WebApps
        /// </summary>
        List<WebAppInfo> GetAllWebApps();
    }
}
