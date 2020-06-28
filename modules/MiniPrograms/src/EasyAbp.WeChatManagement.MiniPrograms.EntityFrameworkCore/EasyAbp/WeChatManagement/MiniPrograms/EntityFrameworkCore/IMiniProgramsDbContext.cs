using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms;
using EasyAbp.WeChatManagement.MiniPrograms.MiniProgramUsers;
using EasyAbp.WeChatManagement.MiniPrograms.UserInfos;

namespace EasyAbp.WeChatManagement.MiniPrograms.EntityFrameworkCore
{
    [ConnectionStringName(MiniProgramsDbProperties.ConnectionStringName)]
    public interface IMiniProgramsDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
        DbSet<MiniProgram> MiniPrograms { get; set; }
        DbSet<MiniProgramUser> MiniProgramUsers { get; set; }
        DbSet<UserInfo> UserInfos { get; set; }
    }
}
