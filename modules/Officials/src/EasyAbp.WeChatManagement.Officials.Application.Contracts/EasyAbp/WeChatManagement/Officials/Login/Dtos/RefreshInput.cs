using System.ComponentModel.DataAnnotations;

namespace EasyAbp.WeChatManagement.Officials.Login.Dtos
{
    public class RefreshInput
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}