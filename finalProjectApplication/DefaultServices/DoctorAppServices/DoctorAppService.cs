using System.Data.Common;
using AutoMapper;
using FinalProjectDB;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace FinalProjectApplication
{
    public class DoctorAppService : IDoctorAppService
    {
        private readonly PetCareContext _petCareContext;
        private IMapper _mapper;

        public DoctorAppService(PetCareContext PetCareContext, IMapper mapper)
        {
            _petCareContext = PetCareContext;
            _mapper = mapper;
        }

        public async Task<(bool, string)> Create(CreateDoctorDto model)
        {
            try
            {
                var Doctor = _mapper.Map<Doctor>(model);
                Doctor.IsDeleted = false;
                Doctor.PasswordHash = BCrypt.Net.BCrypt.HashPassword(Doctor.PasswordHash);

                await _petCareContext.Database.BeginTransactionAsync();
                _petCareContext.Doctor.Add(Doctor);
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

        public async Task<(bool, string)> Delete(int id)
        {
            try
            {
                var doctor = await _petCareContext.Doctor.FirstOrDefaultAsync(
                    w => w.DoctorId == id
                );
                if (doctor != null)
                {
                    doctor.IsDeleted = true;
                    await _petCareContext.Database.BeginTransactionAsync();
                    _petCareContext.Doctor.Update(doctor);
                    await _petCareContext.SaveChangesAsync();
                    await _petCareContext.Database.CommitTransactionAsync();
                }
                return await Task.Run(() => (true, "Sucess"));
            }
            catch (DbException dbex)
            {
                await _petCareContext.Database.RollbackTransactionAsync();
                return await Task.Run(() => (false, dbex.Message));
            }
        }

        public async Task<PageResult<DoctorListDto>> GetAllDoctors(PageInfo pageInfo)
        {
            var pageResult = new PageResult<DoctorListDto> { Data = (
                    from doctor in _petCareContext.Doctor
                    join gender in _petCareContext.Gender on doctor.GenderId equals gender.GenderId
                    join petType in _petCareContext.PetType
                        on doctor.PetTypeId equals petType.PetTypeId
                    join province in _petCareContext.Province
                        on doctor.ProvinceId equals province.ProvinceId
                    join city in _petCareContext.City on doctor.CityId equals city.CityId
                    where doctor.IsDeleted == false
                    select new DoctorListDto
                    {
                        DoctorId = doctor.DoctorId,
                        DoctorName = doctor.DoctorName,
                        Gender = gender.Description,
                        Spesialis = petType.Description,
                        MobilePhoneNumber = doctor.MobilePhoneNumber,
                        Address = doctor.Address,
                        Province = province.Description,
                        City = city.Descriptionn
                    }
                ).Skip(pageInfo.Skip).Take(pageInfo.PageSize).OrderBy(w => w.DoctorId), Total = _petCareContext.Doctor.Where(s => s.IsDeleted == false).Count() };
            return await Task.Run(() => pageResult);
        }

        public List<DoctorListDto> SearchDoctorById(int id)
        {
            var doctorData = (
                from doctor in _petCareContext.Doctor
                join gender in _petCareContext.Gender on doctor.GenderId equals gender.GenderId
                join petType in _petCareContext.PetType on doctor.PetTypeId equals petType.PetTypeId
                join province in _petCareContext.Province
                    on doctor.ProvinceId equals province.ProvinceId
                join city in _petCareContext.City on doctor.CityId equals city.CityId
                where doctor.DoctorId == id && doctor.IsDeleted == false
                select new DoctorListDto
                {
                    DoctorId = doctor.DoctorId,
                    DoctorName = doctor.DoctorName,
                    Gender = gender.Description,
                    Spesialis = petType.Description,
                    MobilePhoneNumber = doctor.MobilePhoneNumber,
                    Address = doctor.Address,
                    Province = province.Description,
                    City = city.Descriptionn
                }
            ).ToList();
            return doctorData;
        }

        public List<DoctorListDto> SearchDoctorName(string name)
        {
            var results = (
                from doctor in _petCareContext.Doctor
                join gender in _petCareContext.Gender on doctor.GenderId equals gender.GenderId
                join petType in _petCareContext.PetType on doctor.PetTypeId equals petType.PetTypeId
                join province in _petCareContext.Province
                    on doctor.ProvinceId equals province.ProvinceId
                join city in _petCareContext.City on doctor.CityId equals city.CityId
                where doctor.DoctorName.Contains($"{name.ToLower()}") && doctor.IsDeleted == false
                select new DoctorListDto
                {
                    DoctorId = doctor.DoctorId,
                    DoctorName = doctor.DoctorName,
                    Gender = gender.Description,
                    Spesialis = petType.Description,
                    MobilePhoneNumber = doctor.MobilePhoneNumber,
                    Address = doctor.Address,
                    Province = province.Description,
                    City = city.Descriptionn
                }
            ).ToList();
            return results;
        }

        public async Task<(bool, string)> Update(UpdateDoctorDto model)
        {
            try
            {
                var doctorData = _petCareContext.Doctor.FirstOrDefault(
                    w => w.DoctorId == model.DoctorId
                );
                doctorData.DoctorName = model.DoctorName;
                doctorData.MobilePhoneNumber = model.MobilePhoneNumber;
                doctorData.Email = model.Email;
                doctorData.GenderId = model.GenderId;
                doctorData.PetTypeId = model.PetTypeId;
                doctorData.Address = model.Address;
                doctorData.ProvinceId = model.ProvinceId;
                doctorData.CityId = model.CityId;
                doctorData.CodePosId = model.CodePosId;
                await _petCareContext.Database.BeginTransactionAsync();
                _petCareContext.Doctor.Update(doctorData);
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

        public DoctorListDto SearchDoctorPhone(string phone, string password)
        {
            var doctor = _petCareContext.Doctor.FirstOrDefault(
                w => w.MobilePhoneNumber.ToLower() == phone.ToLower()
            );
            bool isValidPass = BCrypt.Net.BCrypt.Verify(password, doctor.PasswordHash);
            if (isValidPass)
            {
                var doctorDto = _mapper.Map<DoctorListDto>(doctor);
                return doctorDto;
            }
            return null;
        }

        public List<DoctorListDto> SearchDoctorByCity(int cityId)
        {
            var results = (
                from doctor in _petCareContext.Doctor
                join gender in _petCareContext.Gender on doctor.GenderId equals gender.GenderId
                join petType in _petCareContext.PetType on doctor.PetTypeId equals petType.PetTypeId
                join province in _petCareContext.Province
                    on doctor.ProvinceId equals province.ProvinceId
                join city in _petCareContext.City on doctor.CityId equals city.CityId
                where doctor.CityId.Equals(cityId) && doctor.IsDeleted == false
                select new DoctorListDto
                {
                    DoctorId = doctor.DoctorId,
                    DoctorName = doctor.DoctorName,
                    Gender = gender.Description,
                    Spesialis = petType.Description,
                    MobilePhoneNumber = doctor.MobilePhoneNumber,
                    Address = doctor.Address,
                    Province = province.Description,
                    City = city.Descriptionn
                }
            ).ToList();
            return results;
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
                email.To.Add(MailboxAddress.Parse("bart.hahn77@ethereal.email"));

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

        public async Task<(bool, string)> UpdatePassword(string password, string email)
        {
            var doctorData = _petCareContext.Doctor.FirstOrDefault(
                w => w.Email.ToLower() == email.ToLower()
            );
            if (doctorData != null)
            {
                doctorData.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
                await _petCareContext.Database.BeginTransactionAsync();
                _petCareContext.Doctor.Update(doctorData);
                await _petCareContext.SaveChangesAsync();
                await _petCareContext.Database.CommitTransactionAsync();
                return await Task.Run(() => (true, "Success"));
            }
            return await Task.Run(() => (false, "Not found"));
        }
    }
}
