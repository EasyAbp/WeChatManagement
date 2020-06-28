using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms.Dtos;
using EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers;
using EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers.Dtos;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos.Dtos;
using AutoMapper;
using Volo.Abp.AutoMapper;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public class MiniProgramsApplicationAutoMapperProfile : Profile
    {
        public MiniProgramsApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<MiniProgram, MiniProgramDto>();
            CreateMap<CreateUpdateMiniProgramDto, MiniProgram>(MemberList.Source)
                .Ignore(x => x.IsStatic);
            CreateMap<MiniProgramUser, MiniProgramUserDto>();
            CreateMap<UserInfo, UserInfoDto>();
        }
    }
}
