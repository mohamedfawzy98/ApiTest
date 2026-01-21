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
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)await _dbcontext.Set<Employee>().Include(x => x.Department).ToListAsync();
            }
            else if (typeof(T) == typeof(Department))
            {
                return (IEnumerable<T>)await _dbcontext.Set<Department>().Include(x => x.Employees).ToListAsync();
            }
            return await _dbcontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if (typeof(T) == typeof(Employee))
            {
                return await _dbcontext.Set<Employee>().Include(x => x.Department).FirstOrDefaultAsync(x => x.Id == id) as T;
            }
            else if (typeof(T) == typeof(Department))
            {
                return await _dbcontext.Set<Department>().Include(x => x.Employees).FirstOrDefaultAsync(x => x.Id == id) as T;
            }
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
