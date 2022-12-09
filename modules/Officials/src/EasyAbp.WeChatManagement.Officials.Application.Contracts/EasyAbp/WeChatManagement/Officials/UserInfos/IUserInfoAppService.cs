using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Officials.UserInfos.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.Officials.UserInfos
{
    public interface IUserInfoAppService :
        IReadOnlyAppService< 
            UserInfoDto, 
            Guid, 
            PagedAndSortedResultRequestDto>
    {
        Task<UserInfoDto> UpdateAsync(UserInfoModel input);
    }
}