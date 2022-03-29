using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLapTop.Models
{
    public class CartItem
    {
        public Sanpham Sanpham { get; set; }
        public int Quantity { get; set; }
    }

    public class CartLove
    {
        public Sanpham Product1 { get; set; }
        public int Quantity1 { get; set; }

    }

    public class DonHangCuaToi
    {
        public Hoadon Hoadon { get; set; }
        public int IdDH { get; set; }
        public string TenSanPham { get; set; }
        public string AnhSanPham { get; set; }
        public Nullable<int> DonGia { get; set; }
        public int? SoLuong { get; set; }
        public Nullable<System.DateTime> NgayDatHang { get; set; }
        public int? TinhTrang { get; set; }

    }
}
