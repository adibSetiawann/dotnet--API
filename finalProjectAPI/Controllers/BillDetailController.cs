using FinalProjectApplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillDetailController : ControllerBase
    {
        private IConfiguration _configuration;
        private IBillDetailAppService _billDetailAppService;

        public BillDetailController(IConfiguration configuration, IBillDetailAppService BillDetailAppService)
        {
            _configuration = configuration;
            _billDetailAppService = BillDetailAppService;
        }

        [HttpPost("CreateBillDetail")]
        [Authorize]
        public async Task<IActionResult> CreateBillDetail([FromBody] CreateBillDetailDto model)
        {
            try
            {
                var (isAdded, isMessage) = await _billDetailAppService.Create(model);
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

        [HttpPatch("UpdateBillDetail")]
        [Authorize]
        public async Task<IActionResult> UpdateBillDetail([FromBody] BillDetailListDto model)
        {
            try
            {
                var (isAdded, isMessage) = await _billDetailAppService.Update(model);
                if (!isAdded)
                {
                    return Requests.Response(this, new ApiStatus(406), isMessage, "Error");
                }
                return Requests.Response(this, new ApiStatus(201), isMessage, "Bill Detail Updated");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }



        [HttpPatch("DeleteBillDetail")]
        [Authorize]
        public async Task<IActionResult> DeleteBillDetail([FromQuery] Guid id)
        {
            try
            {
                var (isAdded, isMessage) = await _billDetailAppService.Delete(id);
                if (!isAdded)
                {
                    return Requests.Response(this, new ApiStatus(406), isMessage, "Error");
                }
                return Requests.Response(this, new ApiStatus(200), isMessage, "BillDetail Delete");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpGet("SearchBill")]
        [Authorize]
        public async Task<IActionResult> SearchBillDetail([FromQuery] int billId)
        {
            try
            {
                var data = _billDetailAppService.SearchBillDetail(billId);
                return Requests.Response(this, new ApiStatus(200), "success", data);
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(404), null, ex.Message);
            }
        }
    }
}