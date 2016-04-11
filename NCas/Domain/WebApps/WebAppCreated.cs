using System;
using ENode.Eventing;

namespace NCas.Domain.WebApps
{
    /// <summary>创建网站节点事件
    /// </summary>
    public class WebAppCreated : DomainEvent<string>
    {
        public WebAppInfo Info { get; private set; }

        public WebAppCreated()
        {
            
        }

        public WebAppCreated(WebAppInfo info)
        {
            Info = info;
        }
    }
}
