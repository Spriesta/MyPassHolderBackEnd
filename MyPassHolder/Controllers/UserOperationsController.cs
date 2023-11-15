using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyPassHolder.Common;
using MyPassHolder.Repositories;
using MyPassHolder.RequestResponse;
using MyPassHolder.Security;
using MyPassHolder.Services;

namespace MyPassHolder.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserOperationsController : ControllerBase
    {
        private readonly UserOperationsService _userOperationsService;
        private readonly IConfiguration _configuration;
        private readonly LoginRepository _loginRepository;

        public UserOperationsController(UserOperationsService userOperationsService, IConfiguration configuration, LoginRepository loginRepository)
        {
            _userOperationsService = userOperationsService;
            _configuration = configuration;
            _loginRepository = loginRepository;
        }

        [HttpPost]
        public IActionResult createCategory(CategoryRequest req)
        {
            JsonResult jsonResponse;

            try
            {
                ResponseHandle res = _userOperationsService.createCategory(req);
                if (res.success)
                {
                    jsonResponse = new JsonResult(new { success = true, data = res.data });
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
        public IActionResult createOrUpdateMyPassword(createOrUpdateMyPasswordRequest req)
        {
            JsonResult jsonResponse;

            try
            {
                ResponseHandle res = _userOperationsService.createOrUpdateMyPassword(req);
                if (res.success)
                {
                    jsonResponse = new JsonResult(new { success = true, data = res.data });
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
        public IActionResult deleteMyPassword(long id)
        {
            JsonResult jsonResponse;

            try
            {
                ResponseHandle res = _userOperationsService.deleteMyPassword(id);
                if (res.success)
                {
                    jsonResponse = new JsonResult(new { success = true, data = res.data });
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
        public IActionResult listCategory(CategoryRequest req)
        {
            JsonResult jsonResponse;

            try
            {
                string email = TokenHandler.decodeTokenForEmail(_configuration, req.token);
                User? user = _loginRepository.getAccountWithEmail(email);

                if (user != null)
                {
                    ResponseHandle res = _userOperationsService.listCategory(user.Id);
                    if (res.success)                  
                        jsonResponse = new JsonResult(new { success = true, data = res.data });
                    
                    else
                        jsonResponse = new JsonResult(new { success = false, errorMessage = res.errorMesssage });
                }               
                else
                    jsonResponse = new JsonResult(new { success = false });
            }
            catch (Exception ex)
            {
                jsonResponse = new JsonResult(new { success = false, errorMessage = ex.Message });
            }

            return jsonResponse;
        }
    }
}
