using System;

namespace NCas.Core.WebApps
{
    public class WebAppCache
    {
        public string WebAppId { get; set; }
        public DateTime CreateTime { get; set; }
        /// <summary>是否登陆
        /// </summary>
        public bool IsLogin { get; set; }

        /// <summary>判断票据是否过期
        /// </summary>
        public bool IsExpired(int timeoutSeconds)
        {
            return (DateTime.Now - CreateTime).TotalSeconds >= timeoutSeconds;
        }

        public WebAppCache(string webAppId)
        {
            WebAppId = webAppId;
            CreateTime = DateTime.Now;
            IsLogin = false;
        }

        public void SetLogin()
        {
            IsLogin = true;
        }
    }
}
