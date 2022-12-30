using EasyAbp.WeChatManagement.Officials.UserInfos;
using EasyAbp.WeChatManagement.Officials.UserInfos.Dtos;
using AutoMapper;

namespace EasyAbp.WeChatManagement.Officials
{
    public class OfficialsApplicationAutoMapperProfile : Profile
    {
        public OfficialsApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<UserInfo, UserInfoDto>();
        }
    }
}
