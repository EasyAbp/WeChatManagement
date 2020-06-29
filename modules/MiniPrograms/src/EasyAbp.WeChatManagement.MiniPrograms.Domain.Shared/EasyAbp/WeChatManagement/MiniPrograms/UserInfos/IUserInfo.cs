namespace EasyAbp.WeChatManagement.MiniPrograms.UserInfos
{
    public interface IUserInfo
    {
        string NickName { get; }

        byte Gender { get; }
        
        string Language { get; }

        string City { get; }

        string Province { get; }

        string Country { get; }

        string AvatarUrl { get; }
    }
}