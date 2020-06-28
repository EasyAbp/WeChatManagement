using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.WeChatManagement.MiniPrograms.EntityFrameworkCore.MiniProgramUsers
{
    public class MiniProgramUserRepositoryTests : MiniProgramsEntityFrameworkCoreTestBase
    {
        private readonly IMiniProgramUserRepository _miniProgramUserRepository;

        public MiniProgramUserRepositoryTests()
        {
            _miniProgramUserRepository = GetRequiredService<IMiniProgramUserRepository>();
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
