using ECommon.Components;
using ENode.EQueue;
using ENode.Infrastructure;
using NCas.Common;

namespace NCas.ProcessorHost.TopicProviders
{
    public class ApplicationMessageTopicProvider : AbstractTopicProvider<IApplicationMessage>
    {
        public override string GetTopic(IApplicationMessage applicationMessage)
        {
            return Topics.NCasApplicationMessageTopic;
        }
    }
}
