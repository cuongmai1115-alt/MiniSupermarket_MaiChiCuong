using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required]
        [StringLength(50)]
        public string OrderCode { get; set; } = string.Empty;   // VD: HD20261001-001

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(50)]
        public string CashierUsername { get; set; } = string.Empty;

        public int TotalItems { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FinalAmount { get; set; }

        public int RewardPoints { get; set; } = 0;

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer? Customer { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}