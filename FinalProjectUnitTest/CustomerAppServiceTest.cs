using finalProjectApplication.DefaultServices.CustomerAppServices;
using finalProjectApplication.DefaultServices.CustomerAppServices.Dto;
using FinalProjectApplication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectUnitTest
{
    public class CustomerAppServiceTest : IClassFixture<Startup>
    {
        private ServiceProvider _serviceProvider;

        public CustomerAppServiceTest(Startup fixture)
        {
            _serviceProvider = fixture.ServiceProvider;
        }

        [Fact]
        public void CreateCustomer()
        {
            var service = _serviceProvider.GetService<ICustomerAppService>();

            CustomerDto createCustomerDto = new CustomerDto();
            createCustomerDto.CustomerName = "Budi";
            createCustomerDto.MobilePhoneNumber = "082123123123";
            createCustomerDto.Email = "Budiganteng@gmail.com";

            var result = service.Create(createCustomerDto);

            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllCustomer()
        {
            var service = _serviceProvider.GetService<ICustomerAppService>();

            PageInfo pageInfo = new PageInfo();
            pageInfo.Page = 1;
            pageInfo.PageSize = 5;

            var result = service.GetAllCustomers(pageInfo);

            Assert.NotNull(result);
        }


        [Fact]
        public void UpdateCustomer()
        {
            var service = _serviceProvider.GetService<ICustomerAppService>();

            CustomerDto updateCustomerDto = new CustomerDto();
            updateCustomerDto.CustomerName = "YAWA";
            updateCustomerDto.MobilePhoneNumber = "02222222332223";
            updateCustomerDto.Email = "YawaKayo@gmail.com";

            var result = service.Update(updateCustomerDto);

            Assert.NotNull(result);
        }

        //[Fact]
        //public async void DeleteCustomer()
        //{
        //    var service = _serviceProvider.GetService<ICustomerAppService>();

        //    Guid id = ;// place guid id here 

        //    var result = await service.Delete(id);

        //    Assert.True(result.Item1);
        //}
    }
}
