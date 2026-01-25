using ApiTest.Data.Dto;
using ApiTest.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Registerdto registerdto)
        {

            #region Password
//            {
//                "userName": "Mohamed",
//  "email": "11",
//  "password": "Mohamed123#"
//}
            #endregion
            var user = new ApplicationUser
            {
                UserName = registerdto.UserName,
                Email = registerdto.Email
            };
            var result = await _userManager.CreateAsync(user, registerdto.Password);
            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully" });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
        }
    }
}
