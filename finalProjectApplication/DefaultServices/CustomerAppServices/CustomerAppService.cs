using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using finalProjectApplication.DefaultServices.CustomerAppServices.Dto;
using FinalProjectApplication;
using FinalProjectDB;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;

namespace finalProjectApplication.DefaultServices.CustomerAppServices
{
    public class CustomerAppService : ICustomerAppService
    {
        readonly private PetCareContext _petCareContext;
        readonly private IPetAppService _petAppService;
        private IMapper _mapper;

        public CustomerAppService(PetCareContext petCareContext, IMapper mapper)
        {
            _petCareContext = petCareContext;
            _mapper = mapper;
        }

        public async Task<(bool, string)> Create(CreateCustomerDto model)
        {
            try
            {
                var customer = _mapper.Map<Customer>(model);
                customer.IsDeleted = false;
                customer.PasswordHash = BCrypt.Net.BCrypt.HashPassword(customer.PasswordHash);

                await _petCareContext.Database.BeginTransactionAsync();
                _petCareContext.Customer.Add(customer);
                await _petCareContext.SaveChangesAsync();

                await _petCareContext.Database.CommitTransactionAsync();
                return await Task.Run(() => (true, "Created"));
            }
            catch (DbException dbex)
            {
                await _petCareContext.Database.RollbackTransactionAsync();
                return (false, dbex.Message);
            }
        }

        public async Task<(bool, string)> Delete(Guid id)
        {
            try
            {
                var customer = await _petCareContext.Customer.FirstOrDefaultAsync(
                    w => w.CustomerId == id
                );
                if (customer != null)
                {
                    customer.IsDeleted = true;
                    await _petCareContext.Database.BeginTransactionAsync();
                    _petCareContext.Customer.Update(customer);
                    await _petCareContext.SaveChangesAsync();
                    await _petCareContext.Database.CommitTransactionAsync();
                }
                return await Task.Run(() => (true, "Success"));
            }
            catch (DbException dbex)
            {
                await _petCareContext.Database.RollbackTransactionAsync();
                return await Task.Run(() => (false, dbex.Message));
            }
        }

        public async Task<(bool, string)> ForgotPassword(string emaill)
        {
            var customer = _petCareContext.Customer.FirstOrDefault(
                w => w.Email.ToLower() == emaill.ToLower()
            );
            if (customer != null)
            {
                string body = "Klik link to update password";
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("bart.hahn77@ethereal.email"));
                email.To.Add(MailboxAddress.Parse($"{emaill}"));

                email.Subject = "Forgot Password ";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

                using var smtp = new SmtpClient();
                smtp.Connect(
                    "smtp.ethereal.email",
                    587,
                    MailKit.Security.SecureSocketOptions.StartTls
                );
                smtp.Authenticate("bart.hahn77@ethereal.email", "VsedCQAexnn5XHZunX");
                smtp.Send(email);
                smtp.Disconnect(true);

                return await Task.Run(() => (true, "Success"));
            }

            return await Task.Run(() => (false, "Not found"));
        }

        public async Task<PageResult<CustomerListDto>> GetAllCustomers(PageInfo pageInfo)
        {
            // var petList = ;
            var pageResult = new PageResult<CustomerListDto> { Data = (
                    from customer in _petCareContext.Customer
                    join gender in _petCareContext.Gender
                        on customer.GenderId equals gender.GenderId
                    join province in _petCareContext.Province
                        on customer.ProvinceId equals province.ProvinceId
                    join city in _petCareContext.City on customer.CityId equals city.CityId
                    where customer.IsDeleted == false
                    select new CustomerListDto
                    {
                        CustomerId = customer.CustomerId,
                        CustomerName = customer.CustomerName,
                        Gender = gender.Description,
                        MobilePhoneNumber = customer.MobilePhoneNumber,
                        Email = customer.Email,
                        IsDeleted = customer.IsDeleted,
                        // Pet = _petAppService.SearchPetByCustomer(customer.CustomerId),
                        Address = customer.Address,
                        Province = province.Description,
                        City = city.Descriptionn,
                        CodePosId = customer.CodePosId,
                        CreatedAt = customer.CreatedAt,
                        LastUpdated = customer.LastUpdated
                    }
                ).Skip(pageInfo.Skip).Take(pageInfo.PageSize).OrderBy(w => w.CustomerId), Total = _petCareContext.Customer.Where(s => s.IsDeleted == false).Count() };
            return await Task.Run(() => pageResult);
        }

        public CreateCustomerDto SearchCustomerById(Guid searchId)
        {
            var customer = from data in _petCareContext.Customer select data;
            customer = customer.Where(s => s.CustomerId.Equals(searchId));
            var customerDto = _mapper.Map<CreateCustomerDto>(customer);
            return customerDto;
        }

        public List<CustomerListDto> SearchCustomerName(string name)
        {
            var results = (
                from customer in _petCareContext.Customer
                join gender in _petCareContext.Gender on customer.GenderId equals gender.GenderId
                join province in _petCareContext.Province
                    on customer.ProvinceId equals province.ProvinceId
                join city in _petCareContext.City on customer.CityId equals city.CityId
                where customer.CustomerName.Contains($"{name}") && customer.IsDeleted == false
                select new CustomerListDto
                {
                    CustomerId = customer.CustomerId,
                    CustomerName = customer.CustomerName,
                    Gender = gender.Description,
                    MobilePhoneNumber = customer.MobilePhoneNumber,
                    Email = customer.Email,
                    IsDeleted = customer.IsDeleted,
                    Address = customer.Address,
                    Province = province.Description,
                    City = city.Descriptionn,
                    CodePosId = customer.CodePosId,
                    CreatedAt = customer.CreatedAt,
                    LastUpdated = customer.LastUpdated
                }
            ).ToList();
            return results;
        }

        public CustomerDto SearchCustomerPhone(string phone, string password)
        {
            var customer = _petCareContext.Customer.FirstOrDefault(
                w => w.MobilePhoneNumber.ToLower() == phone.ToLower()
            );
            bool isValidPass = BCrypt.Net.BCrypt.Verify(password, customer.PasswordHash);
            if (isValidPass)
            {
                var customerDto = _mapper.Map<CustomerDto>(customer);
                return customerDto;
            }
            return null;
        }

        public async Task<(bool, string)> SendEmail()
        {
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();
            
            await scheduler.Start();
            
            IJobDetail job = JobBuilder.Create<SendEmail>().WithIdentity("job1", "group1").Build();

            ITrigger trigger = TriggerBuilder
                .Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
                .Build();

            await scheduler.ScheduleJob(job, trigger);
            await Task.Delay(TimeSpan.FromSeconds(60));
            await scheduler.Shutdown();

            return await Task.Run(() => (true, "Success"));
        }

        public async Task<(bool, string)> Update(UpdateCustomerDto model)
        {
            try
            {
                var customerData = _petCareContext.Customer.FirstOrDefault(
                    w => w.CustomerId == model.CustomerId
                );
                customerData.CustomerName = model.CustomerName;
                customerData.MobilePhoneNumber = model.MobilePhoneNumber;
                customerData.Email = model.Email;
                customerData.GenderId = model.GenderId;
                customerData.Address = model.Address;
                customerData.ProvinceId = model.ProvinceId;
                customerData.CityId = model.CityId;
                customerData.CodePosId = model.CodePosId;
                await _petCareContext.Database.BeginTransactionAsync();
                _petCareContext.Customer.Update(customerData);
                await _petCareContext.SaveChangesAsync();
                await _petCareContext.Database.CommitTransactionAsync();
                return await Task.Run(() => (true, "Success"));
            }
            catch (DbException dbex)
            {
                await _petCareContext.Database.RollbackTransactionAsync();
                return await Task.Run(() => (false, dbex.Message));
            }
        }
        public async Task<(bool, string)> UpdatePassword(string password, string email)
        {
            var customerData = _petCareContext.Customer.FirstOrDefault(
                w => w.Email.ToLower() == email.ToLower()
            );
            if (customerData != null)
            {
                customerData.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
                await _petCareContext.Database.BeginTransactionAsync();
                _petCareContext.Customer.Update(customerData);
                await _petCareContext.SaveChangesAsync();
                await _petCareContext.Database.CommitTransactionAsync();
                return await Task.Run(() => (true, "Success"));
            }
            return await Task.Run(() => (false, "Not found"));
        }
    }
}
