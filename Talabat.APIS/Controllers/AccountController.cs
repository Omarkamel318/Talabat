using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.APIS.DTO;
using Talabat.APIS.Error;
using Talabat.Application;
using Talabat.Core.Contract;
using Talabat.Core.Entities.Identities;

namespace Talabat.APIS.Controllers
{

	public class AccountController : BaseAPIController
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IAuthService _authService;
		private readonly IMapper _mapper;

		public AccountController(
			UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IAuthService authService,
			IMapper mapper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_authService = authService;
			_mapper = mapper;
		}


		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user is null)
			{
				return Unauthorized(new APIResponse(401, "invalid login"));

			}
			var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

			if (!result.Succeeded)
			{
				return Unauthorized(new APIResponse(401, "invalid login"));

			}
			return Ok(new UserDto()
			{
				DisplayName = user.UserName,
				Email = user.Email,
				Token = await _authService.CreateTokenAsync(user,_userManager)
			}) ;

		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Signup(SignupDto model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user is null)
			{
				user = new ApplicationUser()
				{
					Email = model.Email,
					DisplayName= model.DisplayName,
					UserName = model.Email.Split("@")[0],
					PhoneNumber=model.Phone
				};
				 await _userManager.CreateAsync(user,model.Password);
				return Ok(new UserDto()
				{
					DisplayName = user.UserName,
					Email = user.Email,
					Token = await _authService.CreateTokenAsync(user, _userManager)

				});

			}
			return Unauthorized(new APIResponse(401, "invalid SignUp"));
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
			var user = await _userManager.FindByEmailAsync(email);
			return Ok(new UserDto()
			{
				DisplayName = user.UserName,
				Email = user.Email,
				Token = await _authService.CreateTokenAsync(user, _userManager)

			});
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpGet("Address")]
		public async Task<ActionResult<AddressDto>> GetAddress()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.Users.Where(u => u.NormalizedEmail == email.ToUpper()).Include(u => u.Address).FirstOrDefaultAsync() ;
			var address = _mapper.Map<AddressDto>(user?.Address);
			return Ok(address);
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
		[HttpPut("Address")]

		public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto address)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.Users.Where(u => u.NormalizedEmail == email.ToUpper()).Include(u => u.Address).FirstOrDefaultAsync(); 
			var newAddress = _mapper.Map<Address>(address);
			newAddress.Id = user.Address.Id;
			user.Address = newAddress;
			var result = await _userManager.UpdateAsync(user);
			if (!result.Succeeded) 
				return BadRequest(new APiValidationErrorResponse(errors: result.Errors.Select(e=>e.Description)));
			return Ok(address);
		}


	}

}

