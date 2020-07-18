using EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos;

namespace EasyAbp.WeChatManagement.MiniPrograms.Web.Pages.WeChatManagement.MiniPrograms.Components.WeChatMiniProgramPcLoginWidget
{
    public class WeChatMiniProgramPcLoginViewModel : GetPcLoginACodeOutput
    {
        public string MiniProgramName { get; set; }
    }
}