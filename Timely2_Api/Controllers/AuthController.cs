using Microsoft.AspNetCore.Mvc;
using Timely.Dtos;
using Timely.Interfaces.IServices;
using Timely.Models;

namespace Timely2_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _userService;

        public AuthController(IUserServices userService)
        {
            _userService = userService;
        }


        [HttpGet("login")]
        public ActionResult<string> Login([FromQuery] LoginRequestDto loginRequest)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
                    return BadRequest("Username and password are required.");

                var token = _userService.GetByEmailAndpass(loginRequest);
                if (token == null)
                    return Unauthorized("اسم المستخدم او كلمه المرور غير صحيحه"); // 401
                return Ok(token); // 200

            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest("حدث خطأ غير متوقع، يرجى المحاولة لاحقًا");

            }

        }

        //[HttpGet("login")]
        //public ActionResult<LoginResponse> Login([FromQuery] LoginRequestDto loginRequest)
        //{

        //    // Dummy authentication logic
        //    if (loginRequest.Email == "user@gmail.com" && loginRequest.Password == "123")
        //    {

        //        var response = new LoginResponse
        //        {
        //            IsSuccess = true,
        //            FirstName = "Rana Mahdi",
        //            Email = "user@gmail.com",

        //        };


        //        return Ok(response);
        //    }
        //    return Unauthorized();

        //}


        [HttpPost("register")]
        public ActionResult Register([FromBody] UserDto registerRequest)
        {
            try
            {
                var existingUser = _userService.IsEmailExist(registerRequest.Email);
                if (existingUser)
                {
                    return BadRequest("User already exists.");
                }

                var newUser = new User
                {
                    Name = registerRequest.Name,
                    Phone = registerRequest.Phone,
                    Email = registerRequest.Email,
                    Password = registerRequest.Password
                };

                _userService.Create(newUser);

                var result = new registerDto
                {
                    Email = newUser.Email,
                    Password = newUser.Password,
                };



                return Ok(result);
            }
            catch (Exception ex)
            {
                var message = ex.Message.ToString();
                return BadRequest($"{message}حدث خطا غير متوقع");
            }
        }
    }
}