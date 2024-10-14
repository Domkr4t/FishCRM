using FishCRM.Infrastructure.Interfaces;
using FIshCRM.Domain.Entity;

namespace FishCRM.Infrastructure.Repositories
{
    public class FishRepository : IBaseRepository<Fish>
    {
        private readonly AppDbContext _appDbContext;

        public FishRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Create(Fish entity)
        {
            await _appDbContext.Fishs.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(Fish entity)
        {
            _appDbContext.Fishs.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<Fish> GetAll()
        {
            return _appDbContext.Fishs;
        }

        public async Task<Fish> Update(Fish entity)
        {
            _appDbContext.Fishs.Update(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }
    }
}
