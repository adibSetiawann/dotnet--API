using FinalProjectApplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private IConfiguration _configuration;
        private IBillAppService _billAppService;

        public BillController(IConfiguration configuration, IBillAppService BillAppService)
        {
            _configuration = configuration;
            _billAppService = BillAppService;
        }

        [HttpPost("CreateBill")]
        [Authorize]
        public async Task<IActionResult> CreateBill([FromBody] CreateBillDto model)
        {
            try
            {
                var (isAdded, isMessage) = await _billAppService.Create(model);
                if (!isAdded)
                {
                    return Requests.Response(this, new ApiStatus(406), isMessage, "Error");
                }
                return Requests.Response(this, new ApiStatus(201), isMessage, "Created");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpPatch("UpdateBill")]
        [Authorize]
        public async Task<IActionResult> UpdateBill([FromQuery] int id)
        {
            try
            {
                var (isAdded, isMessage) = await _billAppService.Update(id);
                if (!isAdded)
                {
                    return Requests.Response(this, new ApiStatus(406), isMessage, "Error");
                }
                return Requests.Response(this, new ApiStatus(200), isMessage, "Bill Payment SUCCESS");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpPatch("CancelBill")]
        [Authorize]
        public async Task<IActionResult> CancelBill([FromQuery] int id)
        {
            try
            {
                var (isAdded, isMessage) = await _billAppService.CancelBill(id);
                if (!isAdded)
                {
                    return Requests.Response(this, new ApiStatus(406), isMessage, "Error");
                }
                return Requests.Response(this, new ApiStatus(200), isMessage, "Bill Payment CANCEL");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }
    }
}