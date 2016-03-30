using GUtils.Components;
using GUtils.Encrypt;
using GUtils.Serializing;

namespace NCas.Core.TicketGrantingCookies
{
    /// <summary>TGC票据
    /// </summary>
    public class TicketGrantingCookie
    {
        /// <summary>票据中的账号信息
        /// </summary>
        public AccountInfo Info { get; set; }
        private readonly IJsonSerializer  _jsonSerializer;

        public TicketGrantingCookie(AccountInfo info)
        {
            Info = info;
            _jsonSerializer = ObjectContainer.Resolve<IJsonSerializer>();
        }

        /// <summary>对TGC进行加密
        /// </summary>
        public string EncodeCookie()
        {
            var json = _jsonSerializer.Serialize(Info);
            var encryptCookie = EncryptHelper.AesEncryString(json);
            return encryptCookie;
        }

        public AccountInfo DecodeCookie(string encodeString)
        {
            var encryptCookie = EncryptHelper.AesDecryString(encodeString);
            var account = _jsonSerializer.Deserialize<AccountInfo>(encryptCookie);
            return account;
        }






    }
}
