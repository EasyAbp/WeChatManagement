using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.WeChatManagement.Common.Web.Pages.WeChatManagement.Common.WeChatApps.WeChatApp.ViewModels
{
    public class CreateEditWeChatAppViewModel
    {
        [Display(Name = "WeChatAppType")]
        public WeChatAppType Type { get; set; }

        [Placeholder("WeChatAppWeChatComponentIdPlaceHolder")]
        [Display(Name = "WeChatAppWeChatComponentId")]
        public Guid? WeChatComponentId { get; set; }
        
        [Required]
        [Display(Name = "WeChatAppName")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "WeChatAppDisplayName")]
        public string DisplayName { get; set; }

        [Required]
        [Display(Name = "WeChatAppOpenAppIdOrName")]
        public string OpenAppIdOrName { get; set; } = "Default";
        
        [Required]
        [Display(Name = "WeChatAppAppId")]
        public string AppId { get; set; }

        [Placeholder("WeChatAppAppSecretPlaceHolder")]
        [Display(Name = "WeChatAppAppSecret")]
        public string AppSecret { get; set; }

        [Display(Name = "WeChatAppToken")]
        public string Token { get; set; }

        [Display(Name = "WeChatAppEncodingAesKey")]
        public string EncodingAesKey { get; set; }
    }
}