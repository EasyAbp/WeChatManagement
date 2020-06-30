using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.WeChatManagement.MiniPrograms.Web.Pages.WeChatManagement.MiniPrograms.MiniPrograms.MiniProgram.ViewModels
{
    public class CreateEditMiniProgramViewModel
    {
        [Display(Name = "MiniProgramWeChatComponentId")]
        public Guid? WeChatComponentId { get; set; }
        
        [Required]
        [Display(Name = "MiniProgramName")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "MiniProgramDisplayName")]
        public string DisplayName { get; set; }

        [Required]
        [DefaultValue("Default")]
        [Display(Name = "MiniProgramOpenAppIdOrName")]
        public string OpenAppIdOrName { get; set; }
        
        [Required]
        [Display(Name = "MiniProgramAppId")]
        public string AppId { get; set; }

        [Required]
        [Display(Name = "MiniProgramAppSecret")]
        public string AppSecret { get; set; }

        [Display(Name = "MiniProgramToken")]
        public string Token { get; set; }

        [Display(Name = "MiniProgramEncodingAesKey")]
        public string EncodingAesKey { get; set; }
    }
}