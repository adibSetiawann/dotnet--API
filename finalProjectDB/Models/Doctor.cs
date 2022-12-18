using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectDB
{
    public class Doctor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoctorId { get; set; }

        [Required]
        public string DoctorName { get; set; }
        public string PasswordHash { get; set; }
        public int GenderId { get; set; }

        [Required]
        [Column(TypeName = "Varchar(10)")]
        public string MobilePhoneNumber { get; set; }
        public int PetTypeId { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public string Address { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public string CodePosId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdated { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual PetType PetType { get; set; }
        public virtual ICollection<BillDetail> BillDetail { get; set; }
        public virtual Province Province { get; set; }
        public virtual City City { get; set; }
    }
}
