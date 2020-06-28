using System;

using System.ComponentModel.DataAnnotations;

namespace EasyAbp.WeChatManagement.MiniPrograms.Web.Pages.WeChatManagement.MiniPrograms.MiniPrograms.MiniProgram.ViewModels
{
    public class CreateEditMiniProgramViewModel
    {
        [Required]
        [Display(Name = "MiniProgramName")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "MiniProgramDisplayName")]
        public string DisplayName { get; set; }

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