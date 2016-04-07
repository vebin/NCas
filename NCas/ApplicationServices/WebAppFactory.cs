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
        private  List<WebAppInfoDto> _webAppDict = new List<WebAppInfoDto>();
        private readonly IWebAppQueryService _webAppQueryService;
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private readonly IScheduleService _scheduleService;
        public WebAppFactory()
        {

        }

        public WebAppFactory(IScheduleService scheduleService, IWebAppQueryService webAppQueryService)
        {
            _scheduleService = scheduleService;
            _webAppQueryService = webAppQueryService;
            LoadWebApp();
            _scheduleService.StartTask("LoadWebApp", LoadWebApp, 1000,
                1000*60*5);
        }


        /// <summary>根据客户端跳转过来的Url查询出客户端信息
        /// </summary>
        public WebAppInfoDto GetWebAppByUrl(string url)
        {
            return _webAppDict.FirstOrDefault(x => PathUtils.SameDomain(x.Url, url));
        }

        /// <summary>根据WebAppId获取WebApp信息
        /// </summary>
        public WebAppInfoDto GetWebAppById(string id)
        {
            return _webAppDict.FirstOrDefault(x => x.WebAppId == id);
        }

        private void LoadWebApp()
        {
            _webAppDict = _webAppQueryService.FindAll().ToList();
        }



    }
}
