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
    [Area("Admin")]
    public class HoadonController : Controller
    {
        private readonly WebLapTopContext _context;

        public HoadonController(WebLapTopContext context)
        {
            _context = context;
        }

        // GET: Hoadon
        public async Task<IActionResult> Index()
        {
            var webLapTopContext = _context.Hoadons.Include(h => h.IdkhachHangNavigation).Include(h => h.IdnhanVienNavigation);
            return View(await webLapTopContext.ToListAsync());
        }

        // GET: Hoadon/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoadon = await _context.Hoadons
                .Include(h => h.IdkhachHangNavigation)
                .Include(h => h.IdnhanVienNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoadon == null)
            {
                return NotFound();
            }

            return View(hoadon);
        }

        // GET: Hoadon/Create
        public IActionResult Create()
        {
            ViewData["IdkhachHang"] = new SelectList(_context.Khachhangs, "Id", "HoVaTen");
            ViewData["IdnhanVien"] = new SelectList(_context.Nhanviens, "Id", "HoVaTen");
            return View();
        }

        // POST: Hoadon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdkhachHang,IdnhanVien,NgayLap,NoiNhan,SoDienThoai,TinhTrang,NgayXacNhan,GhiChu")] Hoadon hoadon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoadon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdkhachHang"] = new SelectList(_context.Khachhangs, "Id", "HoVaTen", hoadon.IdkhachHang);
            ViewData["IdnhanVien"] = new SelectList(_context.Nhanviens, "Id", "HoVaTen", hoadon.IdnhanVien);
            return View(hoadon);
        }

        // GET: Hoadon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoadon = await _context.Hoadons.FindAsync(id);
            if (hoadon == null)
            {
                return NotFound();
            }
            ViewData["IdkhachHang"] = new SelectList(_context.Khachhangs, "Id", "HoVaTen", hoadon.IdkhachHang);
            ViewData["IdnhanVien"] = new SelectList(_context.Nhanviens, "Id", "HoVaTen", hoadon.IdnhanVien);
            return View(hoadon);
        }

        // POST: Hoadon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdkhachHang,IdnhanVien,NgayLap,NoiNhan,SoDienThoai,TinhTrang,NgayXacNhan,GhiChu")] Hoadon hoadon)
        {
            if (id != hoadon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoadon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoadonExists(hoadon.Id))
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
            ViewData["IdkhachHang"] = new SelectList(_context.Khachhangs, "Id", "HoVaTen", hoadon.IdkhachHang);
            ViewData["IdnhanVien"] = new SelectList(_context.Nhanviens, "Id", "HoVaTen", hoadon.IdnhanVien);
            return View(hoadon);
        }

        // GET: Hoadon/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hoadon = await _context.Hoadons
                .Include(h => h.IdkhachHangNavigation)
                .Include(h => h.IdnhanVienNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hoadon == null)
            {
                return NotFound();
            }

            return View(hoadon);
        }

        // POST: Hoadon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hoadon = await _context.Hoadons.FindAsync(id);
            _context.Hoadons.Remove(hoadon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoadonExists(int id)
        {
            return _context.Hoadons.Any(e => e.Id == id);
        }
    }
}
