using System;
using EasyAbp.WeChatManagement.Common.WeChatApps.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.WeChatManagement.Common.WeChatApps
{
    public interface IWeChatAppAppService :
        ICrudAppService< 
            WeChatAppDto, 
            Guid, 
            WeChatAppGetListInput,
            CreateWeChatAppDto,
            UpdateWeChatAppDto>
    {

    }
}