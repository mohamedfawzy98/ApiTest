using ApiTest.Data.Dto;
using ApiTest.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _confg;

        public AccountController(UserManager<ApplicationUser> userManager , IConfiguration configuration)
        {
            _userManager = userManager;
            _confg = configuration;
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
        [HttpPost("Login")]
        public async Task<IActionResult> Login(Registerdto registerdto)
        {
            var GetName = await _userManager.FindByNameAsync(registerdto.UserName);
            if (GetName != null)
            {
                bool found = await _userManager.CheckPasswordAsync(GetName, registerdto.Password);
                if (found)
                {
                    // Genrate JWT

                    // Create PayLoad (Claims)

                    List<Claim> UserCliam = new List<Claim>();

                    // Generete Guid In ID Claim To Not Static Token 
                    UserCliam.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    UserCliam.Add(new Claim(ClaimTypes.Name, GetName.UserName));
                    UserCliam.Add(new Claim(ClaimTypes.NameIdentifier, GetName.Id));
                    UserCliam.Add(new Claim(ClaimTypes.Email, GetName.Email));
                     var Rols = await _userManager.GetRolesAsync(GetName);
                    foreach (var rolitem in Rols)
                    {
                        UserCliam.Add(new Claim(ClaimTypes.Role, rolitem));
                    }


                    // Create Signture (signingCredentials)
                    var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_confg["JWT:SecritKey"]));
                    SigningCredentials signingCred = new SigningCredentials(
                        Key, SecurityAlgorithms.HmacSha256
                        );

                    // Design the token
                    JwtSecurityToken mytoken = new JwtSecurityToken(
                        // Here you can add claims, issuer, audience, expiration, and signing credentials
                        issuer: _confg["JWT:IssuerIp"],  // BaeseUrl
                        audience: _confg["JWT:AudienceIp"],  // Angluer
                        expires: DateTime.UtcNow.AddHours(1),
                        claims: UserCliam,
                        signingCredentials: signingCred

                    );

                    return Ok(new
                    { //JwtSecurityTokenHandler  To Generate Token And Valid
                        token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                        expiration = DateTime.UtcNow.AddHours(1), //mytoken.ValidTo
                    });

                }
                ModelState.AddModelError(string.Empty, "UserName Or Password Is Error");

            }

            return BadRequest("Error");
        }
    }
}
