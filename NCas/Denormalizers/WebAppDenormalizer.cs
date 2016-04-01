using System.Threading.Tasks;
using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using NCas.Common;
using NCas.Common.Enums;
using NCas.Domain.WebApps;

namespace NCas.Denormalizers
{
    /// <summary>网站节点持久化
    /// </summary>
    public class WebAppDenormalizer
        : AbstractDenormalizer
        , IMessageHandler<WebAppCreated>                                                  //创建网站节点
        , IMessageHandler<WebAppChanged>                                                  //删除网站节点
    {

        /// <summary>创建网站节点
        /// </summary>
        public Task<AsyncTaskResult> HandleAsync(WebAppCreated evnt)
        {
            return TryInsertRecordAsync(connection =>
            {
                var info = evnt.Info;
                return connection.InsertAsync(new
                {
                    WebAppId = evnt.AggregateRootId,
                    AppKey=info.AppKey,
                    AppName=info.AppName,
                    Url=info.Url,
                    VerifyTicketUrl=info.VerifyTicketUrl,
                    NotifyUrl=info.NotifyUrl,
                    UseFlag = (int)UseFlag.Useable,
                    Version = evnt.Version,
                    EventSequence = evnt.Sequence
                }, ConfigSettings.WebAppTable);
            });
        }

        /// <summary>删除网站节点
        /// </summary>
        public Task<AsyncTaskResult> HandleAsync(WebAppChanged evnt)
        {
            return TryUpdateRecordAsync(connection => connection.UpdateAsync(new
            {
                UseFlag = evnt.UseFlag,
                Version = evnt.Version,
                EventSequence = evnt.Sequence
            }, new
            {
                AccountId = evnt.AggregateRootId,
                Version = evnt.Version - 1
            }, ConfigSettings.WebAppTable));
        }
    }
}
