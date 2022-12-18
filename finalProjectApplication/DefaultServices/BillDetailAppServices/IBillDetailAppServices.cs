namespace FinalProjectApplication
{
    public interface IBillDetailAppService
    {
        Task<(bool, string)> Create(CreateBillDetailDto model);
        Task<(bool, string)> Update(BillDetailListDto model);
        Task<(bool, string)> Delete(Guid id);
        List<BillDetailListDto> SearchBillDetail(int searchId);
    }
}
