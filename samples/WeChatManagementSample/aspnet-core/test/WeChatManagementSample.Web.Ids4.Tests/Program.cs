using Microsoft.AspNetCore.Builder;
using Volo.Abp.AspNetCore.TestBase;
using WeChatManagementSample;

var builder = WebApplication.CreateBuilder();

builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("WeChatManagementSample.Web.Ids4.csproj");
await builder.RunAbpModuleAsync<WeChatManagementSampleWebTestModule>(applicationName: "WeChatManagementSample.Web.Ids4" );

public partial class Program
{
}