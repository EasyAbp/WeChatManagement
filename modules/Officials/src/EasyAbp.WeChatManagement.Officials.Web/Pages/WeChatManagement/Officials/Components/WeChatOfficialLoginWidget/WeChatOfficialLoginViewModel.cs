using EasyAbp.WeChatManagement.Officials.Login.Dtos;

namespace EasyAbp.WeChatManagement.Officials.Web.Pages.WeChatManagement.Officials.Components.WeChatOfficialLoginWidget
{
    public class WeChatOfficialLoginViewModel : GetLoginAuthorizeUrlOutput
    {
        public string OfficialName { get; set; }
    }
}
