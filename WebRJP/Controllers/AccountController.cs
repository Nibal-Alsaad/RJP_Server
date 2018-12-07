using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WebRJP.Common;

namespace WebRJP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        TokenProvider _tokenProvider;

        public AccountController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            TokenProvider tokenProvider,
            ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenProvider = tokenProvider;
            _logger = logger;
        }



        [HttpPost("/api/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel userInfo)
        {
            if (userInfo == null)
            {
                return BadRequest("Invalid client request");
            }
            var user = await _userManager.FindByNameAsync(userInfo.userName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, userInfo.password, false, false);

                if (result.Succeeded)
                {
                    var tokenString = _tokenProvider.createToken();
                    return Ok(new
                    {
                        token = tokenString
                    });
                }
                else
                {
                    _logger.LogError("password not correct");
                    return Unauthorized();
                }
            }
            _logger.LogError("user not exist");
            return BadRequest("this client not exist");
        }
        [HttpPost("/api/register")]
        public async Task<IActionResult> Register([FromBody] LoginModel userInfo)
        {
            var user = new IdentityUser { UserName = userInfo.userName };
            var result = await _userManager.CreateAsync(user, userInfo.password);
            if (result.Succeeded)
            {
                var tokenString = _tokenProvider.createToken();
                return Ok(new
                {
                    token = tokenString
                });
            }

            _logger.LogError("password short or not contain digits");

            return BadRequest(new
            {

                message = "password length must be 6 atleast and password must contain 1 digit atleast "

            });
        }
    }
}