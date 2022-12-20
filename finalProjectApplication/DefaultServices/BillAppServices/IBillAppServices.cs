namespace FinalProjectApplication
{
    public interface IBillAppService
    {
        Task<(bool, string)> Create(CreateBillDto model);
        Task<(bool, string)> Update(int id);
        Task<(bool, string)> CancelBill(int id);
        Task<String> GetRandomNumber(string poNumber);
        Task<String> CreatePayment(CreatePaymentDto dto);
        Task<String> CreateCharge(ChargeDto dto);
        Task<String> CancelCharge(string orderId);
    }
}
