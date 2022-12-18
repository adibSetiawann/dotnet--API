using System.Data.Common;
using AutoMapper;
using FinalProjectDB;

namespace FinalProjectApplication
{
    public class BillAppService : IBillAppService
    {
        private readonly PetCareContext _petCareContext;
        private IMapper _mapper;

        public BillAppService(PetCareContext PetCareContext, IMapper mapper)
        {
            _petCareContext = PetCareContext;
            _mapper = mapper;
        }

        public async Task<(bool, string)> Create(CreateBillDto model)
        {
            try
            {
                var bill = _mapper.Map<Bill>(model);
                bill.BillDate = DateTime.Now;
                bill.BillStatus = "PENDING";
                bill.BillNumber = createBillNumber(bill.BillDate);

                await _petCareContext.Database.BeginTransactionAsync();
                _petCareContext.Bill.Add(bill);
                await _petCareContext.SaveChangesAsync();
                await _petCareContext.Entry(bill).GetDatabaseValuesAsync();

                await _petCareContext.Database.CommitTransactionAsync();
                return await Task.Run(() => (true, "Created"));
            }
            catch (DbException dbex)
            {
                await _petCareContext.Database.RollbackTransactionAsync();
                return (false, dbex.Message);
            }
        }

        private string createBillNumber(DateTime billDate)
        {
            var month = billDate.Month;
            var bill = from bills in _petCareContext.Bill select bills;
            bill = bill.Where(s => s.BillDate.Month.Equals(month));
            var x = bill.Count() + 1;
            int decimalLength = x.ToString("D").Length + 3;
            string poNumber = $"TRSCMEOW-{month}{x.ToString("D" + decimalLength.ToString())}";
            return poNumber;
        }
        public async Task<(bool, string)> Update(int id)
        {
            try
            {
                await _petCareContext.Database.BeginTransactionAsync();
                var billx = _petCareContext.Bill.FirstOrDefault(w => w.BillId == id);
                billx.BillStatus = "SUCCESS";
                var bill = _mapper.Map<Bill>(billx);
                _petCareContext.Bill.Update(bill);
                await _petCareContext.SaveChangesAsync();
                await _petCareContext.Database.CommitTransactionAsync();
                return await Task.Run(() => (true, "Updated"));
            }
            catch (DbException dbex)
            {
                await _petCareContext.Database.RollbackTransactionAsync();
                return await Task.Run(() => (false, dbex.Message));
            }
        }

        public async Task<(bool, string)> CancelBill(int id)
        {
            try
            {
                await _petCareContext.Database.BeginTransactionAsync();
                var billx = _petCareContext.Bill.FirstOrDefault(w => w.BillId == id);
                billx.BillStatus = "CANCEL";
                var bill = _mapper.Map<Bill>(billx);
                _petCareContext.Bill.Update(bill);
                await _petCareContext.SaveChangesAsync();
                await _petCareContext.Database.CommitTransactionAsync();
                return await Task.Run(() => (true, "Canceled"));
            }
            catch (DbException dbex)
            {
                await _petCareContext.Database.RollbackTransactionAsync();
                return await Task.Run(() => (false, dbex.Message));
            }
        }
    }
}
