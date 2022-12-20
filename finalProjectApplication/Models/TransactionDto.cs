namespace FinalProjectApplication
{
    public class TransactionDto
    {
        public string payment_type { get; set; }
        public TransactionDetails transaction_details { get; set; }
        // public CreditCard credit_card { get; set; }
        // public CustomerDetails customer_details { get; set; }
        public BankTransfer bank_transfer { get; set; }
    }
}