using ApiTest.Data.Dto;
using ApiTest.Data.Model;
using ApiTest.InterFaces;
using ApiTest.Presntisses.Repository;

namespace ApiTest.Presntisses.Services
{
    public class DeptServices : IDepartmentServices
    {
       private readonly IGenaricRepository<Department> _genaricRepository;

        public DeptServices(IGenaricRepository<Department> genaricRepository)
        {
            _genaricRepository = genaricRepository;
        }

        public async Task<IEnumerable<DepartmentDto>> GetAllDept()
        {
            var getdept = await _genaricRepository.GetAllAsync();
            var Deptdto = new List<DepartmentDto>();
            foreach (var dept in getdept)
            {
                Deptdto.Add(new DepartmentDto
                {
                    Id = dept.Id,
                    Name = dept.Name,
                    MangerName = dept.MangerName
                });
              
            }

            return Deptdto;
        }

        public async Task<IEnumerable<CountEmployeeInDepartmentDto>> GetCountAsync()
        {
            var getdept = await _genaricRepository.GetAllAsync();
            var CountDto = new List<CountEmployeeInDepartmentDto>();
            foreach (var dept in getdept)
            {
                CountDto.Add(new CountEmployeeInDepartmentDto
                {
                    DepartmentName = dept.Name,
                    EmployeeCount = dept.Employees?.Count() ?? 0
                });
            }
            return CountDto;
        }
    }
}
