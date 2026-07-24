using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.Common.WeChatApps.Dtos;
using EasyAbp.WeChatManagement.Common.WeChatAppUsers;
using EasyAbp.WeChatManagement.Common.WeChatAppUsers.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.WeChatManagement.Common
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class WeChatAppToWeChatAppDtoMapper : MapperBase<WeChatApp, WeChatAppDto>
    {
        // The secret fields are intentionally not exposed on the output DTO.
        [MapperIgnoreTarget(nameof(WeChatAppDto.AppSecret))]
        [MapperIgnoreTarget(nameof(WeChatAppDto.Token))]
        [MapperIgnoreTarget(nameof(WeChatAppDto.EncodingAesKey))]
        public override partial WeChatAppDto Map(WeChatApp source);

        [MapperIgnoreTarget(nameof(WeChatAppDto.AppSecret))]
        [MapperIgnoreTarget(nameof(WeChatAppDto.Token))]
        [MapperIgnoreTarget(nameof(WeChatAppDto.EncodingAesKey))]
        public override partial void Map(WeChatApp source, WeChatAppDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class WeChatAppUserToWeChatAppUserDtoMapper : MapperBase<WeChatAppUser, WeChatAppUserDto>
    {
        public override partial WeChatAppUserDto Map(WeChatAppUser source);

        public override partial void Map(WeChatAppUser source, WeChatAppUserDto destination);
    }
}
