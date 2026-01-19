using ApiTest.Data.Context;
using ApiTest.Data.Model;
using ApiTest.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Presntisses.Repository
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseModel
    {
        private readonly ApplicationContext _dbcontext;

        public GenaricRepository(ApplicationContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbcontext.Set<T>().FindAsync(id);
        }

        public async Task<int> AddAsync(T entity)
        {
            await _dbcontext.Set<T>().AddAsync(entity);
            return await _dbcontext.SaveChangesAsync();
        }
        public async Task<int> UpdateAsync(T entity)
        {
            _dbcontext.Set<T>().Update(entity);
            return await _dbcontext.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(T entity)
        {
            _dbcontext.Set<T>().Remove(entity);

            return await _dbcontext.SaveChangesAsync();
        }
    }
}
