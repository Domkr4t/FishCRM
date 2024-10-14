namespace FishCRM.Infrastructure.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task Create(T entity);
        IQueryable<T> GetAll();
        Task Delete(T entity);
        Task<T> Update(T entity);
    }
}
