using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.WeChatManagement.Officials.UserInfos
{
    public class UserInfoAppServiceTests : OfficialsApplicationTestBase
    {
        private readonly IUserInfoAppService _userInfoAppService;

        public UserInfoAppServiceTests()
        {
            _userInfoAppService = GetRequiredService<IUserInfoAppService>();
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
