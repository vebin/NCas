using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUtils.Components;
using GUtils.Utilities;
using NCas.QueryServices;
using NCas.QueryServices.Dtos;

namespace NCas.ApplicationServices
{
    /// <summary>
    /// </summary>
    public class WebAppFactory
    {
        private static readonly List<WebAppInfoDto> WebAppDict;

        static WebAppFactory()
        {
            var appQueryService = ObjectContainer.Resolve<IWebAppQueryService>();
            WebAppDict = appQueryService.FindAll().ToList();
        }

        /// <summary>根据客户端跳转过来的Url查询出客户端信息
        /// </summary>
        public static WebAppInfoDto GetWebAppByUrl(string refererUrl)
        {
            return WebAppDict.FirstOrDefault(x => RequestUtils.SameDomain(x.Url, refererUrl));
        }

        /// <summary>根据WebAppId获取WebApp信息
        /// </summary>
        public static WebAppInfoDto GetWebAppById(string id)
        {
            return WebAppDict.FirstOrDefault(x => x.WebAppId == id);
        }



    }
}
