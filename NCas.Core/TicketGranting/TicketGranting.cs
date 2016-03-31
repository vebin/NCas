using GUtils.Components;
using GUtils.Encrypt;
using GUtils.Serializing;

namespace NCas.Core.TicketGranting
{
    /// <summary>TGC票据
    /// </summary>
    public class TicketGranting
    {
        /// <summary>票据中的账号信息
        /// </summary>
        public AccountInfo Info { get; set; }
        private readonly IJsonSerializer  _jsonSerializer;

        public TicketGranting(AccountInfo info)
        {
            Info = info;
            _jsonSerializer = ObjectContainer.Resolve<IJsonSerializer>();
        }

        /// <summary>对TGC进行加密转换成cookie存储的字符
        /// </summary>
        public string EncodeCookie()
        {
            var json = _jsonSerializer.Serialize(Info);
            var encryptCookie = EncryptHelper.AesEncryString(json);
            return encryptCookie;
        }

        /// <summary>对cookie中的TGC字符进行解密,生成账号信息
        /// </summary>
        public AccountInfo DecodeCookie(string encodeString)
        {
            var encryptCookie = EncryptHelper.AesDecryString(encodeString);
            var account = _jsonSerializer.Deserialize<AccountInfo>(encryptCookie);
            return account;
        }






    }
}
