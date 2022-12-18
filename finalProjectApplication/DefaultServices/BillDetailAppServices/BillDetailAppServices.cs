using System.Data.Common;
using AutoMapper;
using FinalProjectDB;

namespace FinalProjectApplication
{
    public class BillDetailAppService : IBillDetailAppService
    {
        private readonly PetCareContext _petCareContext;
        private IMapper _mapper;

        public BillDetailAppService(PetCareContext PetCareContext, IMapper mapper)
        {
            _petCareContext = PetCareContext;
            _mapper = mapper;
        }

        public async Task<(bool, string)> Create(CreateBillDetailDto model)
        {
            try
            {
                var billDetail = _mapper.Map<BillDetail>(model);
                await _petCareContext.Database.BeginTransactionAsync();
                _petCareContext.BillDetail.Add(billDetail);
                await _petCareContext.SaveChangesAsync();
                await _petCareContext.Entry(billDetail).GetDatabaseValuesAsync();

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
                await _petCareContext.Database.BeginTransactionAsync();
                var billDetailData = _petCareContext.BillDetail.FirstOrDefault(
                    w => w.BillDetailId == id
                );

                var billDetail = _mapper.Map<BillDetail>(billDetailData);
                _petCareContext.BillDetail.Remove(billDetail);
                await _petCareContext.SaveChangesAsync();
                return await Task.Run(() => (true, "Bill Detail Deleted"));
            }
            catch (DbException dbex)
            {
                await _petCareContext.Database.RollbackTransactionAsync();
                return await Task.Run(() => (false, dbex.Message));
            }
        }

        public List<BillDetailListDto> SearchBillDetail(int searchId)
        {
            var billDetailData = (
                from bill in _petCareContext.Bill
                join billDetail in _petCareContext.BillDetail on bill.BillId equals billDetail.BillId
                join branchoffice in _petCareContext.BranchOffice on bill.BranchOfficeId equals branchoffice.BranchOfficeId
                join customer in _petCareContext.Customer on bill.CustomerId equals customer.CustomerId
                join doctor in _petCareContext.Doctor
                    on billDetail.DoctorId equals doctor.DoctorId
                join pet in _petCareContext.Pet on billDetail.PetId equals pet.PetId
                join petType in _petCareContext.PetType on pet.PetTypeId equals petType.PetTypeId
                join carePrice in _petCareContext.CarePrice
                    on billDetail.CarePriceId equals carePrice.CarePriceId
                join careType in _petCareContext.CareType on carePrice.CareTypeId equals careType.CareTypeId
                where bill.BillId == searchId
                select new BillDetailListDto
                {
                    BillId = bill.BillId,
                    BillDetailId = billDetail.BillDetailId,
                    BranchOffice = branchoffice.BranchOffice_name,
                    CutomerName = customer.CustomerName,
                    DoctorName = doctor.DoctorName,
                    PetName = pet.PetName,
                    PetType = petType.Description,
                    CareType = careType.description,
                    CarePrice = carePrice.Price,
                    Status = bill.BillStatus
                    
                }
            ).ToList();
            return billDetailData;
        }

        public async Task<(bool, string)> Update(BillDetailListDto model)
        {
            try
            {
                await _petCareContext.Database.BeginTransactionAsync();
                var billDetailData = _petCareContext.BillDetail.FirstOrDefault(
                    w => w.BillDetailId == model.BillDetailId
                );
                var billDetail = _mapper.Map<BillDetail>(billDetailData);
                _petCareContext.BillDetail.Update(billDetail);
                await _petCareContext.SaveChangesAsync();
                await _petCareContext.Database.CommitTransactionAsync();
                return await Task.Run(() => (true, "Bill Detail Updated"));
            }
            catch (DbException dbex)
            {
                await _petCareContext.Database.RollbackTransactionAsync();
                return await Task.Run(() => (false, dbex.Message));
            }
        }
    }
}
