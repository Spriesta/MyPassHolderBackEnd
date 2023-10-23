﻿using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;
        private readonly IConfiguration _configuration;

        public LoginController(LoginService loginService, IConfiguration configuration)
        {
            this._loginService = loginService;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult login(LoginRequest req)
        {
            JsonResult jsonResponse;

            try
            {
                ResponseHandle res = _loginService.login(req);
                if (res.success)
                {
                    Token token = TokenHandler.createToken(_configuration, null, false);
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
    }
}
