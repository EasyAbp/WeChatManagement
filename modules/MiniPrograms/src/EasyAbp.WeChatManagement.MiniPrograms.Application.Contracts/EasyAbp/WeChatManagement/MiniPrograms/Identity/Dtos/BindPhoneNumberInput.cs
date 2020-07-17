using System.ComponentModel.DataAnnotations;

namespace EasyAbp.WeChatManagement.MiniPrograms.Identity.Dtos
{
    public class BindPhoneNumberInput
    {
        [Required]
        public string AppId { get; set; }
        
        [Required]
        public string Code { get; set; }

        [Required]
        public string EncryptedData { get; set; }

        [Required]
        public string Iv { get; set; }
    }
}