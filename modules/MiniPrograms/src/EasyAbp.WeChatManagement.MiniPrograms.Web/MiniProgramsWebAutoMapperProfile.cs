using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms.Dtos;
using EasyAbp.WeChatManagement.MiniPrograms.Web.Pages.WeChatManagement.MiniPrograms.MiniPrograms.MiniProgram.ViewModels;
using AutoMapper;

namespace EasyAbp.WeChatManagement.MiniPrograms.Web
{
    public class MiniProgramsWebAutoMapperProfile : Profile
    {
        public MiniProgramsWebAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<MiniProgramDto, CreateEditMiniProgramViewModel>();
            CreateMap<CreateEditMiniProgramViewModel, CreateUpdateMiniProgramDto>();
        }
    }
}
