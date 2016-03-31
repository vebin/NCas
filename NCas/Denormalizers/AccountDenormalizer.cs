﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommon.Dapper;
using ECommon.IO;
using ENode.Infrastructure;
using NCas.Common;
using NCas.Common.Enums;
using NCas.Domain.Accounts;

namespace NCas.Denormalizers
{
    /// <summary>账号持久化
    /// </summary>
    public class AccountDenormalizer
        : AbstractDenormalizer
        , IMessageHandler<AccountRegistered>                                                     //注册账号
        , IMessageHandler<AccountChanged>                                                        //删除账号
    {

        /// <summary>注册账号
        /// </summary>
        public Task<AsyncTaskResult> HandleAsync(AccountRegistered evnt)
        {
            return TryInsertRecordAsync(connection =>
            {
                var info = evnt.Info;
                return connection.InsertAsync(new
                {
                    AccountId = evnt.AggregateRootId,
                    Code = info.Code,
                    AccountName = info.AccountName,
                    Password = info.Password,
                    UseFlag = (int) UseFlag.Useable,
                    Version = evnt.Version,
                    EventSequence = evnt.Sequence
                }, ConfigSettings.AccountTable);
            });
        }

        /// <summary>删除账号
        /// </summary>
        public Task<AsyncTaskResult> HandleAsync(AccountChanged evnt)
        {
            return TryUpdateRecordAsync(connection => connection.UpdateAsync(new
            {
                UseFlag=evnt.UseFlag,
                Version=evnt.Version,
                EventSequence=evnt.Sequence
            }, new
            {
                AccountId=evnt.AggregateRootId,
                Version=evnt.Version-1
            }, ConfigSettings.AccountTable));
        }
    }
}
