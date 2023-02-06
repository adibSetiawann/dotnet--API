using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using finalProjectApplication.DefaultServices.ValidationUserServices.Dto;
using FinalProjectApplication;
using FinalProjectDB;

namespace finalProjectApplication.DefaultServices.ValidationUserServices
{
    public class ValidationUser : IValidationUser
    {
        private readonly PetCareContext _petCareContext;
        private readonly IMapper _mapper;

        public ValidationUser(PetCareContext petCareContext, IMapper mapper)
        {
            _petCareContext = petCareContext;
            _mapper = mapper;
        }
        public async Task<(bool, string)> CreateOTP(string model)
        {
            try
            {
                var userdata = new EmailStatus();
                userdata.Email = model;
                userdata.Status = "pending";
                var generator = new RandomGenerator();
                userdata.OtpCode = generator.RandomString(4);
                await _petCareContext.Database.BeginTransactionAsync();
                _petCareContext.EmailStatus.Add(userdata);
                await _petCareContext.SaveChangesAsync();
                await _petCareContext.Database.CommitTransactionAsync();

                return await Task.Run(() => (true, "send otp success"));
            }
            catch (DbException dbex)
            {
                await _petCareContext.Database.RollbackTransactionAsync();
                return await Task.Run(() => (false, dbex.Message));
            }
        }

        public async Task<(bool, string)> UpdateStatus(UpdateStatus model)
        {
            try
            {
                var data = (from item in _petCareContext.EmailStatus
                            where item.OtpCode.Equals(model.OTPCode) && item.Email.Equals(model.Email)
                            select item).ToList();
                if (data.Count > 0)
                {
                    var timeNow = DateTime.Now;
                    var registeredTime = DateTime.Parse(data[0].CreatedAt.ToString("yyyy-MM-dd HH:mm"));

                    if (timeNow.Subtract(registeredTime) >= TimeSpan.FromMinutes(10))
                    {
                        data[0].Status = "expired";
                        var userdata = _mapper.Map<EmailStatus>(data[0]);
                        await _petCareContext.Database.BeginTransactionAsync();
                        _petCareContext.EmailStatus.Update(data[0]);
                        await _petCareContext.SaveChangesAsync();
                        await _petCareContext.Database.CommitTransactionAsync();
                        return await Task.Run(() => (true, "OTP Code is expired"));
                    }
                    else
                    {
                        data[0].Status = "verified";
                        var userdata = _mapper.Map<EmailStatus>(data[0]);
                        await _petCareContext.Database.BeginTransactionAsync();
                        _petCareContext.EmailStatus.Update(data[0]);
                        await _petCareContext.SaveChangesAsync();
                        await _petCareContext.Database.CommitTransactionAsync();
                        return await Task.Run(() => (true, "user email verified"));
                    }

                }
                else
                {
                    return await Task.Run(() => (false, "verificarion user failed check if email is registered"));
                }
            }
            catch (DbException dbex)
            {
                await _petCareContext.Database.RollbackTransactionAsync();
                return await Task.Run(() => (false, dbex.Message));
            }
        }
    }
}