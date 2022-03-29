using WebLapTop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLapTop.Data;

namespace WebLapTop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BaoCaoController : Controller
    {
        private readonly WebLapTopContext _context;

        public BaoCaoController(WebLapTopContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(int? month = 0)
        {
            if (month == 0)
            {
                month = DateTime.Today.Month;
            }
            var firstDayOfMonth = new DateTime(DateTime.Today.Year, (int)month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var listHoaDon = _context.Hoadons.Where(x => x.NgayLap >= firstDayOfMonth && x.NgayLap <= lastDayOfMonth);
            var soluongsanpham = 0;
            foreach (var item in listHoaDon)
            {
                
                var listChiTietHoaDon = await _context.Chitiethoadons.Where(x => x.IdhoaDon == item.Id).ToListAsync();
                if (!listChiTietHoaDon.Any()) continue;
                soluongsanpham += listChiTietHoaDon.Select(x => (int)x.SoLuongMua).Sum();
            }
            var sanpham = await _context.Sanphams.ToListAsync();
            ViewData["month"] = month;
            ViewData["tonkho"] = sanpham.Select(x => x.SoLuong).Sum();
            ViewData["soluonghoadon"] = listHoaDon.Count();
            ViewData["soluongsanpham"] = soluongsanpham;
            ViewData["doanhthu"] = listHoaDon.Select(x => x.TongGia).Sum();

            return View();

        }
    }
}
