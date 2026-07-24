using EasyAbp.WeChatManagement.MiniPrograms.UserInfos;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class UserInfoToUserInfoDtoMapper : MapperBase<UserInfo, UserInfoDto>
    {
        public override partial UserInfoDto Map(UserInfo source);

        public override partial void Map(UserInfo source, UserInfoDto destination);
    }
}
