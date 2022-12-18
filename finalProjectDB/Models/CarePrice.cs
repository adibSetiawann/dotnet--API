using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectDB
{
    public class CarePrice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarePriceId { get; set; }

        public int CareTypeId { get; set; }
        public int Price { get; set; }
        public virtual CareType CareType { get; set; }
        public virtual ICollection<BillDetail> BillDetail { get; set; }
    }
}