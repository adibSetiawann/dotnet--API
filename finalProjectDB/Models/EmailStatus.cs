using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectDB
{
    public class EmailStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ValidationUserId { get; set; }
        public string? Email { get; set; }
        public string? OtpCode { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}