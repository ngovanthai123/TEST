using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLapTop.Data;
using WebLapTop.Models;

namespace WebLapTop.Controllers
{
    public class SearchController : Controller
    {
        private readonly WebLapTopContext _context;

        public SearchController(WebLapTopContext context)
        {

            _context = context;
        }
        public async Task<ActionResult> Search(string currentFilter, string searchString, int? pageNumber)

        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var sanpham = from s in _context.Sanphams
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                sanpham = sanpham.Where(s => s.TenSanPham.Contains(searchString)
                                       || s.IddongSanPhamNavigation.TenSanPham.Contains(searchString));
            }
            ViewBag.thongbao = sanpham.Count();
            int pageSize = 9;
            return View(await PaginatedList<Sanpham>.CreateAsync(sanpham.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        
    }
}
