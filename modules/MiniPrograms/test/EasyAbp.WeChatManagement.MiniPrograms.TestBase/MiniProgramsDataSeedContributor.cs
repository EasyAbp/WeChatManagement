using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public class MiniProgramsDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IWeChatAppRepository _weChatAppRepository;
        private readonly IGuidGenerator _guidGenerator;

        public MiniProgramsDataSeedContributor(
            IWeChatAppRepository weChatAppRepository,
            IGuidGenerator guidGenerator)
        {
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
                WeChatAppType.MiniProgram,
                null,
                "TestMiniProgram",
                "TestMiniProgram",
                MiniProgramsTestConsts.OpenAppIdOrName,
                MiniProgramsTestConsts.AppId,
                MiniProgramsTestConsts.AppSecret,
                MiniProgramsTestConsts.Token,
                MiniProgramsTestConsts.EncodingAesKey,
                null,
                false
            ), true);
        }
    }
}