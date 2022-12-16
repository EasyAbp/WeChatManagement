using AutoMapper;
using EasyAbp.WeChatManagement.Common.Web.Pages.WeChatManagement.Common.WeChatApps.WeChatApp.ViewModels;
using EasyAbp.WeChatManagement.Common.WeChatApps.Dtos;

namespace EasyAbp.WeChatManagement.Common.Web
{
    public class CommonWebAutoMapperProfile : Profile
    {
        public CommonWebAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<WeChatAppDto, EditWeChatAppViewModel>();
            CreateMap<CreateWeChatAppViewModel, CreateWeChatAppDto>();
            CreateMap<EditWeChatAppViewModel, UpdateWeChatAppDto>();
        }
    }
}