using ApiTest.Data.Dto;
using ApiTest.Data.Model;
using ApiTest.InterFaces;
using ApiTest.Presntisses.Repository;

namespace ApiTest.Presntisses.Services
{
    public class EmpServices : IEmployeeServices
    {
        private readonly IGenaricRepository<Employee> _genaricRepository;

        public EmpServices(IGenaricRepository<Employee> genaricRepository)
        {
            _genaricRepository = genaricRepository;
        }
        public async Task<IEnumerable<EmployeeReturnToDto>> GetAllAsync()
        {
            var GetDepat = await _genaricRepository.GetAllAsync();
            var Emps = new List<EmployeeReturnToDto>();
            foreach (var emp in GetDepat)
            {
                var empDto = new EmployeeReturnToDto
                {
                    Name = emp.Name,
                    Age = emp.Age,
                    Address = emp.Address,
                    DepartmentName = emp.Department != null ? emp.Department.Name : null
                };
                Emps.Add(empDto);
            }

            return Emps;
        }
    }
}
