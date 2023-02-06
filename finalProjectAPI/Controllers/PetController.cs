using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FinalProjectApplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FinalProjectApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private IConfiguration _configuration;
        private IPetAppService _petAppService;

        public PetController(IConfiguration configuration, IPetAppService petAppService)
        {
            _configuration = configuration;
            _petAppService = petAppService;
        }

        [HttpPost("CreatePet")]
        [Authorize]        
        public async Task<IActionResult> CreatePet([FromBody] CreatePetDto model)
        {
            try
            {
                var (isAdded, isMessage) = await _petAppService.Create(model);
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

        [HttpDelete("DeletePet")]
        [Authorize]
        public async Task<IActionResult> DeletePet([FromQuery] int id)
        {
            try
            {
                var (isDeleted, isMessage) = await _petAppService.Delete(id);
                if (!isDeleted)
                {
                    return Requests.Response(this, new ApiStatus(406), isMessage, "Error");
                }
                return Requests.Response(this, new ApiStatus(200), "Success", "Deleted");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpPatch("UpdatePet")]
        [Authorize]
        public async Task<IActionResult> UpdatePet([FromBody] UpdatePetDto model)
        {
            try
            {
                var (isUpdated, isMessage) = await _petAppService.Update(model);
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

        [HttpGet("SearchPetByName")]
        public async Task<IActionResult> SearchPetByName(string name)
        {
            try
            {
                var data = await _petAppService.SearchPetByName(name);
                return Requests.Response(this, new ApiStatus(200), "", data);
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(404), null, ex.Message);
            }
        }

        [HttpGet("SearchPetByCustomer")]
        public async Task<IActionResult> SearchPetByCustomer(Guid customer)
        {
            try
            {
                var data = await _petAppService.SearchPetByCustomer(customer);
                return Requests.Response(this, new ApiStatus(200), "", data);
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(404), null, ex.Message);
            }
        }


        [HttpGet("AllPet")]
        public async Task<IActionResult> AllPet([FromQuery] PageInfo pageInfo)
        {
            try
            {
                var data = await _petAppService.GetAllPets(pageInfo);
                if (data.Total < 1)
                {
                    return NotFound();
                }
                return Requests.Response(this, new ApiStatus(200), "", data);
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }
    }
}
