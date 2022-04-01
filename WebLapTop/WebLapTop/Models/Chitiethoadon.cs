using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebLapTop.Models
{
    [Table("CHITIETHOADON")]
    public partial class Chitiethoadon
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("IDHoaDon")]
        public int? IdhoaDon { get; set; }
        [Column("IDSanPham")]
        public int? IdsanPham { get; set; }
        public int? SoLuongMua { get; set; }
        public int DonGia { get; set; }

        [ForeignKey(nameof(IdhoaDon))]
        [InverseProperty(nameof(Hoadon.Chitiethoadons))]
        public virtual Hoadon IdhoaDonNavigation { get; set; }
        [ForeignKey(nameof(IdsanPham))]
        [InverseProperty(nameof(Sanpham.Chitiethoadons))]
        public virtual Sanpham IdsanPhamNavigation { get; set; }
    }
}
