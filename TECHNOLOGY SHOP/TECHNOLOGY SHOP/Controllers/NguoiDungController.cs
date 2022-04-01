using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TECHNOLOGY_SHOP.Models;

namespace TECHNOLOGY_SHOP.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        MyDataDataContext data = new MyDataDataContext();
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, tb_TaiKhoan tk)
        {
            var hoten = collection["hoTen"];
            var tendangnhap = collection["tenDangNhap"];
            var matkhau = collection["matKhau"];
            var MatKhauXacNhan = collection["matKhauXacNhan"];
            var email = collection["eMail"];
            var diachi = collection["diaChi"];
            var dienthoai = collection["soDienThoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["ngaySinh"]);
            if (String.IsNullOrEmpty(MatKhauXacNhan))
            {
                ViewData["NhapMKXN "] = "Phải nhập mật khẩu xác nhận!";
            }
            else
            {
                if (!matkhau.Equals(MatKhauXacNhan))
                {
                    ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khẩu xác nhận phải giống nhau";
                }
                else
                {
                    tk.hoTen = hoten;
                    tk.tenDangNhap = tendangnhap;
                    tk.matKhau = matkhau;
                    tk.eMail = email;
                    tk.diaChi = diachi;
                    tk.soDienThoai = dienthoai;
                    tk.laAdmin = true;
                    tk.ngaySinh = DateTime.Parse(ngaysinh);
                    data.tb_TaiKhoans.InsertOnSubmit(tk);
                    data.SubmitChanges();
                    return RedirectToAction("DangNhap");
                }
            }
            return this.DangKy();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var tendangnhap = collection["tenDangNhap"];
            var matkhau = collection["matKhau"];
            tb_TaiKhoan tk = data.tb_TaiKhoans.SingleOrDefault(n => n.tenDangNhap == tendangnhap && n.matKhau == matkhau);
            if (tk != null)
            {
                ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                Session["Taikhoan"] = tk;
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return RedirectToAction("GioHang", "GioHang");
        }
    }
}