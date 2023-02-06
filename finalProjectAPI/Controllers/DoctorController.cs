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
    public class DoctorController : ControllerBase
    {
        private IConfiguration _configuration;
        private IDoctorAppService _doctorAppService;

        public DoctorController(IConfiguration configuration, IDoctorAppService doctorAppService)
        {
            _configuration = configuration;
            _doctorAppService = doctorAppService;
        }

        [AllowAnonymous]
        [HttpPost("LoginDoctor")]
        public IActionResult Login([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();

            var user = AuthenticateUser(login);
            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(user);
                response = Ok(new { phone_number = login.MobilePhoneNumber, token = tokenString });
            }
            else
            {
                response = NotFound();
            }
            return response;
        }

        private UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;

            var doctor = _doctorAppService.SearchDoctorPhone(login.MobilePhoneNumber, login.Password);

            if (doctor == null)
            {
                return null;
            }
            user = new UserModel { MobilePhoneNumber = login.MobilePhoneNumber };
            
            return user;
        }

        private string GenerateJSONWebToken(UserModel userModel)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Key"])
            );
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(1200),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("CreateDoctor")]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto model)
        {
            try
            {
                var (isAdded, isMessage) = await _doctorAppService.Create(model);
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

        [HttpPatch("DeleteDoctor")]
        [Authorize]
        public async Task<IActionResult> DeleteDoctor([FromQuery] int id)
        {
            try
            {
                var (isDeleted, isMessage) = await _doctorAppService.Delete(id);
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

        [HttpPatch("UpdateDoctor")]
        [Authorize]
        public async Task<IActionResult> UpdateDoctor([FromBody] UpdateDoctorDto model)
        {
            try
            {
                var (isUpdated, isMessage) = await _doctorAppService.Update(model);
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

        [HttpGet("SearchDoctorByid")]
        public async Task<IActionResult> SearchDoctorid(int id)
        {
            try
            {
                var data = _doctorAppService.SearchDoctorById(id);
                if (data.Count < 1)
                {
                    return Requests.Response(this, new ApiStatus(404), null, $"data with id {id} not found in the database");
                }
                return Requests.Response(this, new ApiStatus(200), "",data);
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(404), null, ex.Message);
            }
        }

        [HttpGet("SearchDoctorByName")]
        public async Task<IActionResult> SearchDoctorName(string name)
        {
            try
            {
                var data = _doctorAppService.SearchDoctorName(name);
                if (data.Count < 1)
                {
                    return Requests.Response(this, new ApiStatus(404), null, $"data with name {name} not found in the database");
                }
                return Requests.Response(this, new ApiStatus(200), "", data);
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(404), null, ex.Message);
            }
        }

        [HttpGet("SearchDoctorByCity")]
        public async Task<IActionResult> SearchDoctorCity(int cityId)
        {
            try
            {
                var data = _doctorAppService.SearchDoctorByCity(cityId);
                if (data.Count < 1)
                {
                    return Requests.Response(this, new ApiStatus(404), null, "data not found in this city");
                }
                return Requests.Response(this, new ApiStatus(200), "", data);
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpGet("AllDoctor")]
        public async Task<IActionResult> AllDoctor([FromQuery] PageInfo pageInfo)
        {
            try
            {
                var data = await _doctorAppService.GetAllDoctors(pageInfo);
                if (data.Total < 1)
                {
                    return Requests.Response(this, new ApiStatus(404), null, "database doctor is empty ");
                }
                return Requests.Response(this, new ApiStatus(200), "", data);
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpPost("ForgotPassword")]
        [Authorize]
        public async Task<IActionResult> ForgotPassword([FromQuery] string email)
        {
            try
            {
                var (isAdded, isMessage) = await _doctorAppService.ForgotPassword(email);
                if (!isAdded)
                {
                    return Requests.Response(this, new ApiStatus(406), isMessage, "Error");
                }
                return Requests.Response(this, new ApiStatus(200), isMessage, "Success send email");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }

        [HttpPost("UpdatePassword")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword([FromQuery] string password, string email)
        {
            try
            {
                var (isAdded, isMessage) = await _doctorAppService.UpdatePassword(password, email);
                if (!isAdded)
                {
                    return Requests.Response(this, new ApiStatus(406), isMessage, "Error");
                }
                return Requests.Response(this, new ApiStatus(200), isMessage, "Success update passwprd");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(500), null, ex.Message);
            }
        }
    }
}
