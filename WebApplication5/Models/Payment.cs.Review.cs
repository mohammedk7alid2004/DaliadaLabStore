using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        public int? OrderId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? PaymentDate { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PaymentAmount { get; set; }

        [StringLength(50)]

        public string? PaymentMethod { get; set; }

        [StringLength(50)]

        public string? PaymentStatus { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order? Order { get; set; }

       

    }
}
