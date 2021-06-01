using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.WeChatManagement.MiniPrograms.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.WeChatManagement.MiniPrograms.MiniPrograms
{
    public class MiniProgramRepository : EfCoreRepository<IMiniProgramsDbContext, MiniProgram, Guid>, IMiniProgramRepository
    {
        public MiniProgramRepository(IDbContextProvider<IMiniProgramsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<MiniProgram> InsertAsync(MiniProgram entity, bool autoSave = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await CheckDuplicateAsync(entity, cancellationToken);

            return await base.InsertAsync(entity, autoSave, cancellationToken);
        }

        public override async Task<MiniProgram> UpdateAsync(MiniProgram entity, bool autoSave = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await CheckDuplicateAsync(entity, cancellationToken);
            
            return await base.UpdateAsync(entity, autoSave, cancellationToken);
        }

        public override async Task InsertManyAsync(IEnumerable<MiniProgram> entities, bool autoSave = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var miniPrograms = entities.ToArray();

            await CheckDuplicateAsync(miniPrograms, cancellationToken);

            await base.InsertManyAsync(miniPrograms, autoSave, cancellationToken);
        }

        public override async Task UpdateManyAsync(IEnumerable<MiniProgram> entities, bool autoSave = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var miniPrograms = entities.ToArray();

            await CheckDuplicateAsync(miniPrograms, cancellationToken);
            
            await base.UpdateManyAsync(miniPrograms, autoSave, cancellationToken);
        }

        protected virtual async Task CheckDuplicateAsync(MiniProgram entity, CancellationToken cancellationToken)
        {
            if (await (await GetDbSetAsync()).AnyAsync(
                x => (x.AppId == entity.AppId || x.Name == entity.Name) && x.Id != entity.Id,
                cancellationToken: cancellationToken))
            {
                throw new DuplicateMiniProgramException();
            }
        }

        protected virtual async Task CheckDuplicateAsync(MiniProgram[] entities, CancellationToken cancellationToken)
        {
            if (entities.Select(x => x.AppId).Distinct().Count() != entities.Length)
            {
                throw new DuplicateMiniProgramException();
            }
            
            if (entities.Select(x => x.Name).Distinct().Count() != entities.Length)
            {
                throw new DuplicateMiniProgramException();
            }
            
            foreach (var entity in entities)
            {
                var suspect = await (await GetDbSetAsync()).SingleOrDefaultAsync(
                    x => x.AppId == entity.AppId && x.Id != entity.Id,
                    cancellationToken: cancellationToken);

                var foundEntity = entities.FirstOrDefault(x => x.Id == suspect.Id);
                
                if (foundEntity == null || foundEntity.AppId == entity.AppId)
                {
                    throw new DuplicateMiniProgramException();
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
                    throw new DuplicateMiniProgramException();
                }
            }
        }
    }
}