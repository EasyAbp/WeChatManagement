using System;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.MiniPrograms.UserInfos
{
    public interface IUserInfoAppService :
        IReadOnlyAppService< 
            UserInfoDto, 
            Guid, 
            PagedAndSortedResultRequestDto>
    {

    }
}