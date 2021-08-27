using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.Common.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.WeChatManagement.Common.WeChatApps
{
    public class WeChatAppRepository : EfCoreRepository<ICommonDbContext, WeChatApp, Guid>, IWeChatAppRepository
    {
        public WeChatAppRepository(IDbContextProvider<ICommonDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<WeChatApp> InsertAsync(WeChatApp entity, bool autoSave = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await CheckDuplicateAsync(entity, cancellationToken);

            return await base.InsertAsync(entity, autoSave, cancellationToken);
        }

        public override async Task<WeChatApp> UpdateAsync(WeChatApp entity, bool autoSave = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await CheckDuplicateAsync(entity, cancellationToken);
            
            return await base.UpdateAsync(entity, autoSave, cancellationToken);
        }

        public override async Task InsertManyAsync(IEnumerable<WeChatApp> entities, bool autoSave = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var weChatApps = entities.ToArray();

            await CheckDuplicateAsync(weChatApps, cancellationToken);

            await base.InsertManyAsync(weChatApps, autoSave, cancellationToken);
        }

        public override async Task UpdateManyAsync(IEnumerable<WeChatApp> entities, bool autoSave = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var weChatApps = entities.ToArray();

            await CheckDuplicateAsync(weChatApps, cancellationToken);
            
            await base.UpdateManyAsync(weChatApps, autoSave, cancellationToken);
        }

        protected virtual async Task CheckDuplicateAsync(WeChatApp entity, CancellationToken cancellationToken)
        {
            if (await (await GetDbSetAsync()).AnyAsync(
                x => (x.AppId == entity.AppId || x.Name == entity.Name) && x.Id != entity.Id,
                cancellationToken: cancellationToken))
            {
                throw new DuplicateWeChatAppException();
            }
        }

        protected virtual async Task CheckDuplicateAsync(WeChatApp[] entities, CancellationToken cancellationToken)
        {
            if (entities.Select(x => x.AppId).Distinct().Count() != entities.Length)
            {
                throw new DuplicateWeChatAppException();
            }
            
            if (entities.Select(x => x.Name).Distinct().Count() != entities.Length)
            {
                throw new DuplicateWeChatAppException();
            }
            
            foreach (var entity in entities)
            {
                var suspect = await (await GetDbSetAsync()).SingleOrDefaultAsync(
                    x => x.AppId == entity.AppId && x.Id != entity.Id,
                    cancellationToken: cancellationToken);

                var foundEntity = entities.FirstOrDefault(x => x.Id == suspect.Id);
                
                if (foundEntity == null || foundEntity.AppId == entity.AppId)
                {
                    throw new DuplicateWeChatAppException();
                }
            }
            
            foreach (var entity in entities)
            {
                var suspect = await (await GetDbSetAsync()).SingleOrDefaultAsync(
                    x => x.Name == entity.Name && x.Id != entity.Id,
                    cancellationToken: cancellationToken);

                var foundEntity = entities.FirstOrDefault(x => x.Id == suspect.Id);
                
                if (foundEntity == null || foundEntity.Name == entity.Name)
                {
                    throw new DuplicateWeChatAppException();
                }
            }
        }
    }
}