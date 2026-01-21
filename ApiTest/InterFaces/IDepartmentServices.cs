using ApiTest.Data.Dto;
using ApiTest.Data.Model;

namespace ApiTest.InterFaces
{
    public interface IDepartmentServices
    {
        Task<IEnumerable<CountEmployeeInDepartmentDto>> GetCountAsync();
        Task<IEnumerable<DepartmentDto>> GetAllDept();

    }
}
