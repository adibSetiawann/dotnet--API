
namespace FinalProjectApplication
{
    public interface IBillAppService
    {
        Task<(bool, string)>Create(CreateBillDto model);
        Task<(bool, string)> Update(int id);
        Task<(bool, string)> CancelBill(int id);
    }
}
