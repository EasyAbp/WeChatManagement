using System;
using EasyAbp.WeChatManagement.Common;
using EasyAbp.WeChatManagement.Common.WeChatApps;
using EasyAbp.WeChatManagement.Common.WeChatApps.Dtos;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace WeChatManagementSample.Common
{
    /* Verifies the Mapperly-based entity -> DTO mapping registered by the
     * WeChatManagementCommonApplicationModule. */
    public class WeChatAppMappingTests : WeChatManagementSampleApplicationTestBase
    {
        private readonly IObjectMapper<WeChatManagementCommonApplicationModule> _objectMapper;

        public WeChatAppMappingTests()
        {
            _objectMapper = GetRequiredService<IObjectMapper<WeChatManagementCommonApplicationModule>>();
        }

        [Fact]
        public void Should_Map_WeChatApp_To_WeChatAppDto_And_Ignore_Secret_Fields()
        {
            // Arrange
            var app = new WeChatApp(
                id: Guid.NewGuid(),
                tenantId: null,
                type: WeChatAppType.MiniProgram,
                componentWeChatAppId: null,
                name: "my-app",
                displayName: "My App",
                openAppIdOrName: "Default",
                appId: "wx1234567890",
                encryptedAppSecret: "encrypted-secret",
                encryptedToken: "encrypted-token",
                encryptedEncodingAesKey: "encrypted-aes-key",
                isStatic: true);

            // Act
            var dto = _objectMapper.Map<WeChatApp, WeChatAppDto>(app);

            // Assert
            dto.Id.ShouldBe(app.Id);
            dto.Type.ShouldBe(WeChatAppType.MiniProgram);
            dto.Name.ShouldBe("my-app");
            dto.DisplayName.ShouldBe("My App");
            dto.AppId.ShouldBe("wx1234567890");
            dto.IsStatic.ShouldBeTrue();

            // The secret fields must not be exposed on the output DTO.
            dto.AppSecret.ShouldBeNull();
            dto.Token.ShouldBeNull();
            dto.EncodingAesKey.ShouldBeNull();
        }
    }
}
