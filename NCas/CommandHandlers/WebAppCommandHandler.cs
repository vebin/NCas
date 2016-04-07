using ECommon.Components;
using ENode.Commanding;
using NCas.Commands.WebApps;
using NCas.Domain.WebApps;

namespace NCas.CommandHandlers
{
    /// <summary>WebApp相关处理
    /// </summary>
    [Component]
    public class WebAppCommandHandler
        : ICommandHandler<CreateWebApp>                                                      //创建WebApp
        , ICommandHandler<UpdateWebApp>                                                      //更新WebApp
        , ICommandHandler<ChangeWebApp>                                                      //删除WebApp
    {
        /// <summary>创建WebApp
        /// </summary>
        public void Handle(ICommandContext context, CreateWebApp command)
        {
            var info = new WebAppInfo(command.AppKey, command.AppName, command.Url, command.VerifyTicketUrl,
                command.NotifyUrl);
            var webApp = new WebApp(command.AggregateRootId, info);
            context.Add(webApp);
        }

        /// <summary>更新WebApp
        /// </summary>
        public void Handle(ICommandContext context, UpdateWebApp command)
        {
            var info = new WebAppEditableInfo(command.AppName, command.Url, command.VerifyTicketUrl, command.NotifyUrl);
            context.Get<WebApp>(command.AggregateRootId).Update(info);
        }

        /// <summary>删除WebApp
        /// </summary>
        public void Handle(ICommandContext context, ChangeWebApp command)
        {
            context.Get<WebApp>(command.AggregateRootId).Change(command.UseFlag);
        }
    }
}
