using CodeX.Code.BAL;
using CodeX.Core.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CodeX.Core.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        readonly UserManager<ApplicationUser> userManager;
        readonly SignInManager<ApplicationUser> signInManager;
        PersonalBAL objPersonalBAL;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            objPersonalBAL = new PersonalBAL();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await userManager.AddToRoleAsync(await userManager.FindByNameAsync(user.UserName), "Free User");
            await signInManager.SignInAsync(user, isPersistent: false);

            objPersonalBAL.PersonalSave(new Entity.Personal
            {
                FullName = model.FullName,
                MobileNumber = model.MobileNumber
            });


            return Ok(CreateToken(user));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] RegisterModel model)
        {
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!result.Succeeded)
                return BadRequest("Invalid username or password.");

            var user = await userManager.FindByEmailAsync(model.Email);
            return Ok(CreateToken(user));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] RegisterModel model)
        {
            return Ok();
        }


        string CreateToken(ApplicationUser user)
        {
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is secret key"));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(signingCredentials: signingCredentials, claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

    }
}