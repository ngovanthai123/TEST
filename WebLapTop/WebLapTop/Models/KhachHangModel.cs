using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebLapTop.Models
{
    public class KhachHangEdit
    {
        public KhachHangEdit()
        {

        }
        public KhachHangEdit(KhachHangEdit kh)
        {
            Id = kh.Id;
            HoVaTen = kh.HoVaTen;
            SoDienThoai = kh.SoDienThoai;
            DiaChi = kh.DiaChi;
            TenDangNhap = kh.TenDangNhap;
            MatKhau = kh.MatKhau;
            XacNhanMatKhau = kh.XacNhanMatKhau;
            Email = kh.Email;
        }
        public int Id { get; set; }
        public string HoVaTen { get; set; }
        public int SoDienThoai { get; set; }
        public string DiaChi { get; set; }
        public string Email { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string XacNhanMatKhau { get; set; }
    }

    public class KhachHangSignUp
    {
        public int Id { get; set; }
        [Display(Name = "Họ tên khách hàng")]
        [Required(ErrorMessage = "Họ tên không được bỏ trống!")]
        public string HoVaTen { get; set; }
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại không được bỏ trống!")]
        public string SoDienThoai { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ không được bỏ trống!")]
        public string DiaChi { get; set; }
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống!")]
        public string TenDangNhap { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        public string MatKhau { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        [Compare("MatKhau", ErrorMessage = "Xác nhận mật khẩu không chính xác!")]
        [DataType(DataType.Password)]
        public string XacNhanMatKhau { get; set; }

        public string Email { get; set; }
    }

    public class KhachHangLogin
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Tên đăng nhập không được bỏ trống!")]
        public string TenDangNhap { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Mật khẩu không được bỏ trống!")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

    }

    public class ChangePassword
    {
        [Display(Name = "Mật khẩu cũ")]
        [DataType(DataType.Password)]
        public string MatKhauCu { get; set; }

        [Display(Name = "Mật khẩu mới")]
        [DataType(DataType.Password)]
        public string MatKhauMoi { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        public string XacNhanMatKhau { get; set; }
    }
}
