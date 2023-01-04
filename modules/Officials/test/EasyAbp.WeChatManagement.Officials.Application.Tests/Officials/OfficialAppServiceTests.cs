using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.Official.Options;
using EasyAbp.Abp.WeChat.Official.Services.Login;
using EasyAbp.WeChatManagement.Officials.Login;
using EasyAbp.WeChatManagement.Officials.Login.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.WeChatManagement.Officials.Officials
{
    public class OfficialAppServiceTests : OfficialsApplicationTestBase
    {
        private readonly ILoginAppService _loginAppService;
        private IAbpWeChatServiceFactory _abpWeChatServiceFactory;

        public OfficialAppServiceTests()
        {
            _loginAppService = GetRequiredService<ILoginAppService>();
        }

        protected override void AfterAddApplication(IServiceCollection services)
        {
            base.AfterAddApplication(services);

            _abpWeChatServiceFactory = Substitute.For<IAbpWeChatServiceFactory>();
            services.Replace(ServiceDescriptor.Transient(s => _abpWeChatServiceFactory));

            _abpWeChatServiceFactory.CreateAsync<LoginWeService>("AppId").Returns(
                new FakeLoginWeService(new AbpWeChatOfficialOptions(), null));
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