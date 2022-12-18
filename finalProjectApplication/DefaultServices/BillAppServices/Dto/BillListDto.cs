namespace FinalProjectApplication
{
    public class BillListDto
    {
        public int BillId { get; set; }
        public string BillNumber { get; set; }
        public DateTime BillDate { get; set; }
        public string BillStatus { get; set; }
        public Guid CustomerId { get; set; }
        public int BranchOfficeId { get; set; }
    }
}
