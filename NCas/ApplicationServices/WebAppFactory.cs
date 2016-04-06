using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using ECommon.Scheduling;
using GUtils.Utilities;
using NCas.QueryServices;
using NCas.QueryServices.Dtos;

namespace NCas.ApplicationServices
{
    /// <summary>
    /// </summary>
    [Component]
    public class WebAppFactory
    {
        private  List<WebAppInfoDto> WebAppDict = new List<WebAppInfoDto>();
        private readonly IWebAppQueryService _webAppQueryService;
        private readonly IScheduleService _scheduleService;
        public WebAppFactory()
        {

        }

        public WebAppFactory(IScheduleService scheduleService, IWebAppQueryService webAppQueryService)
        {
            _scheduleService = scheduleService;
            _webAppQueryService = webAppQueryService;
            _scheduleService.StartTask("LoadWebApp", LoadWebApp, 1000,
                1000*60);
        }


        /// <summary>根据客户端跳转过来的Url查询出客户端信息
        /// </summary>
        public WebAppInfoDto GetWebAppByUrl(string url)
        {
            return WebAppDict.FirstOrDefault(x => PathUtils.SameDomain(x.Url, url));
        }

        /// <summary>根据WebAppId获取WebApp信息
        /// </summary>
        public WebAppInfoDto GetWebAppById(string id)
        {
            return WebAppDict.FirstOrDefault(x => x.WebAppId == id);
        }

        private void LoadWebApp()
        {
            WebAppDict = _webAppQueryService.FindAll().ToList();
        }



    }
}
