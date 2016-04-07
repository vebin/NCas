using System;
using ENode.Domain;
using NCas.Common;
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
            Assert.IsNotNullOrEmpty("账号唯一编码", info.Code);
            Assert.IsNotNullOrEmpty("账号名", info.AccountName);
            Assert.IsNotNullOrEmpty("密码", info.Password);
            ApplyEvent(new AccountRegistered(this, info));
        }

        /// <summary>修改账号名
        /// </summary>
        public void UpdateAccountName(string accountName)
        {
            Assert.IsNotNullOrEmpty("账号名", accountName);
            ApplyEvent(new AccountNameUpdated(this, accountName));
        }

        /// <summary>修改密码
        /// </summary>
        public void UpdatePassword(string password)
        {
            ApplyEvent(new AccountPasswordUpdated(this,password));
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

        //修改账号名
        private void Handle(AccountNameUpdated evnt)
        {
            _info.AccountName = evnt.AccountName;
        }


        //修改密码
        private void Handle(AccountPasswordUpdated evnt)
        {
            _info.Password = evnt.Password;
        }

        //删除
        private void Handle(AccountChanged evnt)
        {
            _useFlag = evnt.UseFlag;
        }

        #endregion
    }
}
