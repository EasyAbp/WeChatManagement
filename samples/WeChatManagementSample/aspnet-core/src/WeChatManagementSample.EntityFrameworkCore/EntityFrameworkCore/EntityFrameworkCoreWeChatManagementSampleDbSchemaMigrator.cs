﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using WeChatManagementSample.Data;

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
            /* We intentionally resolving the WeChatManagementSampleDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<WeChatManagementSampleDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
