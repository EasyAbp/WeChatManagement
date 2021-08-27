using AutoMapper;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.Common.WeChatApps.Dtos;
using EasyAbp.WeChatManagement.Common.WeChatAppUsers;
using EasyAbp.WeChatManagement.Common.WeChatAppUsers.Dtos;

namespace EasyAbp.WeChatManagement.Common
{
    public class CommonApplicationAutoMapperProfile : Profile
    {
        public CommonApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            
            CreateMap<WeChatApp, WeChatAppDto>();
            CreateMap<CreateUpdateWeChatAppDto, WeChatApp>(MemberList.Source);
            CreateMap<WeChatAppUser, WeChatAppUserDto>();
        }
    }
}