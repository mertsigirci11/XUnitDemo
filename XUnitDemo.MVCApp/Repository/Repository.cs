
using Microsoft.EntityFrameworkCore;
using XUnitDemo.MVCApp.Models;

namespace XUnitDemo.MVCApp.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private UdemyUnitTestDbContext _context;
        private DbSet<TEntity> _dbSet;

        public Repository(UdemyUnitTestDbContext udemyUnitTestDbContext)
        {
            _context = udemyUnitTestDbContext;
            _dbSet = _context.Set<TEntity>();
        }
        public async Task CreateAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync<TEntity>();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
