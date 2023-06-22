
using DipoleBank.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTO;
using System.Security.Claims;

namespace DipoleBank.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _accountService;
        private readonly IEmailServices _emailServices;

        public UserController(IUserService accountService, IEmailServices emailServices)
        {
            _accountService = accountService;
            _emailServices = emailServices;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(SignUp signUp)
        {
            var registerUser = await _accountService.RegisterUser(signUp, "USER");
            if (registerUser.StatusCode == 200)
            {
                return Ok(registerUser);
            }
            else if (registerUser.StatusCode == 404)
            {
                return NotFound(registerUser);
            }
            else
            {
                return BadRequest(registerUser);
            }
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(SignInModel signIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(signIn);
            }
            var loginUser = await _accountService.LoginUser(signIn);

            if (loginUser.StatusCode == 200)
            {
                return Ok(loginUser);
            }
            else if (loginUser.StatusCode == 404)
            {
                return NotFound(loginUser);
            }
            else
            {
                return BadRequest(loginUser);
            }

        }
        [HttpPost("forgot_appkey")]
        public async Task<IActionResult> ForgotAppId(string appId)
        {
            var result = await _accountService.ForgotAppId(appId);
            if (result.StatusCode == 200)
            {
                var message = new Message(new string[] { result.Result.Email }, "Reset AppKey Token", $"<p>Your reset Appkey Token is below<p><h6>{result.Result.ResetToken}</h6> <p>Please use it in your reset appkey page</p>");
                _emailServices.SendEmail(message);
                result.Result = null;
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }

        }
        [HttpPost("reset_appkey")]
        public async Task<IActionResult> ResetAppKey(ResetAppKeyDto resetAppKey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(resetAppKey);
            }
            var result = await _accountService.ResetAppKey(resetAppKey);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("delete_user/{appid}")]
        public async Task<IActionResult> DeleteUser(string appid)
        {

            var result = await _accountService.DeleteUser(appid);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPatch("update_details/{appid}")]
        public async Task<IActionResult> UpdateUserInfo(string appid, UpdateUserDto updateUser)
        {
            var result = await _accountService.UpdateUser(appid, updateUser);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPatch("update_picture/{appid}")]
        public async Task<IActionResult> UploadUserPicture(string appid, IFormFile file)
        {
            var result = await _accountService.UploadUserProfilePicture(appid, file);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize(Roles = "ADMIN", AuthenticationSchemes = "Bearer")]
        [HttpPatch("update_role/{appid}")]
        public async Task<IActionResult> UpdateUserRoleAsync(string appid, string role)
        {
            var result = await _accountService.UpdateUserRole(appid, role);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUser(int pagenumber, int perpagesize)
        {
            var loggedInAppId = User.FindFirstValue("AppId");
            var result = await _accountService.GetAllUser(pagenumber, perpagesize, loggedInAppId);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("attach/{appid}/{appid2}")]
        public async Task<IActionResult> AttachUser (string appid, string appid2)
        {
            var result = await _accountService.AttachUser(appid, appid2);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("dettach/{appid}")]
        public async Task<IActionResult> DettachUser(string appid)
        {
            var result = await _accountService.DettachUser(appid);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("getattach/{appid}")]
        public async Task<IActionResult> GetAttachUser(string appid)
        {
            var result = await _accountService.GetAttachUser(appid);
            if (result.StatusCode == 200)
            {
                return Ok(result);
            }
            else if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
