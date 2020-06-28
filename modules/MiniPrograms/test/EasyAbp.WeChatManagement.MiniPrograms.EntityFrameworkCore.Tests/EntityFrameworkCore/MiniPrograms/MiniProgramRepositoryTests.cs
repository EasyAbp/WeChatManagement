using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.WeChatManagement.MiniPrograms.EntityFrameworkCore.MiniPrograms
{
    public class MiniProgramRepositoryTests : MiniProgramsEntityFrameworkCoreTestBase
    {
        private readonly IMiniProgramRepository _miniProgramRepository;

        public MiniProgramRepositoryTests()
        {
            _miniProgramRepository = GetRequiredService<IMiniProgramRepository>();
        }

        [Fact]
        public async Task Test1()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                // Arrange

                // Act

                //Assert
            });
        }
    }
}
