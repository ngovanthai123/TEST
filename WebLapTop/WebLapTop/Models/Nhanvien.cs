using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebLapTop.Models
{
    [Table("NHANVIEN")]
    public partial class Nhanvien
    {
        public Nhanvien()
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
        [StringLength(255)]
        public string AnhDaiDien { get; set; }
        [Required]
        [StringLength(255)]
        public string TenDangNhap { get; set; }
        [Required]
        [StringLength(255)]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        [Compare("MatKhau", ErrorMessage = "Xác nhận mật khẩu không chính xác!")]
        [DataType(DataType.Password)]
        [StringLength(255)]
        public string XacNhanMatKhau { get; set; }
        public int Quyen { get; set; }

        [InverseProperty(nameof(Hoadon.IdnhanVienNavigation))]
        public virtual ICollection<Hoadon> Hoadons { get; set; }
    }
}
