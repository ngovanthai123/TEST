using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebLapTop.Models
{
    [Table("HOADON")]
    public partial class Hoadon
    {
        public Hoadon()
        {
            Chitiethoadons = new HashSet<Chitiethoadon>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("IDKhachHang")]
        public int? IdkhachHang { get; set; }
        [Column("IDNhanVien")]
        public int? IdnhanVien { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NgayLap { get; set; }
        [StringLength(255)]
        public string NoiNhan { get; set; }
        [StringLength(255)]
        public string SoDienThoai { get; set; }
        public int TinhTrang { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NgayXacNhan { get; set; }
        [Column(TypeName = "text")]
        public string GhiChu { get; set; }

        [ForeignKey(nameof(IdkhachHang))]
        [InverseProperty(nameof(Khachhang.Hoadons))]
        public virtual Khachhang IdkhachHangNavigation { get; set; }
        [ForeignKey(nameof(IdnhanVien))]
        [InverseProperty(nameof(Nhanvien.Hoadons))]
        public virtual Nhanvien IdnhanVienNavigation { get; set; }
        [InverseProperty(nameof(Chitiethoadon.IdhoaDonNavigation))]
        public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; }
    }
}
