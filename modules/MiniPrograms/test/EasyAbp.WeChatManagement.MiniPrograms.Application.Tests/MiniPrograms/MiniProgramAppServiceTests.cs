using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Login;
using EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos;
using Shouldly;
using Xunit;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms
{
    public class MiniProgramAppServiceTests : MiniProgramsApplicationTestBase
    {
        private readonly ILoginAppService _loginAppService;

        public MiniProgramAppServiceTests()
        {
            _loginAppService = GetRequiredService<ILoginAppService>();
        }

        [Fact]
        public async Task Request_Tokens_Should_Get_AccessToken()
        {
            // Arrange
            var input = new LoginInput
            {
                AppId = "AppId",
                Code = "Code"
            };

            // Act
            var result = await _loginAppService.LoginAsync(input);

            // Assert
            result.ShouldNotBeNull();
        }
    }
}
