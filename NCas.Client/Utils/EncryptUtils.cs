using GUtils.Encrypt;
using GUtils.Serializing;

namespace NCas.Client.Utils
{
    public class EncryptUtils
    {
        /// <summary>解密票据
        /// </summary>
        public static string DecryptTicket(string ticket)
        {
            return EncryptHelper.AesDecryString(ticket, password: "123456");
        }

        public static Account DecryptAccount(string encryptAccount,string key)
        {
            var jsonAccount = EncryptHelper.AesDecryString(encryptAccount, password: key);
            IJsonSerializer serializer=new DefaultJsonSerializer();
            var account = serializer.Deserialize<Account>(jsonAccount);
            return account;
        }

        public static string DecryptAccountCode(string encryptAccount)
        {
            var code = EncryptHelper.AesDecryString(encryptAccount);
            return code;
        }

    }
}
