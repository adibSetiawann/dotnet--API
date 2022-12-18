using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using finalProjectApplication.DefaultServices.CustomerAppServices.Dto;
using FinalProjectApplication;

namespace finalProjectApplication.DefaultServices.CustomerAppServices
{
    public interface ICustomerAppService
    {
        Task<(bool, string)> Create(CreateCustomerDto model);
        Task<(bool, string)> Update(UpdateCustomerDto model);
        Task<(bool, string)> Delete(Guid id);
        Task<PageResult<CustomerListDto>> GetAllCustomers(PageInfo pageInfo);
        List<CustomerListDto> SearchCustomerName(string name);
        CustomerDto SearchCustomerPhone(string phone, string password);
        CreateCustomerDto SearchCustomerById(Guid searchId);
        Task<(bool, string)> ForgotPassword(string email);
        Task<(bool, string)> UpdatePassword(string password, string email);
    }
}
