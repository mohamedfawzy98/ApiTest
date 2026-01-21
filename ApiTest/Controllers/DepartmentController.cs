using ApiTest.Data.Dto;
using ApiTest.Data.Model;
using ApiTest.InterFaces;
using ApiTest.Presntisses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IGenaricRepository<Department> _genaricRepository;
        private readonly IDepartmentServices _departmentServices;

        public DepartmentController(IGenaricRepository<Department> genaricRepository , IDepartmentServices departmentServices)
        {
            _genaricRepository = genaricRepository;
            _departmentServices = departmentServices;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenralResponse>>> GetDepartment()
        {
            var dept = await _departmentServices.GetAllDept();
            GenralResponse genralResponse = new GenralResponse();
            if (dept.Count() > 0)
            {
                genralResponse.IsSuccess = true;
                genralResponse.Data = dept;
            }
            else
            {
                genralResponse.IsSuccess = false;
                genralResponse.Data = "No Data Found";
            }
            return new List<GenralResponse> { genralResponse };


        }
        [HttpGet("Count")]
        public async Task<ActionResult<IEnumerable<CountEmployeeInDepartmentDto>>> GetCount()
        {
            var CountDto = await _departmentServices.GetCountAsync();
            return CountDto.ToList();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GenralResponse>> GetDepartmentId(int id)
        {
            var GetDepat = await _genaricRepository.GetByIdAsync(id);
            GenralResponse genralResponse = new GenralResponse();
            if (GetDepat != null)
                {
                genralResponse.IsSuccess = true;
                genralResponse.Data = GetDepat;
            }
            else
            {
                genralResponse.IsSuccess = false;
                genralResponse.Data = "No Data Found";
            }
            return genralResponse;
        }
        [HttpPost]
        public async Task<IActionResult> AddDept(Department department)
        {
           await _genaricRepository.AddAsync(department);
            //return Created($"http://localhost:7148/api/Department/{department.Id}", department);
            return CreatedAtAction("GetDepartmentId", new { id = department.Id }, department);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDept (int id,Department department)
        {
            var GetDeptId = await _genaricRepository.GetByIdAsync(id);
            if (GetDeptId == null)
                return NotFound();
            GetDeptId.Name = department.Name;
            GetDeptId.MangerName = department.MangerName;
            await _genaricRepository.UpdateAsync(GetDeptId);
            
            return Ok($"Department {id} is Updated");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteDept(int id)
        {
            var GetDeptId = await _genaricRepository.GetByIdAsync(id);
            if (GetDeptId == null)
                return NotFound();
            await _genaricRepository.DeleteAsync(GetDeptId);

            return Ok($"Department {id} is Deleted");
        }

    }
}
