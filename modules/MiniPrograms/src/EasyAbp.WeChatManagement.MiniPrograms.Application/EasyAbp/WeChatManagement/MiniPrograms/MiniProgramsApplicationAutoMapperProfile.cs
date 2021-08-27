using EasyAbp.WeChatManagement.MiniPrograms.UserInfos;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos.Dtos;
using AutoMapper;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public class MiniProgramsApplicationAutoMapperProfile : Profile
    {
        public MiniProgramsApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<UserInfo, UserInfoDto>();
        }
    }
}
