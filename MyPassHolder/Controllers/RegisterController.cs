using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPassHolder.Common;
using MyPassHolder.RequestResponse;
using MyPassHolder.Services;
using System.Xml;

namespace MyPassHolder.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class RegisterController : ControllerBase
    {
        private readonly RegisterService _registerService;
       
        public RegisterController(RegisterService registerService) 
        {
            this._registerService = registerService;
        }

        [HttpPost]
        public IActionResult createAccount(RegisterRequest req) 
        {
            JsonResult jsonResponse;

            try
            {
                ResponseHandle res = _registerService.createAccount(req);

                if (res.success)             
                    jsonResponse = new JsonResult(new { success = true, data = res.data });              
                else               
                    jsonResponse = new JsonResult(new { success = false, errorMessage = res.errorMesssage });                
            }
            catch (Exception ex)
            {
                jsonResponse = new JsonResult (new { success = false, errorMessage = ex.Message });
            }

            return jsonResponse;
        }
    }
}
