using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyPassHolder.Common;
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

        public UserOperationsController(UserOperationsService userOperationsService)
        {
            this._userOperationsService = userOperationsService;
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
    }
}
