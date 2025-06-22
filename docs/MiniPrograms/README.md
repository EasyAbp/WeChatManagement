# WeChatManagement.MiniPrograms

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FWeChatManagement%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![小程序模块](https://img.shields.io/nuget/v/EasyAbp.WeChatManagement.MiniPrograms.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.WeChatManagement.MiniPrograms.Domain.Shared)
[![下载量](https://img.shields.io/nuget/dt/EasyAbp.WeChatManagement.MiniPrograms.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.WeChatManagement.MiniPrograms.Domain.Shared)
[![Discord online](https://badgen.net/discord/online-members/xyg8TrRa27?label=Discord)](https://discord.gg/xyg8TrRa27)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/WeChatManagement?style=social)](https://www.github.com/EasyAbp/WeChatManagement)

Abp 小程序管理模块，提供小程序登录、用户个人信息记录、小程序微信服务器等功能，自动适应微信开放平台规则，与微信第三方平台模块轻松衔接。

## Installation

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

    * EasyAbp.WeChatManagement.MiniPrograms.Application
    * EasyAbp.WeChatManagement.MiniPrograms.Application.Contracts
    * (2选1) EasyAbp.WeChatManagement.MiniPrograms.Domain.OpenIddict
    * (2选1) EasyAbp.WeChatManagement.MiniPrograms.Domain.Ids4
    * EasyAbp.WeChatManagement.MiniPrograms.Domain.Shared
    * EasyAbp.WeChatManagement.MiniPrograms.EntityFrameworkCore
    * EasyAbp.WeChatManagement.MiniPrograms.HttpApi
    * EasyAbp.WeChatManagement.MiniPrograms.HttpApi.Client
    * (Optional) EasyAbp.WeChatManagement.MiniPrograms.MongoDB
    * (Optional) EasyAbp.WeChatManagement.MiniPrograms.Web
    * (Optional) EasyAbp.Abp.WeChat.Common.SharedCache.StackExchangeRedis (**重要！如果开发/沙盒/线上均使用了相同的微信AppId，请安装此模块，使用中立缓存共享 AccessToken: https://github.com/EasyAbp/WeChatManagement/issues/15#issuecomment-769718739**)

1. Add `DependsOn(typeof(WeChatManagementMiniProgramsXxxModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-module-dependencies))

1. Add `builder.ConfigureWeChatManagementCommon();` and `builder.ConfigureWeChatManagementMiniPrograms();` to the `OnModelCreating()` method in **MyProjectMigrationsDbContext.cs**.

1. Add EF Core migrations and update your database. See: [ABP document](https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=MVC&DB=EF#add-database-migration).

1. 在 Web/Host 项目的 appsettings.json 中增加微信登录授权服务器配置：
   ```json
   {
     "WeChatManagement": {
       "MiniPrograms": {
         "AuthServer": {
           "Authority": "https://localhost:44380",
           "ClientId": "MyProjectName_WeChatMiniProgram",
           "ClientSecret": "1q2w3e*"
         }
       }
     }
   }
   ```

1. 在 OpenIddictDataSeedContributor 中增加新的客户端 Data Seed (你也可以使用 [IDS4](https://github.com/EasyAbp/WeChatManagement/blob/master/samples/WeChatManagementSample/aspnet-core/src/WeChatManagementSample.Domain/IdentityServer/IdentityServerDataSeedContributor.cs))：
    ```CSharp
    // WeChat MiniProgram
    var weChatMiniProgramClientId =
        configurationSection["MyProjectName_WeChatMiniProgram:ClientId"];
    if (!weChatMiniProgramClientId.IsNullOrWhiteSpace())
    {
        await CreateApplicationAsync(
            name: weChatMiniProgramClientId,
            type: OpenIddictConstants.ClientTypes.Confidential,
            consentType: OpenIddictConstants.ConsentTypes.Implicit,
            displayName: "WeChat Mini-program",
            secret: configurationSection["MyProjectName_WeChatMiniProgram:ClientSecret"] ?? "1q2w3e*",
            grantTypes: new List<string>
            {
                OpenIddictConstants.GrantTypes.RefreshToken,
                WeChatMiniProgramConsts.GrantType
            },
            scopes: commonScopes
        );
    }
    ```
1. 在 OpenIddictDataSeedContributor 中找到
    ```CSharp
    if (!buildInGrantTypes.Contains(grantType))
    {
        application.Permissions.Add(OpenIddictConstants.Permissions.Prefixes.GrantType + grantType);
    }
    ```
    在下面增加
    ```CSharp
    if (grantType == WeChatMiniProgramConsts.GrantType)
    {
        application.Permissions.Add(OpenIddictConstants.Permissions.Endpoints.Token);
    }
    ```

1. 在 DbMigrator 项目的 appsettings.json 中增加：
    ```CSharp
    {
      "OpenIddict": {
        "Applications": {
          "WeChatManagementSample_WeChatMiniProgram": {
            "ClientId": "MyProjectName_WeChatMiniProgram",
            "ClientSecret": "1q2w3e*"
          }
        }
      }
    }
    ```

1. 运行 DbMigrator 项目，以创建新的授权客户端。

## Usage

### 小程序登录

1. 使用 `/api/wechat-management/mini-programs/login/login` (POST) 接口进行微信登录，留意 [LoginInput](https://github.com/EasyAbp/WeChatManagement/blob/master/modules/MiniPrograms/src/EasyAbp.WeChatManagement.MiniPrograms.Application.Contracts/EasyAbp/WeChatManagement/MiniPrograms/Login/Dtos/LoginInput.cs) 的注释说明。
   - 通过 `PhoneNumberCode` 参数支持手机号码查找用户：当通过 openId/unionId 找不到用户时，如果提供了此参数，系统将尝试使用手机号查找匹配用户。
    
2. 使用 `/api/wechat-management/mini-programs/login/refresh` (POST) 接口对 AccessToken 续期。

3. 在有需要时，使用 `/api/wechat-management/mini-programs/user-info` (PUT) 接口对存储的微信用户信息进行更新。(见 https://github.com/EasyAbp/WeChatManagement/issues/20)

### 小程序授权 Razor 页面登录

1. 配置用于微信登录的小程序的 Name，默认为`Default`，参考[本模块设置](https://github.com/EasyAbp/WeChatManagement/blob/master/modules/MiniPrograms/src/EasyAbp.WeChatManagement.MiniPrograms.Domain/EasyAbp/WeChatManagement/MiniPrograms/Settings/MiniProgramsSettings.cs)。

2. 重写登录页，在页面中插入 [WeChatMiniProgramPcLoginWidget](https://github.com/EasyAbp/WeChatManagement/blob/master/modules/MiniPrograms/src/EasyAbp.WeChatManagement.MiniPrograms.Web/Pages/WeChatManagement/MiniPrograms/Components/WeChatMiniProgramPcLoginWidget/WeChatMiniProgramPcLoginWidgetViewComponent.cs)，重写方法参考 [官方文档](https://docs.abp.io/en/abp/latest/How-To/Customize-Login-Page-MVC) 和 [本模块示例](https://github.com/EasyAbp/WeChatManagement/blob/master/samples/WeChatManagementSample/aspnet-core/src/WeChatManagementSample.Web.Ids4/Pages/Account)。

3. 微信扫码后（默认配置下，会打开小程序首页），确保小程序本身已完成用户登录，小程序需要将扫码获得的 [scene](https://developers.weixin.qq.com/miniprogram/dev/api-backend/open-api/qr-code/wxacode.getUnlimited.html) 作为 token 参数传入 `/api/wechat-management/mini-programs/login/authorize-pc` 接口。

4. 完成上一步后，Razor 登录页将自动完成登录并跳转。

![MiniProgram](/docs/MiniPrograms/images/MiniProgram.png)
![MiniProgramUser](/docs/MiniPrograms/images/MiniProgramUser.png)
![UserInfo](/docs/MiniPrograms/images/UserInfo.png)
![PcLogin](/docs/MiniPrograms/images/PcLogin.png)

## Roadmap

- [ ] 微信服务器
- [ ] 旧账号关联微信登录
- [x] 微信授权 Razor 页面登录
- [ ] 对接第三方平台模块
- [ ] 单元测试
