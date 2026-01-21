using ApiTest.Data.Dto;
using ApiTest.Data.Model;

namespace ApiTest.InterFaces
{
    public interface IEmployeeServices
    {
        Task<IEnumerable<EmployeeReturnToDto>> GetAllAsync();

    }
}
