using System.Data.Common;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using AutoMapper;
using FinalProjectDB;
using Newtonsoft.Json;

namespace FinalProjectApplication
{
    public class BillAppService : IBillAppService
    {
        private readonly PetCareContext _petCareContext;
        private IMapper _mapper;
        string baseUrlSandbox = "https://api.sandbox.midtrans.com";
        string authString = "U0ItTWlkLXNlcnZlci1sRHNueGdBVFUyOHVnRXJZTE5pZDZFeDA6";

        public BillAppService(PetCareContext PetCareContext, IMapper mapper)
        {
            _petCareContext = PetCareContext;
            _mapper = mapper;
        }

        public async Task<String> GetRandomNumber(string poNumber)
        {
            using var client = new HttpClient();
            string authString = "U0ItTWlkLXNlcnZlci1sRHNueGdBVFUyOHVnRXJZTE5pZDZFeDA6";
            var req = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://api.sandbox.midtrans.com/v2/{poNumber}/status"
            );
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            req.Headers.Authorization = new AuthenticationHeaderValue("Basic", authString);
            var z = client.Send(req);
            return await z.Content.ReadAsStringAsync();
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
            int day =  billDate.Day;
            int month = billDate.Month;
            int year = billDate.Year;
            var generator = new RandomGenerator();
            string randomString = generator.RandomString(1);
            // var bill = from bills in _petCareContext.Bill select bills;
            // bill = bill.Where(s => s.BillDate.Month.Equals(month));
            string poNumbers = $"TRSCMEOW{year}{month}{day.ToString("D2")}{randomString}";
            return poNumbers;
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
        public static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
        }

        private static HttpContent CreateHttpContent(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return httpContent;
        }

        public async Task<string> CreatePayment(CreatePaymentDto dto)
        {
            using (var client = new HttpClient())

            using (
                var content = new StringContent(
                    JsonConvert.SerializeObject(dto),
                    System.Text.Encoding.UTF8,
                    "application/json"
                )
            )
            using (
                var req = new HttpRequestMessage(
                    HttpMethod.Post,
                    $"{baseUrlSandbox}/v1/payment-links"
                )
            )
            using (var contentData = CreateHttpContent(dto))
            {
                req.Content = contentData;
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );
                req.Headers.Authorization = new AuthenticationHeaderValue("Basic", authString);
                using var responseMessage = await client.SendAsync(
                    req,
                    HttpCompletionOption.ResponseHeadersRead
                );
                return await responseMessage.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> CreateCharge(ChargeDto dto)
        {
            var temp = 0;
            dto.transaction_details.order_id = createBillNumber(DateTime.Now);
            foreach (var item in dto.item_details)
            {
                temp = item.price * item.quantity;
                dto.transaction_details.gross_amount += temp;
            }
            using (var client = new HttpClient())

            using (
                var content = new StringContent(
                    JsonConvert.SerializeObject(dto),
                    System.Text.Encoding.UTF8,
                    "application/json"
                )
            )
            using (var req = new HttpRequestMessage(HttpMethod.Post, $"{baseUrlSandbox}/v2/charge"))
            using (var contentData = CreateHttpContent(dto))
            {
                req.Content = contentData;
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );
                req.Headers.Authorization = new AuthenticationHeaderValue("Basic", authString);
                using var responseMessage = await client.SendAsync(
                    req,
                    HttpCompletionOption.ResponseHeadersRead
                );
                return await responseMessage.Content.ReadAsStringAsync();
            }
        }

        public async Task<string> CancelCharge(string orderId)
        {
            using (
                var req = new HttpRequestMessage(
                    HttpMethod.Post,
                    $"{baseUrlSandbox}/v2/{orderId}/cancel"
                )
            )
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json")
                );
                req.Headers.Authorization = new AuthenticationHeaderValue("Basic", authString);
                using var responseMessage = await client.SendAsync(
                    req,
                    HttpCompletionOption.ResponseHeadersRead
                );
                return await responseMessage.Content.ReadAsStringAsync();
            }
        }
    }
}
