using System;
using ENode.Eventing;

namespace NCas.Domain.WebApps
{
    /// <summary>创建网站节点事件
    /// </summary>
    [Serializable]
    public class WebAppCreated : DomainEvent<string>
    {
        public WebAppInfo Info { get; private set; }

        public WebAppCreated()
        {
            
        }

        public WebAppCreated(WebApp webApp, WebAppInfo info) : base()
        {
            Info = info;
        }
    }
}
