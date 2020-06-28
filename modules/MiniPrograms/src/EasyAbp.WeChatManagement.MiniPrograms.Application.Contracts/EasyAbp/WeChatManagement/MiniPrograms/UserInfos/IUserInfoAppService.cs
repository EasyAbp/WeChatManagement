using System;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.MiniPrograms.UserInfos
{
    public interface IUserInfoAppService :
        ICrudAppService< 
            UserInfoDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            object,
            object>
    {

    }
}