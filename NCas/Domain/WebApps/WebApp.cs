using ENode.Domain;
using NCas.Common;
using NCas.Common.Enums;

namespace NCas.Domain.WebApps
{
    /// <summary>网站节点聚合根
    /// </summary>
    public class WebApp : AggregateRoot<string>
    {
        private WebAppInfo _info;
        private int _useFlag;

        /// <summary>创建网站节点
        /// </summary>
        public WebApp(string id, WebAppInfo info) : base(id)
        {
            ApplyEvent(new WebAppCreated(this, info));
        }

        /// <summary>删除节点
        /// </summary>
        public void Change(int useFlag)
        {
            Assert.IsNotInEnum("删除状态", typeof (UseFlag), useFlag);
            ApplyEvent(new WebAppChanged(this, useFlag));
        }


        #region Event Handle Methods
        //创建
        private void Handle(WebAppCreated evnt)
        {
            _id = evnt.AggregateRootId;
            _info = evnt.Info;
            _useFlag = (int)UseFlag.Useable;
        }


        //删除
        private void Handle(WebAppChanged evnt)
        {
            _useFlag = evnt.UseFlag;
        }

        #endregion

    }
}
