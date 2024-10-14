using FishCRM.Infrastructure.Interfaces;
using FIshCRM.Domain.Entity;

namespace FishCRM.Infrastructure.Repositories
{
    public class FishBaseRepository : IBaseRepository<FishBase>
    {
        private readonly AppDbContext _appDbContext;

        public FishBaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Create(FishBase entity)
        {
            await _appDbContext.FishBases.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(FishBase entity)
        {
            _appDbContext.FishBases.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<FishBase> GetAll()
        {
            return _appDbContext.FishBases;
        }

        public async Task<FishBase> Update(FishBase entity)
        {
            _appDbContext.FishBases.Update(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }

    }
}

