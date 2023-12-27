using Microsoft.AspNetCore.Builder;
using Volo.Abp.AspNetCore.TestBase;
using WeChatManagementSample;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<WeChatManagementSampleWebTestModule>();

public partial class Program
{
}