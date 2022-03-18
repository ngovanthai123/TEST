using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebLapTop.Models
{
    [Table("KHACHHANG")]
    public partial class Khachhang
    {
        public Khachhang()
        {
            Hoadons = new HashSet<Hoadon>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string HoVaTen { get; set; }
        [StringLength(255)]
        public string SoDienThoai { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [StringLength(255)]
        public string TenDangNhap { get; set; }
        [Required]
        [StringLength(255)]
        public string MatKhau { get; set; }
        [Required]
        [StringLength(255)]
        [Compare("MatKhau", ErrorMessage = "Xác nhận mật khẩu không chính xác!")]
        [DataType(DataType.Password)]
        public string XacNhanMatKhau { get; set; }
        [StringLength(255)]
        public string DiaChi { get; set; }

        [InverseProperty(nameof(Hoadon.IdkhachHangNavigation))]
        public virtual ICollection<Hoadon> Hoadons { get; set; }
    }
}
