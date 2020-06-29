namespace EasyAbp.WeChatManagement.MiniPrograms.UserInfos
{
    public class UserInfoModel : IUserInfo
    {
        public string NickName { get; set; }

        public byte Gender { get; set; }
        
        public string Language { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string Country { get; set; }

        public string AvatarUrl { get; set; }
    }
}