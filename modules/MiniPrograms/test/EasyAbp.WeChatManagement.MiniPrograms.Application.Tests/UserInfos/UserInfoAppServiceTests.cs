using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.WeChatManagement.MiniPrograms.UserInfos
{
    public class UserInfoAppServiceTests : MiniProgramsApplicationTestBase
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
