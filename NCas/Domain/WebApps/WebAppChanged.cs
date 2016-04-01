using ENode.Eventing;

namespace NCas.Domain.WebApps
{
    /// <summary>删除网站节点事件
    /// </summary>
    public class WebAppChanged : DomainEvent<string>
    {
        public int UseFlag { get; private set; }

        public WebAppChanged()
        {

        }

        public WebAppChanged(WebApp webApp, int useFlag) : base()
        {
            UseFlag = useFlag;
        }
    }
}
