using ECommon.Components;
using ENode.EQueue;
using ENode.Infrastructure;
using NCas.Common;

namespace NCas.ProcessorHost.TopicProviders
{
    [Component]
    public class ExceptionTopicProvider : AbstractTopicProvider<IPublishableException>
    {
        public override string GetTopic(IPublishableException exception)
        {
            return Topics.NCasExceptionTopic;
        }
    }
}
