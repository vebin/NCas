using System;
using ENode.Domain;
using NCas.Common.Enums;

namespace NCas.Domain.Accounts
{
    /// <summary>用户账号聚合根
    /// </summary>
    [Serializable]
    public class Account : AggregateRoot<string>
    {
        private AccountInfo _info;
        private int _useFlag;

        /// <summary>注册账号
        /// </summary>
        public Account(string id, AccountInfo info) : base(id)
        {
            ApplyEvent(new AccountRegistered(this, info));
        }

        /// <summary>删除账号
        /// </summary>
        public void Change(int useFlag)
        {
            ApplyEvent(new AccountChanged(this,useFlag));
        }



        #region Event Handle Methods
        //创建
        private void Handle(AccountRegistered evnt)
        {
            _id = evnt.AggregateRootId;
            _info = evnt.Info;
            _useFlag = (int)UseFlag.Useable;
        }


 
        //删除
        private void Handle(AccountChanged evnt)
        {
            _useFlag = evnt.UseFlag;
        }

        #endregion
    }
}
