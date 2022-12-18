using System.ComponentModel.DataAnnotations;

namespace FinalProjectDB
{
    public class BillDetail
    {
        [Key]
        public Guid BillDetailId { get; set; }
        public int BillId { get; set; }
        public int DoctorId { get; set; }
        public int PetId { get; set; }
        public int CarePriceId { get; set; }
    }
}
