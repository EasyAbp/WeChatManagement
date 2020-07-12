using Shouldly;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.Login;
using EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos;
using Xunit;

namespace EasyAbp.WeChatManagement.MiniPrograms
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
            var input = new LoginDto
            {
                AppId = "AppId",
                Code = "Code",
                EncryptedData = "EncryptedData",
                Iv = "Iv",
                RawData = "RawData",
                Signature = "Signature",
                UserInfo = new UserInfoModel
                {
                    NickName = "NickName",
                    Gender = 0,        
                    Language = "en",
                    City = "City",
                    Province = "Province",
                    Country = "Country",
                    AvatarUrl = "https://image.com/img.jpg"
                }
            };

            // Act
            var response = await _loginAppService.LoginAsync(input);

            // Assert
            response.ShouldNotBeNull();
            response.AccessToken.ShouldNotBeEmpty();
        }
    }
}
