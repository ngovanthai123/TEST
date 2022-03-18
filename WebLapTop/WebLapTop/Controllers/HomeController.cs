using AspNetCoreHero.ToastNotification.Abstractions;
using WebLapTop.Data;
using WebLapTop.Library;
using WebLapTop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WebLapTop.Controllers
{
    public class HomeController : Controller
    {
        const string SessionMand = "_Id";
        const string SessionHoten = "_HoVaTen";
        const string SessionTenDN = "_TenDN";

        public const string CARTKEY = "shopcart";
        public INotyfService _notifyService { get; }

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
            var dress = _context.Sanphams;
            return View(dress);
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

        // Lưu danh sách CartItem trong giỏ hàng vào session
        void SaveCartSession(List<CartItem> lst)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(lst);
            //session.SetString("shopcart", jsoncart);
            HttpContext.Response.Cookies.Append(HttpContext.Session.GetInt32(SessionMand).ToString(), jsoncart);
        }

        // Đọc danh sách CartItem từ session
        List<CartItem> GetCartItems()
        {
            var jsoncart = HttpContext.Request.Cookies[HttpContext.Session.GetInt32("_Id").ToString()];
            if (!string.IsNullOrEmpty(jsoncart))
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);

            }
            return new List<CartItem>();
        }

        // Cho hàng vào giỏ
        public async Task<IActionResult> AddToCart(int id, int quanlity)
        {
            if (HttpContext.Session.GetInt32("_Id") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var product = await _context.Sanphams
                .FirstOrDefaultAsync(m => m.Id == id);
                if (product == null)
                {
                    return NotFound("Sản phẩm không tồn tại");
                }
                var cart = GetCartItems();
                var item = cart.Find(p => p.Sanpham.Id == id);
                if (item != null)
                {
                    if (quanlity < product.SoLuong)
                    {
                        if (quanlity == 0) item.Quantity++;
                        else item.Quantity += quanlity;
                    }
                    /*ModelState.AddModelError("ErrorCart", "Số lượng không còn đủ");
                    item.Quantity = 1;*/
                }
                else
                {
                    if (quanlity < product.SoLuong)
                    {
                        if (quanlity == 0)
                        {
                            cart.Add(new CartItem() { Sanpham = product, Quantity = 1 });
                        }
                        else
                            cart.Add(new CartItem() { Sanpham = product, Quantity = quanlity });
                    }
                    else
                    {
                        ModelState.AddModelError("ErrorCart", "Số lượng không còn đủ");
                        if (product.SoLuong > 0) cart.Add(new CartItem() { Sanpham = product, Quantity = 1 });
                    }

                }
                SaveCartSession(cart);
                return RedirectToAction(nameof(Cart));
            }
        }

        // Chuyển đến view xem giỏ hàng
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

        // Xóa một mặt hàng khỏi giỏ
        public IActionResult RemoveItem(int id)
        {
            var cart = GetCartItems();
            var item = cart.Find(p => p.Sanpham.Id == id);
            if (item != null)
            {
                cart.Remove(item);
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

        // Cập nhật số lượng một mặt hàng trong giỏ
        public IActionResult UpdateItem(int id, int quantity)
        {
            var cart = GetCartItems();
            var item = cart.Find(p => p.Sanpham.Id == id);
            var product = _context.Sanphams
                .FirstOrDefault(m => m.Id == id);
            if (item != null)
            {
                if (quantity < product.SoLuong)
                    item.Quantity = quantity;

                else
                {

                    if (product.SoLuong > 1)
                    {
                        item.Quantity = 1;
                    }
                }
            }
            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

        public IActionResult CheckOut()
        {
            return View(GetCartItems());
        }

        //lưu thông tin hóa đơn
        [HttpPost]
        public async Task<IActionResult> CreateBill(int cusname, string cusadd, string cusphone)
        {

            var session = HttpContext.Session;
            var b = new Hoadon();
            b.NgayLap = DateTime.Now;
            b.IdkhachHang = HttpContext.Session.GetInt32(SessionMand);
            if (b.IdkhachHang == null)
            {
                return Redirect("/Home/Login");
            }
            b.SoDienThoai = cusphone;
            b.NoiNhan = cusadd;

            b.TinhTrang = 0;
            _context.Add(b);
            await _context.SaveChangesAsync();
            //them billdetails
            var cart = GetCartItems();
            int amount = 0;
            int total = 0;

            foreach (var i in cart)
            {
                var d = new Chitiethoadon();
                d.IdhoaDon = b.Id;
                d.IdsanPham = i.Sanpham.Id;
                var sanpham = _context.Sanphams.FirstOrDefault(t => t.Id == d.IdsanPham);
                sanpham.SoLuong -= i.Quantity;
                await _context.SaveChangesAsync();
                if (i.Sanpham.GiaKhuyenMai > 0)
                {
                    d.DonGia = i.Sanpham.GiaKhuyenMai;
                }
                else
                {
                    d.DonGia = i.Sanpham.GiaBan;
                }
                d.SoLuongMua = Convert.ToInt16(i.Quantity);

                amount = i.Sanpham.GiaBan * i.Quantity;
                total += amount;
                _context.Add(d);
            }

            await _context.SaveChangesAsync();

            HttpContext.Response.Cookies.Append(HttpContext.Session.GetInt32(SessionMand).ToString(), "");

            //chuyển hướng 
            return RedirectToAction("Index", "SanPham");

        }

    
        public ActionResult Login()
        {
            ModelState.AddModelError("LoginError", "");
            return View();
        }


        // POST: Home/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(KhachHangLogin khachHang)
        {
            if (ModelState.IsValid)
            {
                string mahoamatkhau = SHA1.ComputeHash(khachHang.MatKhau);
                var taiKhoan = db.Khachhangs.Where(r => r.TenDangNhap == khachHang.TenDangNhap && r.MatKhau == mahoamatkhau).SingleOrDefault();

                if (taiKhoan == null)
                {
                    ModelState.AddModelError("LoginError", "Tên đăng nhập hoặc mật khẩu không chính xác!");
                    return View(khachHang);
                }
                else
                {
                    // Đăng ký SESSION

                    HttpContext.Session.SetInt32(SessionMand, taiKhoan.Id);
                    HttpContext.Session.SetString(SessionHoten, taiKhoan.HoVaTen);
                    HttpContext.Session.SetString(SessionTenDN, taiKhoan.TenDangNhap);


                    // Quay về trang chủ
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(khachHang);
        }

        public ActionResult Logout()
        {
            // Xóa SESSION
            HttpContext.Session.Remove("_Id");
            HttpContext.Session.Remove("_Hoten");
            HttpContext.Session.Remove("_TenDN");
            HttpContext.Session.Remove(CARTKEY);

            // Quy về trang chủ
            return RedirectToAction("Login", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp([Bind("Id,HoVaTen,SoDienThoai,DiaChi,Email,TenDangNhap,MatKhau,XacNhanMatKhau")] Khachhang khachHang)
        {
            if (ModelState.IsValid)
            {
                var check = db.Khachhangs.FirstOrDefault(r => r.TenDangNhap == khachHang.TenDangNhap);
                if (check == null)
                {

                    khachHang.MatKhau = SHA1.ComputeHash(khachHang.MatKhau);
                    khachHang.XacNhanMatKhau = SHA1.ComputeHash(khachHang.XacNhanMatKhau);
                    _context.Add(khachHang);
                    await _context.SaveChangesAsync();
                    ModelState.AddModelError("SignUpError", "Đăng ký thành công!");
                    return RedirectToAction(nameof(Login));

                }
                else
                {
                    ViewBag.error = "Tên đăng nhập đã tồn tại !!! Bạn hãy chọn tên khác!!!!";
                    return View();
                }
            }
            return View();
        }
        public IActionResult SignUp()
        {
            ModelState.AddModelError("SignUpError", "");
            return View();
        }

        // change password
        public ActionResult ChangePassword()
        {
            ModelState.AddModelError("LoginError", "");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                int id = (int)HttpContext.Session.GetInt32("_Id");

                string matKhauMaHoa = SHA1.ComputeHash(model.MatKhauCu);
                string XacNhanMatKhauMaHoa = SHA1.ComputeHash(model.XacNhanMatKhau);
                string matkhaumoi;
                var taiKhoan = _context.Khachhangs.Where(r => r.Id == id && r.MatKhau == matKhauMaHoa).SingleOrDefault();
                if (taiKhoan == null)
                {
                    ModelState.AddModelError("LoginError", "Mật khẩu cũ không chính xác!");
                    return View(model);
                }
                else
                {
                    matkhaumoi = SHA1.ComputeHash(model.MatKhauMoi);
                    Khachhang n = _context.Khachhangs.Find(id);
                    n.MatKhau = matkhaumoi;
                    n.XacNhanMatKhau = matkhaumoi;

                    _context.Entry(n).State = EntityState.Modified;
                    _context.SaveChanges();

                    ModelState.AddModelError("LoginError", "Đổi mật khẩu thành công!");

                }
            }
            return RedirectToAction("Logout", "Home");
        }

        public ActionResult DonHangCuaToi()
        {
            int makh = Convert.ToInt32(HttpContext.Session.GetInt32("_Id").ToString());
            var DonHangCuaToi = (from sp in db.Sanphams
                                 join chitiet in db.Chitiethoadons on sp.Id equals chitiet.IdsanPham
                                 join dhang in db.Hoadons on chitiet.IdhoaDon equals dhang.Id
                                 join kh in db.Khachhangs on dhang.IdkhachHang equals kh.Id
                                 where (kh.Id == makh)

                                 select new DonHangCuaToi()
                                 {
                                     TenSanPham = sp.TenSanPham,
                                     AnhSanPham = sp.AnhSanPham,
                                     DonGia = chitiet.DonGia,
                                     IdDH = dhang.Id,
                                     SoLuong = chitiet.SoLuongMua,
                                     NgayDatHang = dhang.NgayLap,
                                     TinhTrang = dhang.TinhTrang
                                 }).OrderByDescending(dhang => dhang.NgayDatHang).ToList();

            return View(DonHangCuaToi);
        }

    }
}
