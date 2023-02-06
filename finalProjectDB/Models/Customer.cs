using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectDB
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; }

        [Required]
        public string CustomerName { get; set; }
        public int GenderId { get; set; }

        [Required]
        [Column(TypeName = "Varchar(12)")]
        public string MobilePhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public bool IsDeleted { get; set; }
        public string Address { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public int CodePosId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdated { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual ICollection<Pet> Pet { get; set; }
        public virtual ICollection<Bill> Bill { get; set; }
        public virtual Province Province { get; set; }
        public virtual City City { get; set; }
    }
}
