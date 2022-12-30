using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Security.Encryption;

namespace EasyAbp.WeChatManagement.Officials
{
    public class OfficialsDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IStringEncryptionService _stringEncryptionService;
        private readonly IWeChatAppRepository _weChatAppRepository;
        private readonly IGuidGenerator _guidGenerator;

        public OfficialsDataSeedContributor(
            IStringEncryptionService stringEncryptionService,
            IWeChatAppRepository weChatAppRepository,
            IGuidGenerator guidGenerator)
        {
            _stringEncryptionService = stringEncryptionService;
            _weChatAppRepository = weChatAppRepository;
            _guidGenerator = guidGenerator;
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
                _stringEncryptionService.Encrypt(OfficialsTestConsts.AppSecret),
                _stringEncryptionService.Encrypt(OfficialsTestConsts.Token),
                _stringEncryptionService.Encrypt(OfficialsTestConsts.EncodingAesKey),
                false
            ), true);
        }
    }
}