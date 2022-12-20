using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectDB
{
    public class Bill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillId { get; set; }

        [Required]
        public string BillNumber { get; set; }
        public DateTime BillDate { get; set; }
        public string BillStatus { get; set; }
        public Guid CustomerId { get; set; }
        public int BranchOfficeId { get; set; }
        public virtual BranchOffice BranchOffice { get; set; }
        public virtual ICollection<BillDetail> BillDetail { get; set; }
        public string OrderId { get; set; }
        public string PaymentType { get; set; }

        // public string PaymentMethod { get; set; }
        public DateTime TransactionTime { get; set; }
        public string TransactionStatus { get; set; }
        public string VaNumber { get; set; }
        public string Bank { get; set; }
    }
}
