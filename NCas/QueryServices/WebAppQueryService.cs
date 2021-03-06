﻿using System.Collections.Generic;
using System.Linq;
using ECommon.Components;
using ECommon.Dapper;
using NCas.Common;
using NCas.Common.Enums;
using NCas.QueryServices.Dtos;

namespace NCas.QueryServices
{
    /// <summary>网站节点查询
    /// </summary>
    [Component]
    public class WebAppQueryService : BaseQueryService, IWebAppQueryService
    {
        /// <summary>查询所有的节点
        /// </summary>
        public IEnumerable<WebAppInfoDto> FindAll()
        {
            using (var connection = GetConnection())
            {
                return connection.QueryList<WebAppInfoDto>(new
                {
                    UseFlag = (int) UseFlag.Useable
                }, ConfigSettings.WebAppTable);
            }
        }

        /// <summary>根据WebAppId查询WebApp信息
        /// </summary>
        public WebAppInfoDto FindById(string webAppId)
        {
            using (var connection=GetConnection())
            {
                return connection.QueryList<WebAppInfoDto>(new
                {
                    UseFlag=(int)UseFlag.Useable,
                    WebAppId=webAppId
                }, ConfigSettings.WebAppTable).FirstOrDefault();
            }
        }


        /// <summary>根据WebAppKey查询WebApp信息
        /// </summary>
        public WebAppInfoDto FindByKey(string webAppKey)
        {
            using (var connection=GetConnection())
            {
                return connection.QueryList<WebAppInfoDto>(new
                {
                    UseFlag = (int) UseFlag.Useable,
                    WebAppKey = webAppKey
                }, ConfigSettings.WebAppTable).FirstOrDefault();
            }
        }

    }
}
