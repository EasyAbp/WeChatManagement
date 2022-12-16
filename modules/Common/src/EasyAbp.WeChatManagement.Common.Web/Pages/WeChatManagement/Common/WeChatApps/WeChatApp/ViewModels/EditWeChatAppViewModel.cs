using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.Abp.TagHelperPlus.EasySelector;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.WeChatManagement.Common.Web.Pages.WeChatManagement.Common.WeChatApps.WeChatApp.ViewModels
{
    public class EditWeChatAppViewModel
    {
        [EasySelector(
            getListedDataSourceUrl: "/api/wechat-management/common/wechat-app?type=ThirdPartyPlatform",
            getSingleDataSourceUrl: "/api/wechat-management/common/wechat-app/{id}",
            keyPropertyName: "id",
            textPropertyName: "displayName",
            alternativeTextPropertyName: "name",
            hideSubText: false)]
        [InputInfoText("WeChatAppComponentWeChatAppIdInputInfoText")]
        [Display(Name = "WeChatAppComponentWeChatAppId")]
        public Guid? ComponentWeChatAppId { get; set; }
        
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