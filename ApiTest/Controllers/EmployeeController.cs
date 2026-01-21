using ApiTest.Data.Dto;
using ApiTest.Data.Model;
using ApiTest.InterFaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly IGenaricRepository<Employee> _genaricRepository;
        private readonly IEmployeeServices _employeeServices;

        public EmployeeController(IGenaricRepository<Employee> genaricRepository , IEmployeeServices employeeServices)
        {
            _genaricRepository = genaricRepository;
            _employeeServices = employeeServices;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeReturnToDto>>> GetEmployee()
        {
            var GetEmps = await _employeeServices.GetAllAsync();

            return GetEmps.ToList();
        }
       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeId(int id)
        {
            var GetDepat = await _genaricRepository.GetByIdAsync(id);
            return Ok(GetDepat);
        }
        [HttpPost]
        public async Task<IActionResult> AddDept(EmployeeDto employeedto)
        {
            var employee = new Employee
            {
                Name = employeedto.Name,
                Age = employeedto.Age,
                Address = employeedto.Address,
                DepartmentId = employeedto.DepartmentId
            };
            await _genaricRepository.AddAsync(employee);
            //return Created($"http://localhost:7148/api/Employee/{Employee.Id}", Employee);
            return CreatedAtAction("GetEmployeeId", new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDept(int id, Employee employee)
        {
            var GetempId = await _genaricRepository.GetByIdAsync(id);
            if (GetempId == null)
                return NotFound();
            GetempId.Name = employee.Name;
            GetempId.Address = employee.Address;
            GetempId.Age = employee.Age;
            await _genaricRepository.UpdateAsync(GetempId);

            return Ok($"Employee {id} is Updated");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDept(int id)
        {
            var GetDeptId = await _genaricRepository.GetByIdAsync(id);
            if (GetDeptId == null)
                return NotFound();
            await _genaricRepository.DeleteAsync(GetDeptId);

            return Ok($"Employee {id} is Deleted");
        }
    }
}
