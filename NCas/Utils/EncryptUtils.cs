using GUtils.Encrypt;

namespace NCas.Utils
{
    public class EncryptUtils
    {
        /// <summary>加密Account的密码
        /// </summary>
        public static string EncryptAccountPassword(string password)
        {
            string encryptPassword = EncryptHelper.GetDoubleMd5(password);
            return encryptPassword;
        }

        public static string EncryptAccountCode(string code)
        {
            return EncryptHelper.AesEncryString(code);
        }
    }
}
