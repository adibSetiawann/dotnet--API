namespace FinalProjectApplication
{
    public class CreatePaymentDto
    {
        public TransactionDetails transaction_details { get; set; }
        public int usage_limit { get; set; }
        public List<ItemDetail> item_details { get; set; }
        public CustomerDetails customer_details { get; set; }
    }
}