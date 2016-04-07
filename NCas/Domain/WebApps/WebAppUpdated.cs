using System;
using ENode.Eventing;

namespace NCas.Domain.WebApps
{
    /// <summary>更新WebApp事件
    /// </summary>
    [Serializable]
    public class WebAppUpdated : DomainEvent<string>
    {
        public WebAppEditableInfo Info { get; private set; }

        public WebAppUpdated()
        {

        }

        public WebAppUpdated(WebApp webApp, WebAppEditableInfo info) : base()
        {
            Info = info;
        }
    }
}
