using System.ComponentModel.DataAnnotations;

namespace EasyAbp.WeChatManagement.Officials.Identity.Dtos
{
    public class BindPhoneNumberInput
    {
        [Required]
        public string AppId { get; set; }
        
        [Required]
        public string Code { get; set; }
    }
}