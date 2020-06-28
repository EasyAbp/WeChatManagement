using System;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms
{
    public interface IMiniProgramAppService :
        ICrudAppService< 
            MiniProgramDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateMiniProgramDto,
            CreateUpdateMiniProgramDto>
    {

    }
}