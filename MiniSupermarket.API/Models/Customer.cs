using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniSupermarket.API.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống.")]
        [StringLength(100, ErrorMessage = "Tên khách hàng tối đa 100 ký tự.")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được để trống.")]
        [StringLength(15, ErrorMessage = "Số điện thoại tối đa 15 ký tự.")]
        [Column(TypeName = "varchar(15)")]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(200, ErrorMessage = "Địa chỉ tối đa 200 ký tự.")]
        public string? Address { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Điểm thưởng không được âm.")]
        public int RewardPoints { get; set; } = 0;

        [Required(ErrorMessage = "Hạng thẻ không được để trống.")]
        [StringLength(50, ErrorMessage = "Hạng thẻ tối đa 50 ký tự.")]
        public string MembershipRank { get; set; } = "Chuẩn";
    }
}