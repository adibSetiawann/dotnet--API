using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using finalProjectApplication;
using FinalProjectApplication;
using FinalProjectDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
                return Requests.Response(
                    this,
                    new ApiStatus(200),
                    isMessage,
                    "Bill Payment SUCCESS"
                );
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
                return Requests.Response(
                    this,
                    new ApiStatus(200),
                    isMessage,
                    "Bill Payment CANCEL"
                );
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpGet("GetBill")]
        public async Task<IActionResult> GetBillDetailll([FromQuery] string poNumber)
        {
            try
            {
                var data = await _billAppService.GetRandomNumber(poNumber);
                if (data.Length < 1)
                {
                    return Requests.Response(
                        this,
                        new ApiStatus(404),
                        null,
                        $"database customer is empty"
                    );
                }
                ChargeSuccessDto chargeSuccessDto = new ChargeSuccessDto();
                chargeSuccessDto = JsonConvert.DeserializeObject<ChargeSuccessDto>(data);
                return Requests.Response(
                    this,
                    new ApiStatus(200),
                    "Success",
                    chargeSuccessDto
                );
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpPost("CreateMidtrans")]
        public async Task<IActionResult> CreateMidtrans([FromBody] CreatePaymentDto dto)
        {
            try
            {
                var data = await _billAppService.CreatePayment(dto);
                SuccessDto successDto = new SuccessDto();
                successDto = JsonConvert.DeserializeObject<SuccessDto>(data);

                return Requests.Response(
                    this,
                    new ApiStatus(200),
                    "success",
                    successDto
                );
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }
        [HttpPost("CreateCharge")]
        public async Task<IActionResult> CreateCharge([FromBody] ChargeDto dto)
        {
            try
            {
                var data = await _billAppService.CreateCharge(dto);
                ChargeSuccessDto chargeSuccessDto = new ChargeSuccessDto();
                chargeSuccessDto = JsonConvert.DeserializeObject<ChargeSuccessDto>(data);

                return Requests.Response(
                    this,
                    new ApiStatus(200),
                    "Charge Created",
                    chargeSuccessDto
                );
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpPost("CancelCharge")]
        public async Task<IActionResult> CancelCharge([FromQuery] string orderId)
        {
            try
            {
                var data = await _billAppService.CancelCharge(orderId);
                // ChargeSuccessDto chargeSuccessDto = new ChargeSuccessDto();
                // chargeSuccessDto = JsonConvert.DeserializeObject<ChargeSuccessDto>(data);

                return Requests.Response(
                    this,
                    new ApiStatus(200),
                    "success",
                    $"order with id {orderId} canceled"
                );
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }
    }
}