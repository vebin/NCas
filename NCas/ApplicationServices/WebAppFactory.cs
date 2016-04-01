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
        private static readonly List<WebAppInfoDto> _webAppDict = new List<WebAppInfoDto>();
        private static readonly IWebAppQueryService AppQueryService;

        static WebAppFactory()
        {
            AppQueryService = ObjectContainer.Resolve<IWebAppQueryService>();
            _webAppDict = AppQueryService.FindAll().ToList();
        }

        /// <summary>根据客户端跳转过来的Url查询出客户端信息
        /// </summary>
        public static WebAppInfoDto GetWebAppByUrl(string refererUrl)
        {
            return _webAppDict.FirstOrDefault(x => RequestUtils.SameDomain(x.Url, refererUrl));
        }



    }
}
