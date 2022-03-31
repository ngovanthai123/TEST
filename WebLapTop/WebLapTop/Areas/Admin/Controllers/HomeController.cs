﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebLapTop.Data;
using WebLapTop.Library;
using WebLapTop.Models;

namespace WebLapTop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        const string SessionMand = "_Id";
        const string SessionHoten = "_Hoten";
        const string SessionTenDN = "_TenDN";
        const string SessionQuyen = "_Quyen";
        const string SessionAnhDaiDien = "_AnhDaiDien";

        private readonly ILogger<HomeController> _logger;
        private readonly WebLapTopContext _context;

        private WebLapTopContext db = new WebLapTopContext();
      

        public HomeController(ILogger<HomeController> logger, WebLapTopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Logout()
        {
            // Xóa SESSION
            HttpContext.Session.Remove("_Id");
            HttpContext.Session.Remove("_Hoten");
            HttpContext.Session.Remove("_TenDN");
            HttpContext.Session.Remove("_Quyen");
            HttpContext.Session.Remove("_AnhDaiDien");
            // Quy về trang chủ
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Login()
        {

            ModelState.AddModelError("LoginError", "");
            return View();
        }

        // POST: Home/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ManegeLogin nhanvien)
        {
            if (ModelState.IsValid)
            {
                string mahoamatkhau = SHA1.ComputeHash(nhanvien.MatKhau);
                var tk = db.Nhanviens.Where(r => r.TenDangNhap == nhanvien.TenDangNhap && r.MatKhau == mahoamatkhau).SingleOrDefault();

                if (tk == null)
                {
                    ModelState.AddModelError("LoginError", "Tên đăng nhập hoặc mật khẩu không chính xác!");
                    return View(nhanvien);
                }
                else
                {
                    // Đăng ký SESSION

                    HttpContext.Session.SetInt32(SessionMand, tk.Id);
                    HttpContext.Session.SetString(SessionHoten, tk.HoVaTen);
                    HttpContext.Session.SetString(SessionTenDN, tk.TenDangNhap);
                    HttpContext.Session.SetInt32(SessionQuyen, tk.Quyen);
                    HttpContext.Session.SetString(SessionAnhDaiDien, tk.AnhDaiDien);
                    //if (tk.ChucVu == 1)
                    //    return RedirectToAction("Index", "Home", new { area = "Admin" });
                    //if (tk.ChucVu == 0)
                    //    return RedirectToAction("Index", "Home", new { area = "Admin" });

                    return RedirectToAction("Index", "Home");
                }
            }


            return View(nhanvien);
        }

        // change password
        public ActionResult ChangePasswordNV()
        {
            ModelState.AddModelError("LoginError", "");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePasswordNV(ChangePasswordNV model)
        {
            if (ModelState.IsValid)
            {
                int id = (int)HttpContext.Session.GetInt32("_Id");

                string matKhauMaHoa = SHA1.ComputeHash(model.MatKhauCu);
                string XacNhanMatKhauMaHoa = SHA1.ComputeHash(model.XacNhanMatKhau);
                string matkhaumoi;
                var taiKhoan = _context.Nhanviens.Where(r => r.Id == id && r.MatKhau == matKhauMaHoa).SingleOrDefault();
                if (taiKhoan == null)
                {
                    ModelState.AddModelError("LoginError", "Mật khẩu cũ không chính xác!");
                    return View(model);
                }
                else
                {
                    matkhaumoi = SHA1.ComputeHash(model.MatKhauMoi);
                    Nhanvien n = _context.Nhanviens.Find(id);
                    n.MatKhau = matkhaumoi;
                    n.XacNhanMatKhau = matkhaumoi;

                    _context.Entry(n).State = EntityState.Modified;
                    _context.SaveChanges();

                    ModelState.AddModelError("LoginError", "Đổi mật khẩu thành công!");

                }
            }
            return RedirectToAction("Logout", "Home");

        }

    }
}
