using ApiTest.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindingController : ControllerBase
    {
        [HttpPost("{name}")]   // Path Route
        public IActionResult Binding1(Department department, string name)
        { // if i delete ("{name}") from Get will be binding from Query

            return Ok();
        }

        // To Customize Compliex in Route No Body As No Body In Get
        [HttpGet("{Id}/{Name}/{MangerName}")]
        public IActionResult Binding2([FromRoute]Department department)
        {
            return Ok();
        }

        // To Customize Primitive In Body No Route
        [HttpPost]
        public IActionResult Binding3([FromBody] int age)
        {
            return Ok();
        }
    }
}
