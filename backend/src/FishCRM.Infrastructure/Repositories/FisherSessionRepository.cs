using FishCRM.Infrastructure.Interfaces;
using FIshCRM.Domain.Entity;

namespace FishCRM.Infrastructure.Repositories
{
    public class FisherSessionRepository : IBaseRepository<FisherSession>
    {
        private readonly AppDbContext _appDbContext;

        public FisherSessionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Create(FisherSession entity)
        {
            await _appDbContext.FisherSessions.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task Delete(FisherSession entity)
        {
            _appDbContext.FisherSessions.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<FisherSession> GetAll()
        {
            return _appDbContext.FisherSessions;
        }

        public async Task<FisherSession> Update(FisherSession entity)
        {
            _appDbContext.FisherSessions.Update(entity);
            await _appDbContext.SaveChangesAsync();

            return entity;
        }
    }
}
