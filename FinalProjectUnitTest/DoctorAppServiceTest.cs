using finalProjectApplication.DefaultServices.CustomerAppServices.Dto;
using finalProjectApplication.DefaultServices.CustomerAppServices;
using FinalProjectApplication;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectUnitTest
{
    public class DoctorAppServiceTest : IClassFixture<Startup>
    {
        private ServiceProvider _serviceProvider;

        public DoctorAppServiceTest(Startup fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }


        [Fact]
        public void CreateDoctor()
        {
            var service = _serviceProvider.GetService<IDoctorAppService>();

            CreateDoctorDto createDoctorDto = new CreateDoctorDto();
            createDoctorDto.DoctorName = "DCR-001";
            createDoctorDto.MobilePhoneNumber = "082211221122";
            createDoctorDto.Email = "DCR001@gmail.com";

            var result = service.Create(createDoctorDto);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllDoctors()
        {
            var service = _serviceProvider.GetService<IDoctorAppService>();

            PageInfo pageInfo = new PageInfo();
            pageInfo.Page = 1;
            pageInfo.PageSize = 5;

            var result = service.GetAllDoctors(pageInfo);

            Assert.NotNull(result);
        }

        [Fact]
        public void UpdateDoctor()
        {
            var service = _serviceProvider.GetService<IDoctorAppService>();

            UpdateDoctorDto updateCustomerDto = new UpdateDoctorDto();
            updateCustomerDto.DoctorName = "DCT-002";
            updateCustomerDto.MobilePhoneNumber = "0082217722";
            updateCustomerDto.Email = "Halodok@gmail.com";

            var result = service.Update(updateCustomerDto);

            Assert.NotNull(result);
        }

        [Fact]
        public async void DeleteDoctor()
        {
            var service = _serviceProvider.GetService<IDoctorAppService>();

            int id = 1;

            var result = await service.Delete(id);

            Assert.True(result.Item1);
        }
    }
}
