using ApiTest.Data.Model;

namespace ApiTest.InterFaces
{
    public interface IGenaricRepository<T> where T : BaseModel
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<int> AddAsync(T entity);   
        Task<int> UpdateAsync(T entity);   
        Task<int> DeleteAsync(T entity);   
    }
}
