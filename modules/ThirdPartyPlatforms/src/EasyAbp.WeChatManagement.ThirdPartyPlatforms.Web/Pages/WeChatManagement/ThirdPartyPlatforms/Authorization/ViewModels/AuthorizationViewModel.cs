using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.Abp.TagHelperPlus.EasySelector;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.WeChatManagement.ThirdPartyPlatforms.Web.Pages.WeChatManagement.ThirdPartyPlatforms.Authorization.
    ViewModels;

/// <summary>
/// 参考：https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/Before_Develop/Authorization_Process_Technical_Description.html
/// </summary>
public class AuthorizationViewModel
{
    [EasySelector(
        getListedDataSourceUrl: "/api/wechat-management/common/wechat-app?type=ThirdPartyPlatform",
        getSingleDataSourceUrl: "/api/wechat-management/common/wechat-app/{id}",
        keyPropertyName: "id",
        textPropertyName: "displayName",
        alternativeTextPropertyName: "name",
        hideSubText: false,
        runScriptOnWindowLoad: true)]
    [Required]
    [Display(Name = "ThirdPartyPlatformWeChatAppId")]
    public Guid ThirdPartyPlatformWeChatAppId { get; set; }
    
    [Display(Name = "AuthorizerName")]
    [Required]
    [InputInfoText("AuthorizerNameInputInfoText")]
    public string AuthorizerName { get; set; }

    [Display(Name = "AllowOfficial")]
    public bool AllowOfficial { get; set; } = true;

    [Display(Name = "AllowMiniProgram")]
    public bool AllowMiniProgram { get; set; } = true;

    [Display(Name = "SpecifiedAppId")]
    public string SpecifiedAppId { get; set; }

    [Display(Name = "CategoryIds")]
    [InputInfoText("CategoryIdsInputInfoText")]
    public string CategoryIds { get; set; }
}