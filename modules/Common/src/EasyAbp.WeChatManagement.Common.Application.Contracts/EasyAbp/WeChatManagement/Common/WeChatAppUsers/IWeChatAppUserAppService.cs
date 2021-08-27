using System;
using EasyAbp.WeChatManagement.Common.WeChatAppUsers.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.Common.WeChatAppUsers
{
    public interface IWeChatAppUserAppService :
        IReadOnlyAppService< 
            WeChatAppUserDto, 
            Guid, 
            PagedAndSortedResultRequestDto>
    {

    }
}