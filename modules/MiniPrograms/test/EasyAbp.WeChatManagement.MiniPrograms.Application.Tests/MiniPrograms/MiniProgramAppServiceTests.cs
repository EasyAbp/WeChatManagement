using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.MiniProgram.Options;
using EasyAbp.Abp.WeChat.MiniProgram.Services.Login;
using EasyAbp.WeChatManagement.MiniPrograms.Login;
using EasyAbp.WeChatManagement.MiniPrograms.Login.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using Shouldly;
using Volo.Abp.DependencyInjection;
using Xunit;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms
{
    public class MiniProgramAppServiceTests : MiniProgramsApplicationTestBase
    {
        private readonly ILoginAppService _loginAppService;
        private IAbpWeChatServiceFactory _abpWeChatServiceFactory;

        public MiniProgramAppServiceTests()
        {
            _loginAppService = GetRequiredService<ILoginAppService>();
        }

        protected override void AfterAddApplication(IServiceCollection services)
        {
            base.AfterAddApplication(services);

            _abpWeChatServiceFactory = Substitute.For<IAbpWeChatServiceFactory>();
            services.Replace(ServiceDescriptor.Transient(s => _abpWeChatServiceFactory));

            _abpWeChatServiceFactory.CreateAsync<LoginWeService>("AppId").Returns(
                new FakeLoginWeService(new AbpWeChatMiniProgramOptions(), null));
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