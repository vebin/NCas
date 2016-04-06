using ECommon.Components;
using ENode.EQueue;
using ENode.Eventing;
using NCas.Common;

namespace NCas.ProcessorHost.TopicProviders
{
    [Component]
    public class DomainEventTopicProvider : AbstractTopicProvider<IDomainEvent>
    {
        public override string GetTopic(IDomainEvent evnt)
        {
            return Topics.NCasDomainEventTopic;
        }
    }
}
