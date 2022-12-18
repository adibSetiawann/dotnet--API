using finalProjectApplication.DefaultServices.CustomerAppServices;
using finalProjectApplication.DefaultServices.CustomerAppServices.Dto;
using FinalProjectApplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace finalProjectAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private IConfiguration _configuration;
        private ICustomerAppService _customerAppService;

        public CustomerController(IConfiguration configuration, ICustomerAppService customerAppService)
        {
            _configuration = configuration;
            _customerAppService = customerAppService;
        }

        [AllowAnonymous]
        [HttpPost("LoginCustomer")]
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

            var customer = _customerAppService.SearchCustomerPhone(login.MobilePhoneNumber, login.Password);

            if (customer == null)
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

        [HttpPost("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDto model)
        {
            try
            {
                var (isAdded, isMessage) = await _customerAppService.Create(model);
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

        [HttpPatch("DeleteCustomer")]
        [Authorize]
        public async Task<IActionResult> DeleteCustomer([FromQuery] Guid id)
        {
            try
            {
                var (isDeleted, isMessage) = await _customerAppService.Delete(id);
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

        [HttpPatch("UpdateCustomer")]
        [Authorize]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDto model)
        {
            try
            {
                var (isUpdated, isMessage) = await _customerAppService.Update(model);
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

        [HttpGet("SearchCustomerByName")]
        public async Task<IActionResult> SearchCustomerByName(string name)
        {
            try
            {
                var data = _customerAppService.SearchCustomerName(name);
                if (data.Count < 1)
                {
                    return Requests.Response(this, new ApiStatus(404), null, $"{name} not found in the database");
                }
                return Requests.Response(this, new ApiStatus(200), data, "");
            }
            catch (Exception ex)
            {
                return Requests.Response(this, new ApiStatus(404), null, ex.Message);
            }
        }

        [HttpGet("GetAllCustomer")]
        public async Task<IActionResult> AllCustomer([FromQuery] PageInfo pageInfo)
        {
            try
            {
                var data = await _customerAppService.GetAllCustomers(pageInfo);
                if (data.Total < 1)
                {
                    return Requests.Response(this, new ApiStatus(404), null, $"database customer is empty");
                }
                return Requests.Response(this, new ApiStatus(200), data, "");
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
                var (isAdded, isMessage) = await _customerAppService.ForgotPassword(email);
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
                var (isAdded, isMessage) = await _customerAppService.UpdatePassword(password, email);
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

