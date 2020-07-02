using System;
using EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers
{
    public interface IMiniProgramUserAppService :
        IReadOnlyAppService< 
            MiniProgramUserDto, 
            Guid, 
            PagedAndSortedResultRequestDto>
    {

    }
}