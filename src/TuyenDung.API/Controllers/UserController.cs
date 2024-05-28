using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using TuyenDung.API.Common;
using TuyenDung.API.Helper;
using TuyenDung.Data.Dto;
using TuyenDung.Data.Model;
using TuyenDung.Data.Model.Enum;
using TuyenDung.Service.Services.InterfaceIServices;

namespace TuyenDung.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserIServices _userIService;
        private readonly Token _token;
        public UserController(IUserIServices userService,Token token)
        {
            _token = token;
            _userIService = userService;
        }
        [HttpPost("CreateAdmin")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody]UserDto userDto)
        {
            try
            {
                if(userDto == null)
                {
                    throw new ArgumentNullException("All data fields have not been filled in");
                }
                var create = _userIService.RegisterUserAdmin(userDto);
                return Ok(new XBaseResult
                {
                    data = create,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = create.Id,
                    message = "Create User Successfully"
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new XBaseResult
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        [HttpPost("CreateUser")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userDto)
        {
            try
            {
                if (userDto == null)
                {
                    throw new ArgumentNullException("All data fields have not been filled in");
                }
                var create = _userIService.RegisterUser(userDto);
                return Ok(new XBaseResult
                {
                    data = create,
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    totalCount = create.Id,
                    message = "Create User Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new XBaseResult
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        [HttpPost("Login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Login(RequestDto requestDto)
        {
            try
            {
                if(requestDto == null || string.IsNullOrEmpty(requestDto.Email) || string.IsNullOrEmpty(requestDto.Password))
                {
                    return BadRequest("Email and password are required.");
                }
                User user = _userIService.Login(requestDto);
                string jwtToken = _token.CreateToken(user);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.UtcNow.AddMinutes(3000)
                };
                HttpContext.Response.Cookies.Append("authenticationToken", jwtToken, cookieOptions);
                return Ok(new XBaseResult
                {
                    success = true,
                    httpStatusCode = (int)HttpStatusCode.OK,
                    message = jwtToken
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new XBaseResult
                {
                    success = false,
                    httpStatusCode = (int)HttpStatusCode.BadRequest,
                    message = ex.Message
                });
            }
        }
        [HttpPost("Logout")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult Logout()
        {
            try
            {
                //lây thông tin người dùng
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    var tokenStatus = _token.CheckTokenStatus(userId);
                    if(tokenStatus == StatusToken.Expired)
                    {
                        return Unauthorized("The token is no longer valid. Please log in again.");
                    }
                    var result = _userIService.Logout(userId);

                    if (result)
                    {
                        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        return Ok("Logged out successfully.");
                    }
                    else
                    {
                        return StatusCode(500, "An error occurred during logout.");
                    }
                }
                else
                {
                    return BadRequest("Invalid user ID.");
                }
            }
            catch(Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }
    }
}
