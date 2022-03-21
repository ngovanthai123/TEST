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
    public class BaivietController : Controller
    {
        private readonly WebLapTopContext _context;

        public BaivietController(WebLapTopContext context)
        {
            _context = context;
        }

        // GET: Baiviet
        public async Task<IActionResult> Index()
        {
            return View(await _context.Baiviets.ToListAsync());
        }

        // GET: Baiviet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiviet = await _context.Baiviets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baiviet == null)
            {
                return NotFound();
            }

            return View(baiviet);
        }

        // GET: Baiviet/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Baiviet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file,[Bind("Id,TieuDe,TomTat,NoiDung,NgayDang,LuotXem,AnhBaiViet")] Baiviet baiviet)
        {
            if (ModelState.IsValid)
            {
                baiviet.AnhBaiViet = Upload(file);
                _context.Add(baiviet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(baiviet);
        }

        // GET: Baiviet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiviet = await _context.Baiviets.FindAsync(id);
            if (baiviet == null)
            {
                return NotFound();
            }
            return View(baiviet);
        }

        // POST: Baiviet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int id, [Bind("Id,TieuDe,TomTat,NoiDung,NgayDang,LuotXem,AnhBaiViet")] Baiviet baiviet)
        {
            if (id != baiviet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        baiviet.AnhBaiViet = Upload(file);
                    }
                    _context.Update(baiviet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BaivietExists(baiviet.Id))
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
            return View(baiviet);
        }

        // GET: Baiviet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var baiviet = await _context.Baiviets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (baiviet == null)
            {
                return NotFound();
            }

            return View(baiviet);
        }

        // POST: Baiviet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var baiviet = await _context.Baiviets.FindAsync(id);
            _context.Baiviets.Remove(baiviet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BaivietExists(int id)
        {
            return _context.Baiviets.Any(e => e.Id == id);
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
