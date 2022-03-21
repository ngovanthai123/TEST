using WebLapTop.Data;
using WebLapTop.Models;
using WebLapTop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebLapTop.Controllers
{
    public class BanTin : Controller
    {
        private WebLapTopContext db = new WebLapTopContext();
        private readonly WebLapTopContext _context;
        private readonly IProduct _product;
        public BanTin(WebLapTopContext context, IProduct product)

        {
            _context = context;
            _product = product;


        }
        public IActionResult Index(string timkiem, int? page = 0)
        {

            int limit = 5;
            int start;
            if (page > 0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            start = (int)(page - 1) * limit;

            ViewBag.pageCurrent = page;


            int totalProduct = _product.totalProduct();

            ViewBag.totalProduct = totalProduct;

            ViewBag.numberPage = _product.numberPage(totalProduct, limit);
            var dress = _context.Baiviets
                .Skip((int)((page - 1) * limit)).Take(limit);
            /* var dress = (from sp in db.Sanphams
                          join dong in db.Dongsanphams on sp.IddongSanPham equals dong.Id
                          join dm in db.Danhmucsanphams on dong.IddanhMuc equals dm.Id
                          where (dm.Id == 1)
                          select new 
                          {
                              sp.TenSanPham,
                              sp.AnhSanPham,
                              sp.GiaBan,
                              sp.GiaKhuyenMai,
                              sp.SoLuong,
                              sp.NgayBatDauKhuyenMai,
                              sp.NgayKetThucKhuyenMai,
                          });
 */

            if (!String.IsNullOrEmpty(timkiem))
            {
                dress = dress.Where(s => s.TieuDe.Contains(timkiem));
            }
            ViewBag.thongbao = dress.Count();
            return View(dress);
        }




        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Baiviets
                .Include(s => s.Id)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }
    }
}
