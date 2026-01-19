using ApiTest.Data.Model;
using ApiTest.InterFaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IGenaricRepository<Department> _genaricRepository;

        public DepartmentController(IGenaricRepository<Department> genaricRepository)
        {
            _genaricRepository = genaricRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetDepartment()
        {
            var GetDepat = await _genaricRepository.GetAllAsync();
            return Ok(GetDepat);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentId(int id)
        {
            var GetDepat = await _genaricRepository.GetByIdAsync(id);
            return Ok(GetDepat);
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
            
            return Ok($"Department {id} is Updateed");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult>DeleteDept(int id)
        {
            var GetDeptId = await _genaricRepository.GetByIdAsync(id);
            if (GetDeptId == null)
                return NotFound();
            await _genaricRepository.DeleteAsync(GetDeptId);

            return Ok($"Department {id} is Delted");
        }

    }
}
