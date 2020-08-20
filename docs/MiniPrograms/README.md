# WeChatManagement.MiniPrograms

[![小程序模块](https://img.shields.io/nuget/v/EasyAbp.WeChatManagement.MiniPrograms.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.WeChatManagement.MiniPrograms.Domain.Shared)
[![下载量](https://img.shields.io/nuget/dt/EasyAbp.WeChatManagement.MiniPrograms.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.WeChatManagement.MiniPrograms.Domain.Shared)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/WeChatManagement?style=social)](https://www.github.com/EasyAbp/WeChatManagement)

Abp 小程序管理模块，提供小程序登录、用户个人信息记录、小程序微信服务器等功能，自动适应微信开放平台规则，与微信第三方平台模块轻松衔接。

## Online Demo

We have launched an online demo for this module: [https://pm.samples.easyabp.io](https://pm.samples.easyabp.io)

## Installation

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/How-To.md#add-nuget-packages))

    * EasyAbp.WeChatManagement.MiniPrograms.Application
    * EasyAbp.WeChatManagement.MiniPrograms.Application.Contracts
    * EasyAbp.WeChatManagement.MiniPrograms.Domain
    * EasyAbp.WeChatManagement.MiniPrograms.Domain.Shared
    * EasyAbp.WeChatManagement.MiniPrograms.EntityFrameworkCore
    * EasyAbp.WeChatManagement.MiniPrograms.HttpApi
    * EasyAbp.WeChatManagement.MiniPrograms.HttpApi.Client
    * (Optional) EasyAbp.WeChatManagement.MiniPrograms.MongoDB
    * (Optional) EasyAbp.WeChatManagement.MiniPrograms.Web

1. Add `DependsOn(typeof(WeChatManagementMiniProgramsXxxModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/How-To.md#add-module-dependencies))

1. Add `builder.ConfigureWeChatManagementMiniPrograms();` to the `OnModelCreating()` method in **MyProjectMigrationsDbContext.cs**.

1. Add EF Core migrations and update your database. See: [ABP document](https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=MVC#add-new-migration-update-the-database).

1. 在 Web / HttpApi.Host 启动项目的 appsettings.json 的 AuthServer 中增加 `ClientId` 和 `ClientSecret` 配置（可使用文件中 IdentityServer 中的配置）。

1. 在 IdentityServerClientGrantTypes 表中给上一步使用的 Client 增加一条 `WeChatMiniProgram_credentials` 的 GrantType.

## Usage

### 小程序登录

1. 使用 `/api/weChatManagement/miniPrograms/login/login` 接口进行微信登录，留意 [LoginInput](https://github.com/EasyAbp/WeChatManagement/blob/master/modules/MiniPrograms/src/EasyAbp.WeChatManagement.MiniPrograms.Application.Contracts/EasyAbp/WeChatManagement/MiniPrograms/Login/Dtos/LoginInput.cs) 的注释说明。
    
2. 使用 `/api/weChatManagement/miniPrograms/login/refresh` 接口对 AccessToken 续期。

### 小程序授权 Razor 页面登录

1. 配置用于微信登录的小程序的 Name，默认为`Default`，参考[本模块设置](https://github.com/EasyAbp/WeChatManagement/blob/master/modules/MiniPrograms/src/EasyAbp.WeChatManagement.MiniPrograms.Domain/EasyAbp/WeChatManagement/MiniPrograms/Settings/MiniProgramsSettings.cs)。

2. 重写登录页，在页面中插入 [WeChatMiniProgramPcLoginWidget](https://github.com/EasyAbp/WeChatManagement/blob/master/modules/MiniPrograms/src/EasyAbp.WeChatManagement.MiniPrograms.Web/Pages/WeChatManagement/MiniPrograms/Components/WeChatMiniProgramPcLoginWidget/WeChatMiniProgramPcLoginWidgetViewComponent.cs)，重写方法参考 [官方文档](https://docs.abp.io/en/abp/latest/How-To/Customize-Login-Page-MVC) 和 [本模块示例](https://github.com/EasyAbp/WeChatManagement/blob/master/samples/WeChatManagementSample/aspnet-core/src/WeChatManagementSample.Web/Pages/Account)。

3. 微信扫码后（默认配置下，会打开小程序首页），确保小程序本身已完成用户登录，小程序将获得的 [scene](https://developers.weixin.qq.com/miniprogram/dev/api-backend/open-api/qr-code/wxacode.getUnlimited.html) 作为 token 参数传入 `/api/weChatManagement/miniPrograms/login/authorizePc` 接口。

4. 此时，Razor 登录页将自动完成登录并跳转。

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
