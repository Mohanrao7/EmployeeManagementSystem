using EmployeeManagementApi.Models;
using EmployeeManagementApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace EmployeeManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository IRepo;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository IRepo)
        {
            this.userManager = userManager;
            this.IRepo = IRepo;
        }

        public UserManager<IdentityUser> UserManager { get; }

        //Post: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            var identityUser = new IdentityUser
            {
                UserName = dto.UserName,
                Email = dto.UserName
            };
            var identityResult= await userManager.CreateAsync(identityUser,dto.Password);

            if(identityResult.Succeeded)
            {
                //Add roles to this User
                if(dto.Roles!=null && dto.Roles.Any())
                {
                    identityResult= await userManager.AddToRolesAsync(identityUser, dto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registerd! Please Login.");
                    }
                }
                

            }
            return BadRequest("Something went wrong");



        }

        //Post: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var user=await userManager.FindByEmailAsync(dto.UserName);
            if (user != null)
            {
                var checkPasswordResult=await userManager.CheckPasswordAsync(user, dto.Password);
                if (checkPasswordResult)
                {
                    //Get Roles For this user
                    var roles=await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        //Create Token


                        var jwtToken= IRepo.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response);
                    }

                    
                    
                }
            }
            return BadRequest("Username or Password Incorrect");
        }
    }
}
