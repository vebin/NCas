using GUtils.Encrypt;

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

        
    }
}
