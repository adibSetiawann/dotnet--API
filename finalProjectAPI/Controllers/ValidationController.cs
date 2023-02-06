using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finalProjectApplication.DefaultServices.ValidationUserServices;
using finalProjectApplication.DefaultServices.ValidationUserServices.Dto;
using FinalProjectApplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace finalProjectAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValidationController : ControllerBase
    {
        private IConfiguration _configuration;
        private IValidationUser _validationUser;
        public ValidationController(IConfiguration configuration, IValidationUser validationUser)
        {
            _configuration = configuration;
            _validationUser = validationUser;
        }
        [HttpPatch("VerifyEmailUser")]
        // [Authorize]
        public async Task<IActionResult> VerifyEmailUser([FromQuery] UpdateStatus model)
        {
            try
            {
                var (isUpdated, isMessage) = await _validationUser.UpdateStatus(model);
                if (!isUpdated)
                {
                    return Requests.Response(this, new ApiStatus(406), isMessage, "error");
                }
                return Requests.Response(this, new ApiStatus(200), isMessage, "Success");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }
    }
}