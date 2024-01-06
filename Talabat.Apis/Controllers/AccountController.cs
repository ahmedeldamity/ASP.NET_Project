using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Dtos;
using Talabat.Apis.Errors;
using Talabat.Core.Entities.Identity;
using Talabat.Core.IServices;

namespace Talabat.Apis.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthService _authService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null) 
                return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded) 
                return Unauthorized(new ApiResponse(401));

            return Ok(new UserDto()
            {
                DisplatName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var user = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400));

            return Ok(new UserDto()
            {
                DisplatName = user.DisplayName,
                Email = user.Email,
                Token = await _authService.CreateTokenAsync(user, _userManager)
            });
        }
    }
}   