using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.WeChatManagement.MiniPrograms.EntityFrameworkCore.UserInfos
{
    public class UserInfoRepositoryTests : MiniProgramsEntityFrameworkCoreTestBase
    {
        private readonly IUserInfoRepository _userInfoRepository;

        public UserInfoRepositoryTests()
        {
            _userInfoRepository = GetRequiredService<IUserInfoRepository>();
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
