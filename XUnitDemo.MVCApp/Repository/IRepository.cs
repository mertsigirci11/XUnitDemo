namespace XUnitDemo.MVCApp.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
    }
}
