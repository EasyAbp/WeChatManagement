using System.ComponentModel.DataAnnotations;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos
{
    public class RefreshInput
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}