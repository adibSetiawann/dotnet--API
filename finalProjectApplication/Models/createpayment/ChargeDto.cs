namespace FinalProjectApplication
{
    public class ChargeDto
    {
        public string payment_type { get; set; }
        public TransactionDetails transaction_details { get; set; }
        public CustomerDetails customer_details { get; set; }
        public List<ItemDetail> item_details { get; set; }
        public BankTransfer bank_transfer { get; set; }
    }
}