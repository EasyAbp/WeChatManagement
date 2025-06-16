using EasyAbp.Abp.WeChat.MiniProgram.Services.PhoneNumber;
using EasyAbp.WeChatManagement.Common.WeChatApps;

namespace EasyAbp.WeChatManagement.MiniPrograms.Login
{
    public class LoginByPhoneResultInfoModel
    {
        public WeChatApp MiniProgram { get; init; }

        public string LoginProvider { get; init; }

        public string ProviderKey { get; init; }

        public string PhoneNumber { get; init; }

        public GetPhoneNumberResponse GetPhoneNumberResponse { get; init; }
    }
}
