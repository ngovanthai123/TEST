using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebLapTop.Data;
using WebLapTop.Models;

namespace WebLapTop.Controllers
{
    public class ImagesSanPhamController : Controller
    {
        private readonly WebLapTopContext _context;

        public ImagesSanPhamController(WebLapTopContext context)
        {
            _context = context;
        }

        // GET: ImagesSanPham
        public async Task<IActionResult> Index()
        {
            var webLapTopContext = _context.ImagesSanPhams.Include(i => i.IdSanphamNavigation);
            return View(await webLapTopContext.ToListAsync());
        }

        // GET: ImagesSanPham/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagesSanPham = await _context.ImagesSanPhams
                .Include(i => i.IdSanphamNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imagesSanPham == null)
            {
                return NotFound();
            }

            return View(imagesSanPham);
        }

        // GET: ImagesSanPham/Create
        public IActionResult Create()
        {
            ViewData["IdSanpham"] = new SelectList(_context.Sanphams, "Id", "TenSanPham");
            return View();
        }

        // POST: ImagesSanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdSanpham,Image")] ImagesSanPham imagesSanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(imagesSanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdSanpham"] = new SelectList(_context.Sanphams, "Id", "TenSanPham", imagesSanPham.IdSanpham);
            return View(imagesSanPham);
        }

        // GET: ImagesSanPham/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagesSanPham = await _context.ImagesSanPhams.FindAsync(id);
            if (imagesSanPham == null)
            {
                return NotFound();
            }
            ViewData["IdSanpham"] = new SelectList(_context.Sanphams, "Id", "TenSanPham", imagesSanPham.IdSanpham);
            return View(imagesSanPham);
        }

        // POST: ImagesSanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdSanpham,Image")] ImagesSanPham imagesSanPham)
        {
            if (id != imagesSanPham.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imagesSanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImagesSanPhamExists(imagesSanPham.Id))
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
            ViewData["IdSanpham"] = new SelectList(_context.Sanphams, "Id", "TenSanPham", imagesSanPham.IdSanpham);
            return View(imagesSanPham);
        }

        // GET: ImagesSanPham/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imagesSanPham = await _context.ImagesSanPhams
                .Include(i => i.IdSanphamNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imagesSanPham == null)
            {
                return NotFound();
            }

            return View(imagesSanPham);
        }

        // POST: ImagesSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imagesSanPham = await _context.ImagesSanPhams.FindAsync(id);
            _context.ImagesSanPhams.Remove(imagesSanPham);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImagesSanPhamExists(int id)
        {
            return _context.ImagesSanPhams.Any(e => e.Id == id);
        }
    }
}
