using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;

namespace EasyAbp.WeChatManagement.MiniPrograms
{
    public class MiniProgramsDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IMiniProgramRepository _miniProgramRepository;
        private readonly IGuidGenerator _guidGenerator;

        public MiniProgramsDataSeedContributor(
            IMiniProgramRepository miniProgramRepository,
            IGuidGenerator guidGenerator)
        {
            _miniProgramRepository = miniProgramRepository;
            _guidGenerator = guidGenerator;
        }
        
        public async Task SeedAsync(DataSeedContext context)
        {
            /* Instead of returning the Task.CompletedTask, you can insert your test data
             * at this point!
             */

            await _miniProgramRepository.InsertAsync(new MiniProgram(
                _guidGenerator.Create(),
                null,
                null,
                "TestMiniProgram",
                "TestMiniProgram",
                MiniProgramsTestConsts.OpenAppId,
                MiniProgramsTestConsts.AppId,
                MiniProgramsTestConsts.AppSecret,
                MiniProgramsTestConsts.Token,
                MiniProgramsTestConsts.EncodingAesKey,
                false
            ), true);
        }
    }
}