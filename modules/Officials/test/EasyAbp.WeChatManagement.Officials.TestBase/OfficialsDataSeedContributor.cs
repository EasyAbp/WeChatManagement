using EasyAbp.WeChatManagement.Common.WeChatApps;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.WeChatManagement.Officials;

public class OfficialsDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly IWeChatAppRepository _weChatAppRepository;

    public OfficialsDataSeedContributor(
        IGuidGenerator guidGenerator, ICurrentTenant currentTenant, IWeChatAppRepository weChatAppRepository)
    {
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        _weChatAppRepository = weChatAppRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        /* Instead of returning the Task.CompletedTask, you can insert your test data
         * at this point!
         */

        await _weChatAppRepository.InsertAsync(new WeChatApp(
            _guidGenerator.Create(),
            null,
            WeChatAppType.Official,
            null,
            "TestOfficial",
            "TestOfficial",
            OfficialsTestConsts.OpenAppIdOrName,
            OfficialsTestConsts.AppId,
            OfficialsTestConsts.AppSecret,
            OfficialsTestConsts.Token,
            OfficialsTestConsts.EncodingAesKey,
            false
        ), true);
    }
}
