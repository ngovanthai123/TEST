using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebLapTop.Data;
using WebLapTop.Models;

namespace WebLapTop.Controllers
{
    [Area("Admin")]
    public class SanphamController : Controller
    {
        private WebLapTopContext db = new WebLapTopContext();
        private readonly WebLapTopContext _context;

        public SanphamController(WebLapTopContext context)
        {
            _context = context;
        }

        // GET: Sanpham
        public async Task<IActionResult> Index()
        {
            var webLapTopContext = _context.Sanphams.Include(s => s.IddongSanPhamNavigation).Include(s => s.IdloaiPhuKienNavigation).Include(s => s.IdnoiSanXuatNavigation);
            return View(await webLapTopContext.ToListAsync());
        }

        // GET: Sanpham/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                Sanpham bv = db.Sanphams.Find(id);
                bv.LuotXem = bv.LuotXem + 1;
                db.Entry(bv).State = EntityState.Modified;
                db.SaveChanges();
            }
            var sanpham = await _context.Sanphams
                .Include(s => s.IddongSanPhamNavigation)
                .Include(s => s.IdloaiPhuKienNavigation)
                .Include(s => s.IdnoiSanXuatNavigation)
                
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }

        // GET: Sanpham/Create
        public IActionResult Create()
        {
            ViewData["IddongSanPham"] = new SelectList(_context.Dongsanphams, "Id", "TenSanPham");
            ViewData["IdloaiPhuKien"] = new SelectList(_context.Loaiphukiens, "Id", "TenPhuKien");
            ViewData["IdnoiSanXuat"] = new SelectList(_context.Noisanxuats, "Id", "TenNoiSanXuat");
            return View();
        }

        // POST: Sanpham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<IFormFile> file, [Bind("Id,IdnoiSanXuat,IddongSanPham,IdloaiPhuKien,TenSanPham,AnhSanPham,Mau,GiaBan,GiaKhuyenMai,NgayBatDauKhuyenMai,NgayKetThucKhuyenMai,SoLuong,MoTa,BaoHanh,LuotXem")] Sanpham sanpham)
        {
            if (ModelState.IsValid)
            {
                sanpham.LuotXem = 0;
                _context.Sanphams.Add(sanpham);
                await _context.SaveChangesAsync();
                if (file.Count() != 0)
                {
                    var imagesProduct = new List<ImagesSanPham>();
                    foreach (var item in file)
                    {
                        imagesProduct.Add(
                            new ImagesSanPham()
                            {
                                IdSanpham = sanpham.Id,
                                Image = Upload(item)
                            });
                    }

                    var p = _context.Sanphams.Find(sanpham.Id);
                    p.AnhSanPham = imagesProduct[0].Image;

                    _context.ImagesSanPhams.AddRange(imagesProduct);
                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IddongSanPham"] = new SelectList(_context.Dongsanphams, "Id", "TenSanPham", sanpham.IddongSanPham);
            ViewData["IdloaiPhuKien"] = new SelectList(_context.Loaiphukiens, "Id", "TenPhuKien", sanpham.IdloaiPhuKien);
            ViewData["IdnoiSanXuat"] = new SelectList(_context.Noisanxuats, "Id", "TenNoiSanXuat", sanpham.IdnoiSanXuat);
            return View(sanpham);
        }

        // GET: Sanpham/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanpham = await _context.Sanphams.FindAsync(id);
            if (sanpham == null)
            {
                return NotFound();
            }
            ViewData["IddongSanPham"] = new SelectList(_context.Dongsanphams, "Id", "TenSanPham", sanpham.IddongSanPham);
            ViewData["IdloaiPhuKien"] = new SelectList(_context.Loaiphukiens, "Id", "TenPhuKien", sanpham.IdloaiPhuKien);
            ViewData["IdnoiSanXuat"] = new SelectList(_context.Noisanxuats, "Id", "TenNoiSanXuat", sanpham.IdnoiSanXuat);
            return View(sanpham);
        }

        // POST: Sanpham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int id, [Bind("Id,IdnoiSanXuat,IddongSanPham,IdloaiPhuKien,TenSanPham,AnhSanPham,Mau,GiaBan,GiaKhuyenMai,NgayBatDauKhuyenMai,NgayKetThucKhuyenMai,SoLuong,MoTa,BaoHanh,LuotXem")] Sanpham sanpham)
        {
            if (id != sanpham.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        sanpham.AnhSanPham = Upload(file);
                    }
                    _context.Update(sanpham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanphamExists(sanpham.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IddongSanPham"] = new SelectList(_context.Dongsanphams, "Id", "TenSanPham", sanpham.IddongSanPham);
            ViewData["IdloaiPhuKien"] = new SelectList(_context.Loaiphukiens, "Id", "TenPhuKien", sanpham.IdloaiPhuKien);
            ViewData["IdnoiSanXuat"] = new SelectList(_context.Noisanxuats, "Id", "TenNoiSanXuat", sanpham.IdnoiSanXuat);
            return View(sanpham);
        }

        // GET: Sanpham/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var sanpham = await _context.Sanphams
                .Include(s => s.IddongSanPhamNavigation)
                .Include(s => s.IdloaiPhuKienNavigation)
                .Include(s => s.IdnoiSanXuatNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sanpham == null)
            {
                return NotFound();
            }

            return View(sanpham);
        }

        // POST: Sanpham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanpham = await _context.Sanphams.FindAsync(id);
            _context.Sanphams.Remove(sanpham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanphamExists(int id)
        {
            return _context.Sanphams.Any(e => e.Id == id);
        }

        //Load ảnh sản phẩm
        public string Upload(IFormFile file)
        {
            string UploadFileName = null;
            if (file != null)
            {
                UploadFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var path = $"wwwroot\\images\\{ UploadFileName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

            }
            return UploadFileName;
        }
    }
}
