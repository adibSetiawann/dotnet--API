using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectDB
{
    public class Pet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PetId { get; set; }
        public string PetName { get; set; }
        public int GenderId { get; set; }
        public int PetTypeId { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual PetType PetType { get; set; }
        public virtual ICollection<BillDetail> BillDetail { get; set; }
    }
}
