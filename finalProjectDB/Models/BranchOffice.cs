using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProjectDB
{
    public class BranchOffice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BranchOfficeId { get; set; }

        [Required]
        public string BranchOffice_name { get; set; }

        [Required]
        [Column(TypeName = "Varchar(12)")]
        public string MobilePhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int ProvinceId { get; set; }
        public int CityId { get; set; }
        public int CodePosId { get; set; }
        public virtual Province Province { get; set; }
        public virtual City City { get; set; }
    }
}
