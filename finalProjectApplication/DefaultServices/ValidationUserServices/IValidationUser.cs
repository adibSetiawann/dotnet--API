using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using finalProjectApplication.DefaultServices.ValidationUserServices.Dto;

namespace finalProjectApplication.DefaultServices.ValidationUserServices
{
    public interface IValidationUser
    {
        Task<(bool, string)> CreateOTP(string email);
        Task<(bool, string)> UpdateStatus(UpdateStatus model);
        // Task<(bool, string)> SendOTPEmail(string email);
    }
}