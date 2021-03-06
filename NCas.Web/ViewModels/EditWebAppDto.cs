﻿using System.ComponentModel.DataAnnotations;
using NCas.Commands.WebApps;

namespace NCas.Web.ViewModels
{
    public class EditWebAppDto
    {
        public string WebAppId { get; set; }

        /// <summary>应用名称
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "系统名称不能为空")]
        public string AppName { get; set; }

        /// <summary>Url地址
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "系统地址不能为空")]
        public string Url { get; set; }

        /// <summary>验证Ticket地址
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "验证Tickt地址不能为空")]
        public string VerifyTicketUrl { get; set; }

        /// <summary>设置账号Url
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "回写Account地址不能为空")]
        public string PutAccountUrl { get; set; }

        /// <summary>通知某账号登出地址
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "通知地址不能为空")]
        public string NotifyUrl { get; set; }

        public EditWebAppDto()
        {

        }

        public EditWebAppDto(string webAppId, string appName, string url, string verifyTicketUrl, string putAccountUrl,
            string notifyUrl)
        {
            WebAppId = webAppId;
            AppName = appName;
            Url = url;
            VerifyTicketUrl = verifyTicketUrl;
            PutAccountUrl = putAccountUrl;
            NotifyUrl = notifyUrl;
        }

        public UpdateWebApp ToCommand()
        {
            return new UpdateWebApp(WebAppId, AppName, Url, VerifyTicketUrl, PutAccountUrl, NotifyUrl);
        }

    }



}