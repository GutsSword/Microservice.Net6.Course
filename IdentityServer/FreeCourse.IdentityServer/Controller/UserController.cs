using FreeCourse.IdentityServer.Dtos;
using FreeCourse.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;
namespace FreeCourse.IdentityServer.Controller
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            var user = new ApplicationUser()
            {
                Email = signUpDto.Email,
                UserName = signUpDto.UserName,
            };
            
            var result = await userManager.CreateAsync(user, signUpDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x=>x.Description).ToList());
            }

            return NoContent();

        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);

            if (userIdClaim == null)
                return BadRequest();

            var user = await userManager.FindByIdAsync(userIdClaim.Value);

            if (user == null)
                return BadRequest();

            return Ok(new
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            });
        }
    }
}
