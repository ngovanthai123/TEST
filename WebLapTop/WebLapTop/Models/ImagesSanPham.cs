using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebLapTop.Models
{
    [Table("ImagesSanPham")]
    public partial class ImagesSanPham
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        public int? IdSanpham { get; set; }
        [StringLength(255)]
        public string Image { get; set; }

        [ForeignKey(nameof(IdSanpham))]
        [InverseProperty(nameof(Sanpham.ImagesSanPhams))]
        public virtual Sanpham IdSanphamNavigation { get; set; }
    }
}
