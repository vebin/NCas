using System;
using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using ECommon.Logging;
using ECommon.Scheduling;
using GUtils.Utilities;
using NCas.QueryServices;

namespace NCas.Core.WebApps
{

    /// <summary>WebApp临时管理
    /// </summary>
    [Component]
    public class DefaultWebAppManager : IWebAppManager
    {
        /// <summary>已经登陆的WebApp,以生成的WebAppKey为Key
        /// </summary>
        private readonly Dictionary<string, WebAppCache> _temporaryWebAppDict = new Dictionary<string, WebAppCache>();
        /// <summary>WebApp数据集合,以WebAppId为key
        /// </summary>
        private readonly Dictionary<string, WebAppInfo> _webAppInfoDict = new Dictionary<string, WebAppInfo>(); 

        private readonly object _lockObject = new object();
        private readonly int _timeoutSecond;
        private readonly ILogger _logger;
        private readonly IWebAppQueryService _webAppQueryService;

        public DefaultWebAppManager()
        {

        }

        public DefaultWebAppManager(WebAppCacheSetting setting, IScheduleService scheduleService,
            ILoggerFactory loggerFactory, IWebAppQueryService webAppQueryService)
        {
            _timeoutSecond = setting.WebAppInactiveSeconds;
            _logger = loggerFactory.Create(GetType().FullName);
            _webAppQueryService = webAppQueryService;
            UpdateWebAppInfoDict();
            scheduleService.StartTask("UpdateWebAppInfoDict", UpdateWebAppInfoDict, 1000, 1000*60*3);
        }

        /// <summary>生成对应的,用于数据库缓存的Key
        /// </summary>
        public string GetCacheKey(string webAppId)
        {
            var key = Guid.NewGuid().ToString("N");
            lock (_lockObject)
            {
                //先要查询一下是否存在该WebApp
                var webAppInfo = _webAppInfoDict.FirstOrDefault(x => x.Key == webAppId).Value;
                if (webAppInfo == null)
                {
                    _logger.InfoFormat("Not Find The WebApp By WebAppId ! WebAppId:{0}", webAppId);
                }
                else
                {
                    _temporaryWebAppDict.Add(key, new WebAppCache(webAppId));
                }
            }
            return key;
        }


        /// <summary>设置为登陆
        /// </summary>
        public void SetLogin(string webAppId)
        {
            lock (_lockObject)
            {
                //获取缓存的WebApp
                var webAppCache =
                    _temporaryWebAppDict.FirstOrDefault(x => x.Value.WebAppId == webAppId).Value;
                if (webAppCache != null)
                {
                    webAppCache.SetLogin();
                }
            }
        }

        /// <summary>根据WebApp缓存Key获取缓存
        /// </summary>
        public WebAppInfo GetWebAppInfoByCacheKey(string key, string callBackUrl = "")
        {
            lock (_lockObject)
            {
                var webAppCache = _temporaryWebAppDict.FirstOrDefault(x => x.Key == key).Value;
                if (webAppCache == null)
                {
                    if (string.IsNullOrEmpty(callBackUrl))
                    {
                        _logger.ErrorFormat(
                            "The WebApp can't been found by cacheKey:{0}, and the callBackUrl is also null.", key);
                    }
                    return _webAppInfoDict.FirstOrDefault(x => PathUtils.SameDomain(x.Value.Url, callBackUrl)).Value;
                }
                return _webAppInfoDict.FirstOrDefault(x => x.Key == webAppCache.WebAppId).Value;
            }
        }

        /// <summary>根据WebAppId获取系统中的WebApp
        /// </summary>
        public WebAppInfo GetWebAppInfoById(string webAppId)
        {
            lock (_lockObject)
            {
                return _webAppInfoDict.FirstOrDefault(x => x.Key == webAppId).Value;
            }
        }


        /// <summary>根据Url获取唯一的WebApp
        /// </summary>
        public WebAppInfo GetWebAppInfoByUrl(string url)
        {
            lock (_lockObject)
            {
                return _webAppInfoDict.FirstOrDefault(x => PathUtils.SameDomain(x.Value.Url, url)).Value;
            }
        }

        /// <summary>更新WebApp的操作
        /// </summary>
        public void UpdateWebAppInfoDict()
        {
            var webAppInfoDtos = _webAppQueryService.FindAll();

            lock (_lockObject)
            {
                foreach (var webAppInfoDto in webAppInfoDtos)
                {
                    var webAppInfo = new WebAppInfo(webAppInfoDto.WebAppId, webAppInfoDto.AppKey, webAppInfoDto.AppName,
                        webAppInfoDto.Url, webAppInfoDto.VerifyTicketUrl, webAppInfoDto.PutAccountUrl,
                        webAppInfoDto.NotifyUrl);
                    _webAppInfoDict.Add(webAppInfo.WebAppId, webAppInfo);
                }
            }
        }

        /// <summary>获取全部的WebApps
        /// </summary>
        public List<WebAppInfo> GetAllWebApps()
        {
            return _webAppInfoDict.Select(x => x.Value).ToList();
        }


        ///// <summary>移除过期的缓存
        ///// </summary>
        //public void RemoveExpiredWebApp()
        //{
        //    var expiredWebApps = _temporaryWebAppDict.Values.Where(x => x.IsExpired(_timeoutSecond));
        //    foreach (var webApp in expiredWebApps)
        //    {
        //        lock (_lockObject)
        //        {
        //            if (_temporaryWebAppDict.Remove(webApp.WebAppId))
        //            {
        //                if (_logger.IsDebugEnabled)
        //                {
        //                    _logger.DebugFormat(
        //                        "Removed expired WebApp from cache,WebAppId:{0},removeTime:{1}",
        //                        webApp.WebAppId, DateTime.Now);
        //                }
        //            }
        //        }
        //    }
        //}

    }
}
