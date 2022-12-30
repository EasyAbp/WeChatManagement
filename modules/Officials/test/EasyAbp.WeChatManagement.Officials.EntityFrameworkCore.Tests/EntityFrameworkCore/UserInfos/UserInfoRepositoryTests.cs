using System;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Officials.UserInfos;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.WeChatManagement.Officials.EntityFrameworkCore.UserInfos
{
    public class UserInfoRepositoryTests : OfficialsEntityFrameworkCoreTestBase
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
