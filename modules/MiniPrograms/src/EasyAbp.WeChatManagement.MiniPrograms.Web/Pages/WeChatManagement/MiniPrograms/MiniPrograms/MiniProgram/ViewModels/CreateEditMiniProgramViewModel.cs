using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.WeChatManagement.MiniPrograms.Web.Pages.WeChatManagement.MiniPrograms.MiniPrograms.MiniProgram.ViewModels
{
    public class CreateEditMiniProgramViewModel
    {
        [Placeholder("MiniProgramWeChatComponentIdPlaceHolder")]
        [Display(Name = "MiniProgramWeChatComponentId")]
        public Guid? WeChatComponentId { get; set; }
        
        [Required]
        [Display(Name = "MiniProgramName")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "MiniProgramDisplayName")]
        public string DisplayName { get; set; }

        [Required]
        [Display(Name = "MiniProgramOpenAppIdOrName")]
        public string OpenAppIdOrName { get; set; } = "Default";
        
        [Required]
        [Display(Name = "MiniProgramAppId")]
        public string AppId { get; set; }

        [Placeholder("MiniProgramAppSecretPlaceHolder")]
        [Display(Name = "MiniProgramAppSecret")]
        public string AppSecret { get; set; }

        [Display(Name = "MiniProgramToken")]
        public string Token { get; set; }

        [Display(Name = "MiniProgramEncodingAesKey")]
        public string EncodingAesKey { get; set; }
    }
}