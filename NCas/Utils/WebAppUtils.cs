using System;

namespace NCas.Utils
{
    public class WebAppUtils
    {
        /// <summary>生成WebAppKey
        /// </summary>
        public static string CreateWebAppKey()
        {
            return Guid.NewGuid().ToString("N");
        }

    }
}
