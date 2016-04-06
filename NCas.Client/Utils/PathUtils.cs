using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NCas.Client.Utils
{
    /// <summary>路径工具类
    /// </summary>
    public class PathUtils
    {
        /// <summary>If the path is absolute is return as is, otherwise is combined with AppDomain.CurrentDomain.SetupInformation.ApplicationBase
        /// The path are always server relative path.
        /// </summary>
        public static string LocateServerPath(string path)
        {
            if (System.IO.Path.IsPathRooted(path) == false)
            {
                path = System.IO.Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, path);
            }
            return path;
        }

        /// <summary>相对路径信息转换成绝对路径信息
        /// </summary>
        public static string Mappath(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                return relativePath;
            }
            relativePath = relativePath.Replace("//", "/");
            //当前的程序的目录
            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string gainPath = RelativeToAbsolute(relativePath);
            //如果是以~/xxx/xx形式的,或者以/aaa/xxx形式的,则直接拼
            if (relativePath.StartsWith(@"~") || relativePath.StartsWith(@"/"))
            {
                return System.IO.Path.Combine(basePath, gainPath);
            }
            string[] str = relativePath.Split('/');
            basePath = str.Where(p => p == "..").Aggregate(basePath, (current, p) => BackOneDict(current));
            string absolutePath = System.IO.Path.Combine(basePath, gainPath);
            return absolutePath;
        }

        /// <summary>回退一个目录
        /// </summary>
        public static string BackOneDict(string absolutePath)
        {
            //如果是绝对路径
            if (System.IO.Path.IsPathRooted(absolutePath) == true)
            {
                absolutePath = absolutePath.Replace(@"\\", @"\");
                if (absolutePath.EndsWith(@"\"))
                {
                    absolutePath = absolutePath.Remove(absolutePath.Length - 1, 1);
                }
                absolutePath = absolutePath.Substring(0, absolutePath.LastIndexOf('\\'));
            }
            return absolutePath;
        }

        /// <summary>合并相对Url和参照Url
        /// </summary>
        public static string CombineUrl(string baseUrl, string relativeUrl)
        {
            if (relativeUrl.Length == 0 || relativeUrl[0] != '/')
            {
                relativeUrl = '/' + relativeUrl;
            }

            if (baseUrl.Length > 0 && baseUrl[baseUrl.Length - 1] == '/')
            {
                baseUrl = baseUrl.Substring(0, baseUrl.Length - 1);
            }

            return baseUrl + relativeUrl;
        }

        /// <summary>组合相对路径
        /// </summary>
        public static string CombineRelativePath(params string[] paths)
        {
            return paths.Aggregate("", CombineRelativePath);
        }

        /// <summary>相对路径转换成绝对路径
        /// </summary>
        public static string RelativeToAbsolute(string relativePath)
        {
            string[] str = relativePath.Split('/');
            string path = str.Where(item => !string.IsNullOrEmpty(item) && item != "~" && item != "..")
                .Aggregate("", (current, item) => current + (item + @"\"));
            path = path.Remove(path.Length - 1);
            return path;
        }

        /// <summary>合并盘符+绝对路径
        /// </summary>
        public static string CombineDriverAbsolute(string driver, string absolutePath)
        {
            if (driver.Length > 0 && driver.IndexOf(":", StringComparison.Ordinal) < 0)
            {
                driver = driver + @":";
            }
            driver += @"\";
            if (absolutePath.Length > 0)
            {
                if (absolutePath.StartsWith(@"\"))
                {
                    absolutePath = absolutePath.Substring(1);
                }
                if (absolutePath[absolutePath.Length - 1] != '\\')
                {
                    absolutePath = absolutePath + @"\";
                }

            }
            return System.IO.Path.Combine(driver, absolutePath);
        }

        /// <summary>合并两个路径  比如路径 User,2015 合并后为 /User/2015
        /// </summary>
        public static string CombineRelativePath(string basePath, string relativePath)
        {
            if (basePath.Length > 0)
            {
                if (basePath[0] != '/')
                {
                    basePath = @"/" + basePath;
                }
                if (basePath[basePath.Length - 1] == '/')
                {
                    basePath = basePath.Substring(0, basePath.Length - 1);
                }
            }
            if (relativePath.Length > 0 && relativePath[0] != '/')
            {
                relativePath = '/' + relativePath;
            }
            return basePath + relativePath;
        }

        /// <summary>获取某个Url链接中的全部参数
        /// </summary>
        public static Dictionary<string, string> GetAllParams(string url)
        {
            var paramUrl = url.IndexOf('?') > 0 ? url.Substring(url.IndexOf('?')) : "";
            //去除#后面的字符
            paramUrl = paramUrl.IndexOf('#') > 0 ? paramUrl.Remove(paramUrl.IndexOf('#')) : paramUrl;
            //判断?后面是否还有参数
            if (paramUrl.Length > 1 && paramUrl.StartsWith("?"))
            {
                paramUrl = paramUrl.Remove(0, 1);
            }
            var param = paramUrl.Split('&');
            return
                param.Select(item => item.Split('='))
                    .Where(dictItem => dictItem.Length == 2)
                    .ToDictionary(dictItem => dictItem[0], dictItem => dictItem[1]);
        }

        /// <summary>参数过滤,并且去除不合法的参数
        /// </summary>

        public static Dictionary<string, string> FilterParam(Dictionary<string, string> dicArrayPre)
        {
            return dicArrayPre.Where(temp => !string.IsNullOrEmpty(temp.Value))
                .ToDictionary(temp => temp.Key, temp => HttpUtility.HtmlEncode(temp.Value));
        }

        /// <summary>获取某链接除指定参数以外的新链接
        /// </summary>
        public static string GetUrlWithOutParam(string url, string key)
        {
            var paramDict = GetAllParams(url);
            paramDict = FilterParam(paramDict);
            var keyValue =
                paramDict.FirstOrDefault(x => string.Equals(x.Key, key, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrEmpty(keyValue.Key))
            {
                paramDict.Remove(keyValue.Key);
            }
            return AttachParamToUrl(GetBaseUrl(url), paramDict);
        }

        /// <summary>获取某链接除多个参数以外的新链接
        /// </summary>
        public static string GetUrlWithOutParams(string url, params string[] keys)
        {
            var paramDict = GetAllParams(url);
            paramDict = FilterParam(paramDict);
            var repeatDict = (from param in paramDict
                              from key in keys
                              where string.Equals(param.Key, key, StringComparison.CurrentCultureIgnoreCase)
                              select param).ToDictionary(param => param.Key, param => param.Value);
            foreach (var repeatItem in repeatDict)
            {
                paramDict.Remove(repeatItem.Key);
            }
            return AttachParamToUrl(GetBaseUrl(url), paramDict);
        }

        /// <summary>将新参数附加到旧链接上,形成新的地址
        /// </summary>
        public static string GetUrlWithIncreaseParam(string url, string key, string value)
        {
            var paramDict = GetAllParams(url);
            if (string.IsNullOrEmpty(paramDict.FirstOrDefault(x => string.Equals(x.Key, key, StringComparison.CurrentCultureIgnoreCase)).Key))
            {
                paramDict.Add(key, value);
            }
            return AttachParamToUrl(GetBaseUrl(url), paramDict);
        }

        /// <summary>将新的参数集合附加到旧链接上,形成新的地址
        /// </summary>
        public static string GetUrlWithIncreaseParams(string url, Dictionary<string, string> increaseDict)
        {
            var paramDict = GetAllParams(url);
            foreach (var increaseItem in increaseDict.Where(increaseItem => string.IsNullOrEmpty(
                paramDict.FirstOrDefault(
                    x => string.Equals(x.Key, increaseItem.Key, StringComparison.CurrentCultureIgnoreCase)).Key)))
            {
                paramDict.Add(increaseItem.Key, increaseItem.Value);
            }
            return AttachParamToUrl(GetBaseUrl(url), paramDict);
        }

        /// <summary>获取参数之前的Url地址
        /// </summary>
        public static string GetBaseUrl(string url)
        {
            //baseUrl，?问号之前的url地址
            var baseUrl = new Uri(url).GetLeftPart(UriPartial.Path);
            return baseUrl;
        }

        /// <summary>根据Url地址获取基础Url地址
        /// </summary>
        public static string GetDomainUrl(string url)
        {
            var uri = new Uri(url);
            return uri.GetLeftPart(UriPartial.Authority);
        }

        /// <summary>将参数附加到Url地址上
        /// </summary>
        private static string AttachParamToUrl(string baseUrl, Dictionary<string, string> paramDict)
        {
            var sb = new StringBuilder(baseUrl);
            var index = 0;
            foreach (var param in paramDict)
            {
                var combine = index == 0 ? "?" : "&";
                index++;
                sb.AppendFormat("{0}{1}={2}", combine, param.Key, param.Value);
            }
            return sb.ToString();
        }



        /// <summary>获取某个路径中文件的扩展名
        /// </summary>
        public static string GetPathExtension(string path)
        {
            string ext = "";
            if (!string.IsNullOrEmpty(path))
            {
                if (path.IndexOf('.') > 0)
                {
                    ext = path.Substring(path.LastIndexOf('.') + 1);
                }
            }
            return ext;
        }

        /// <summary>
        /// Get the web site application root path. Combine the request.Url.GetLeftPart(UriPartial.Authority) with request.ApplicationPath
        /// </summary>
        /// <returns>字符串（Url）</returns>
        public static string GetWebAppUrl()
        {
            var request = HttpContext.Current.Request;
            return CombineUrl(request.Url.GetLeftPart(UriPartial.Authority), request.ApplicationPath);
        }
    }
}
