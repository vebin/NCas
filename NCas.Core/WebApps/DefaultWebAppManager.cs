using System;
using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using ECommon.Logging;
using ECommon.Scheduling;

namespace NCas.Core.WebApps
{

    /// <summary>WebApp临时管理
    /// </summary>
    [Component]
    public class DefaultWebAppManager : IWebAppManager
    {
        private readonly Dictionary<string, WebAppCache> _temporaryWebAppDict = new Dictionary<string, WebAppCache>();
        private readonly object _lockObject = new object();
        private readonly int _timeoutSecond;
        private readonly ILogger _logger;

        public DefaultWebAppManager()
        {
        }

        public DefaultWebAppManager(WebAppCacheSetting setting, IScheduleService scheduleService,
            ILoggerFactory loggerFactory)
        {
            _timeoutSecond = setting.WebAppInactiveSeconds;
            _logger = loggerFactory.Create(GetType().FullName);
            scheduleService.StartTask("RemoveExpiredWebApps", RemoveExpiredWebApp, 1000,
                setting.ScanExpiredWebAppIntervalSeconds);
        }

        /// <summary>生成WebApp对应的缓存Id,同时进行缓存
        /// </summary>
        public string GetCacheKey(string webAppId)
        {
            var key = Guid.NewGuid().ToString("N");
            lock (_lockObject)
            {
                _temporaryWebAppDict.Add(key, new WebAppCache(webAppId));
            }
            return key;
        }

        /// <summary>根据缓存Id获取AppId
        /// </summary>
        public string GetWebAppId(string key)
        {
            lock (_lockObject)
            {
                return _temporaryWebAppDict[key].WebAppId;
            }
        }

        /// <summary>移除过期的缓存
        /// </summary>
        public void RemoveExpiredWebApp()
        {
            var expiredWebApps = _temporaryWebAppDict.Values.Where(x => x.IsExpired(_timeoutSecond));
            foreach (var webApp in expiredWebApps)
            {
                lock (_lockObject)
                {
                    if (_temporaryWebAppDict.Remove(webApp.WebAppId))
                    {
                        if (_logger.IsDebugEnabled)
                        {
                            _logger.DebugFormat(
                                "Removed expired WebApp from cache,WebAppId:{0},removeTime:{1}",
                                webApp.WebAppId, DateTime.Now);
                        }
                    }
                }
            }
        }

    }
}
