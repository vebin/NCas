using System;
using GUtils.Encrypt;
using GUtils.Logging;
using GUtils.Serializing;
using GUtils.Utilities;

namespace NCas.Core.TicketGrantingCookies
{
    /// <summary>TGC管理
    /// 
    /// </summary>
    public class DefaultTicketGrantingCookieManager : ITicketGrantingCookieManager
    {
        private readonly ILogger _logger;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly TicketGrantingCookieSetting _setting;
        public DefaultTicketGrantingCookieManager(TicketGrantingCookieSetting setting, ILoggerFactory loggerFactory,
            IJsonSerializer jsonSerializer)
        {
            _setting = setting;
            _logger = loggerFactory.Create(GetType().FullName);
            _jsonSerializer = jsonSerializer;
        }

        /// <summary>获取TGC
        /// </summary>
        public AccountInfo GetTicketGrantingCookie()
        {
            var cookieString = CookieUtils.Get("TGC");
            var account = DecodeCookie(cookieString);
            return account;
        }

        /// <summary>生成TGC并写入cookie
        /// </summary>
        public void SetTicketGrantingCookie(AccountInfo account)
        {
            var cookieString = EncodeCookie(account);
            var exp = _setting.TgcExpiredSeconds == 0
                ? DateTime.MaxValue
                : DateTime.Now.AddSeconds(_setting.TgcExpiredSeconds);
            CookieUtils.WriteCookie("TGC", cookieString, exp);
        }








        /// <summary>对TGC进行加密
        /// </summary>
        private string EncodeCookie(AccountInfo account)
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
        private AccountInfo DecodeCookie(string encodeString)
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

    }
}
