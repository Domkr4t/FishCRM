using FishCRM.Infrastructure.Interfaces;
using FIshCRM.Domain.Entity;

namespace FishCRM.Infrastructure.Repositories
{
    public class FisherRepository : IBaseRepository<Fisher>
    {
        private readonly AppDbContext _appDbContext;

        public FisherRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Create(Fisher entity)
        {
            await _appDbContext.Fishers.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(Fisher entity)
        {
            _appDbContext.Fishers.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<Fisher> GetAll()
        {
            return _appDbContext.Fishers;
        }

        public async Task<Fisher> Update(Fisher entity)
        {
            _appDbContext.Fishers.Update(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }
    }
}
