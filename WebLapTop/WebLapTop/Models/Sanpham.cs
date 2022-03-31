using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebLapTop.Models
{
    [Table("SANPHAM")]
    public partial class Sanpham
    {
        public Sanpham()
        {
            Binhluans = new HashSet<Binhluan>();
            Chitiethoadons = new HashSet<Chitiethoadon>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("IDNoiSanXuat")]
        public int? IdnoiSanXuat { get; set; }
        [Column("IDDongSanPham")]
        public int? IddongSanPham { get; set; }
        [Column("IDLoaiPhuKien")]
        public int? IdloaiPhuKien { get; set; }
        [Required]
        [StringLength(255)]
        public string TenSanPham { get; set; }
        [StringLength(255)]
        public string AnhSanPham { get; set; }
        [StringLength(255)]
        public string Mau { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public int GiaBan { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = false)]
        public int GiaKhuyenMai { get; set; }
        [Column(TypeName = "date")]
        public DateTime? NgayBatDauKhuyenMai { get; set; }
        [Column(TypeName = "date")]
        public DateTime? NgayKetThucKhuyenMai { get; set; }
        public int? SoLuong { get; set; }
        [Column(TypeName = "text")]
        public string MoTa { get; set; }
        [StringLength(255)]
        public string BaoHanh { get; set; }

        [ForeignKey(nameof(IddongSanPham))]
        [InverseProperty(nameof(Dongsanpham.Sanphams))]
        public virtual Dongsanpham IddongSanPhamNavigation { get; set; }
        [ForeignKey(nameof(IdloaiPhuKien))]
        [InverseProperty(nameof(Loaiphukien.Sanphams))]
        public virtual Loaiphukien IdloaiPhuKienNavigation { get; set; }
        [ForeignKey(nameof(IdnoiSanXuat))]
        [InverseProperty(nameof(Noisanxuat.Sanphams))]
        public virtual Noisanxuat IdnoiSanXuatNavigation { get; set; }
        [InverseProperty(nameof(Binhluan.IdsanPhamNavigation))]
        public virtual ICollection<Binhluan> Binhluans { get; set; }
        [InverseProperty(nameof(Chitiethoadon.IdsanPhamNavigation))]
        public virtual ICollection<Chitiethoadon> Chitiethoadons { get; set; }
    }
}
