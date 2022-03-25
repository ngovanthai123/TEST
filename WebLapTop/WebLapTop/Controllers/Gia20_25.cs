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
    public class Gia20_25 : Controller
    {
        private WebLapTopContext db = new WebLapTopContext();
        private readonly WebLapTopContext _context;
        private readonly IProduct _product;
        public Gia20_25(WebLapTopContext context, IProduct product)

        {
            _context = context;
            _product = product;


        }
        public IActionResult Index(string timkiem, int? page = 0)
        {

            int limit = 6;
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
            var dress = _context.Sanphams
                .Where(sp => sp.GiaBan > 20000000 && sp.GiaBan <= 25000000 || sp.GiaKhuyenMai > 0 && sp.GiaKhuyenMai > 20000000 && sp.GiaKhuyenMai <= 25000000)
            .Skip((int)((page - 1) * limit)).Take(limit);

            if (!String.IsNullOrEmpty(timkiem))
            {
                dress = dress.Where(s => s.TenSanPham.Contains(timkiem));
            }
            return View(dress);
        }




        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanphams
                .Include(s => s.IdnoiSanXuatNavigation)
                .Include(s => s.IddongSanPhamNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }
    }
}
