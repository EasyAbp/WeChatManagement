using EasyAbp.WeChatManagement.Common.Web.Pages.WeChatManagement.Common.WeChatApps.WeChatApp.ViewModels;
using EasyAbp.WeChatManagement.Common.WeChatApps.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.WeChatManagement.Common.Web
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class WeChatAppDtoToEditWeChatAppViewModelMapper : MapperBase<WeChatAppDto, EditWeChatAppViewModel>
    {
        public override partial EditWeChatAppViewModel Map(WeChatAppDto source);

        public override partial void Map(WeChatAppDto source, EditWeChatAppViewModel destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CreateWeChatAppViewModelToCreateWeChatAppDtoMapper : MapperBase<CreateWeChatAppViewModel, CreateWeChatAppDto>
    {
        public override partial CreateWeChatAppDto Map(CreateWeChatAppViewModel source);

        public override partial void Map(CreateWeChatAppViewModel source, CreateWeChatAppDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class EditWeChatAppViewModelToUpdateWeChatAppDtoMapper : MapperBase<EditWeChatAppViewModel, UpdateWeChatAppDto>
    {
        public override partial UpdateWeChatAppDto Map(EditWeChatAppViewModel source);

        public override partial void Map(EditWeChatAppViewModel source, UpdateWeChatAppDto destination);
    }
}
