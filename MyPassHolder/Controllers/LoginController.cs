using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPassHolder.Common;
using MyPassHolder.RequestResponse;
using MyPassHolder.Security;
using MyPassHolder.Services;

namespace MyPassHolder.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;
        private readonly IConfiguration _configuration;

        public LoginController(LoginService loginService, IConfiguration configuration)
        {
            this._loginService = loginService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult login(LoginRequest req)
        {
            JsonResult jsonResponse;

            try
            {
                ResponseHandle res = _loginService.login(req);
                if (res.success)
                {
                    Token token = TokenHandler.createToken(_configuration, null, false, res.email);
                    jsonResponse = new JsonResult(new { success = true, token = token });
                }
                else
                    jsonResponse = new JsonResult(new { success = false, errorMessage = res.errorMesssage });
            }
            catch (Exception ex)
            {
                jsonResponse = new JsonResult(new { success = false, errorMessage = ex.Message });
            }

            return jsonResponse;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult forgetPassword(string email)   //token lı link göndermek gerek
        {
            JsonResult jsonResponse;

            try
            {
                ResponseHandle res = _loginService.forgetPassword(email);
                if (res.success)
                {
                    jsonResponse = new JsonResult(new { success = true });
                }
                else
                    jsonResponse = new JsonResult(new { success = false, errorMessage = res.errorMesssage });
            }
            catch (Exception ex)
            {
                jsonResponse = new JsonResult(new { success = false, errorMessage = ex.Message });
            }

            return jsonResponse;
        }

        [HttpPost]
        [Authorize]
        public IActionResult changePassword(ChangePasswordRequest req)  //email şifremi unuttum link ile geldi
        {
            JsonResult jsonResponse;

            try
            {
                string email = TokenHandler.decodeTokenForEmail(_configuration, req.token); 

                ResponseHandle res = _loginService.changePassword(email, req.newPassword);
                if (res.success)
                {
                    jsonResponse = new JsonResult(new { success = true });
                }
                else
                    jsonResponse = new JsonResult(new { success = false, errorMessage = res.errorMesssage });
            }
            catch (Exception ex)
            {
                jsonResponse = new JsonResult(new { success = false, errorMessage = ex.Message });
            }

            return jsonResponse;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult tokenValidator(string token)
        {
            JsonResult jsonResponse;

            try
            {
                Boolean isValid = TokenValidator.isTokenValid(token);
                if (isValid)
                {
                    jsonResponse = new JsonResult(new { success = true });
                }
                else
                    jsonResponse = new JsonResult(new { success = false, errorMessage = "Token Geçerli Değil" });
            }
            catch (Exception ex)
            {
                jsonResponse = new JsonResult(new { success = false, errorMessage = ex.Message });
            }

            return jsonResponse;
        }
    }
}
