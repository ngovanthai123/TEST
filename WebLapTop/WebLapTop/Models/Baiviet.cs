using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebLapTop.Models
{
    [Table("BAIVIET")]
    public partial class Baiviet
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(255)]
        public string TieuDe { get; set; }
        [Column(TypeName = "ntext")]
        public string TomTat { get; set; }
        [Column(TypeName = "ntext")]
        public string NoiDung { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? NgayDang { get; set; }
        public int? LuotXem { get; set; }
        [StringLength(255)]
        public string AnhBaiViet { get; set; }
    }
}
