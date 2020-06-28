﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WeChatManagementSample.Data;
using Volo.Abp.DependencyInjection;

namespace WeChatManagementSample.EntityFrameworkCore
{
    public class EntityFrameworkCoreWeChatManagementSampleDbSchemaMigrator
        : IWeChatManagementSampleDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreWeChatManagementSampleDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the WeChatManagementSampleMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<WeChatManagementSampleMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}