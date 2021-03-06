﻿using System;
using ECommon.Components;
using ECommon.Logging;
using ECommon.Serializing;
using GUtils.Encrypt;
using GUtils.Utilities;

namespace NCas.Core.TicketGrantings
{
    /// <summary>TGC管理
    /// </summary>
    [Component]
    public class DefaultTicketGrantingManager : ITicketGrantingManager
    {
        private readonly ILogger _logger;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly TicketGrantingSetting _setting;

        public DefaultTicketGrantingManager()
        {

        }

        public DefaultTicketGrantingManager(TicketGrantingSetting setting, ILoggerFactory loggerFactory,
            IJsonSerializer jsonSerializer)
        {
            _setting = setting;
            _logger = loggerFactory.Create(GetType().FullName);
            _jsonSerializer = jsonSerializer;
        }

        /// <summary>获取TGC
        /// </summary>
        public AccountInfo GetTicketGranting()
        {
            var cookieString = CookieUtils.Get("TGC");
            var account = string.IsNullOrEmpty(cookieString) ? null : DecodeCookie(cookieString);
            return account;
        }

        /// <summary>生成TGC并写入cookie
        /// </summary>
        public void SetTicketGranting(AccountInfo account)
        {
            var cookieString = EncodeCookie(account);
            var exp = _setting.TgcExpiredSeconds == 0
                ? DateTime.MaxValue
                : DateTime.Now.AddSeconds(_setting.TgcExpiredSeconds);
            CookieUtils.WriteCookie("TGC", cookieString, exp);
        }

        /// <summary>移除cookie的 TGC
        /// </summary>
        public void RemoveTicketGranting()
        {
           CookieUtils.Remove("TGC");
        }

        /// <summary>对TGC进行加密
        /// </summary>
        public string EncodeCookie(AccountInfo account)
        {
            try
            {
                var json = _jsonSerializer.Serialize(account);
                var encryptCookie = EncryptHelper.AesEncryString(json);
                return encryptCookie;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("EncodeCookie Error:{0}", ex.Message);
            }
            return string.Empty;
        }

        /// <summary>TGC解密
        /// </summary>
        public AccountInfo DecodeCookie(string encodeString)
        {
            try
            {
                var encryptCookie = EncryptHelper.AesDecryString(encodeString);
                var account = _jsonSerializer.Deserialize<AccountInfo>(encryptCookie);
                return account;
            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("DecodeCookie Error:{0}", ex.Message);

            }
            return null;
        }

        /// <summary>加密生成返回的Account
        /// </summary>
        public string BackAccount(AccountInfo account, out string key)
        {
            key = RandomUtils.GetOnlyNumber(4);
            var accountBack = new AccountBack(account.Code, account.AccountName);
            var json = _jsonSerializer.Serialize(accountBack);
            var encryptAccount = EncryptHelper.AesEncryString(json, password: key);
            return encryptAccount;
        }

    }
}
