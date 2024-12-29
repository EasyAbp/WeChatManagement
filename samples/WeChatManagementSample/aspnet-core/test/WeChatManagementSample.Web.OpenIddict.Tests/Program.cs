using Microsoft.AspNetCore.Builder;
using Volo.Abp.AspNetCore.TestBase;
using WeChatManagementSample;

var builder = WebApplication.CreateBuilder();

builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("WeChatManagementSample.Web.OpenIddict.csproj");
await builder.RunAbpModuleAsync<WeChatManagementSampleWebTestModule>(applicationName: "WeChatManagementSample.Web.OpenIddict" );

public partial class Program
{
}