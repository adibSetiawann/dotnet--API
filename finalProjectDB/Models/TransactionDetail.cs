namespace FinalProjectDB
{
    public class TransactionDetail
    {
        public List<VaNumber> va_numbers { get; set; }
        public List<PaymentAmount> payment_amounts { get; set; }
        public string transaction_time { get; set; }
        public string gross_amount { get; set; }
        public string currency { get; set; }
        public string order_id { get; set; }
        public string payment_type { get; set; }
        public string signature_key { get; set; }
        public string status_code { get; set; }
        public string transaction_id { get; set; }
        public string transaction_status { get; set; }
        public string fraud_status { get; set; }
        public string expire_time { get; set; }
        public string settlement_time { get; set; }
        public string status_message { get; set; }
        public string merchant_id { get; set; }
    }
}
