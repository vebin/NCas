using ECommon.Components;
using ENode.Commanding;
using ENode.EQueue;
using NCas.Common;

namespace NCas.Web.TopicProviders
{
    [Component]
    public class CommandTopicProvider : AbstractTopicProvider<ICommand>
    {
        public override string GetTopic(ICommand command)
        {
            return Topics.NCasCommandTopic;
        }
    }
}