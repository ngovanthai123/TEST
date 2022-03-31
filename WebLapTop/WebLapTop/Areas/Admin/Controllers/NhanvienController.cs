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
using WebLapTop.Library;
using WebLapTop.Models;

namespace WebLapTop.Controllers
{
    [Area("Admin")]
    public class NhanvienController : Controller
    {
        private readonly WebLapTopContext _context;

        public NhanvienController(WebLapTopContext context)
        {
            _context = context;
        }

        // GET: Nhanvien
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nhanviens.ToListAsync());
        }

        // GET: Nhanvien/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return View(nhanvien);
        }

        // GET: Nhanvien/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nhanvien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("Id,HoVaTen,SoDienThoai,Email,AnhDaiDien,TenDangNhap,MatKhau,XacNhanMatKhau,Quyen")] Nhanvien nhanvien)
        {
            if (ModelState.IsValid)
            {
                nhanvien.AnhDaiDien = Upload(file);
                nhanvien.MatKhau = SHA1.ComputeHash(nhanvien.MatKhau);
                nhanvien.XacNhanMatKhau = SHA1.ComputeHash(nhanvien.XacNhanMatKhau);
                _context.Add(nhanvien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhanvien);
        }

        // GET: Nhanvien/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens.FindAsync(id);
            if (nhanvien == null)
            {
                return NotFound();
            }
            return View(nhanvien);
        }

        // POST: Nhanvien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int id, [Bind("Id,HoVaTen,SoDienThoai,Email,AnhDaiDien,TenDangNhap,MatKhau,XacNhanMatKhau,Quyen")] Nhanvien nhanvien)
        {
            if (id != nhanvien.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    nhanvien.MatKhau = SHA1.ComputeHash(nhanvien.MatKhau);
                    nhanvien.XacNhanMatKhau = SHA1.ComputeHash(nhanvien.XacNhanMatKhau);
                    if (file != null)
                    {
                        nhanvien.AnhDaiDien = Upload(file);
                    }
                    _context.Update(nhanvien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanvienExists(nhanvien.Id))
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
            return View(nhanvien);
        }

        // GET: Nhanvien/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return View(nhanvien);
        }

        // POST: Nhanvien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhanvien = await _context.Nhanviens.FindAsync(id);
            _context.Nhanviens.Remove(nhanvien);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanvienExists(int id)
        {
            return _context.Nhanviens.Any(e => e.Id == id);
        }

        //Load ảnh sản phẩm
        public string Upload(IFormFile file)
        {
            string UploadFileName = null;
            if (file != null)
            {
                UploadFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var path = $"wwwroot\\anhNV\\{ UploadFileName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

            }
            return UploadFileName;
        }
    }
}
