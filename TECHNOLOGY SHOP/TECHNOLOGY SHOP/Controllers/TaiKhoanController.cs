using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TECHNOLOGY_SHOP.Models;
namespace TECHNOLOGY_SHOP.Controllers
{
    public class TaiKhoanController : Controller
    {

        MyDataDataContext data = new MyDataDataContext();
        // GET: TaiKhoan
        public ActionResult Index()
        {
            if (Session["tenDangNhap"] == null)
            {
                ViewBag.tenDangNhap = null;
            }
            else
            {
                ViewBag.tenDangNhap = Session["tenDangNhap"];
            }

            var allTaiKhoan = from taikhoan in data.tb_TaiKhoans select taikhoan;
            return View(allTaiKhoan);
        }

        public ActionResult Detail(int id)
        {
            if (Session["tenDangNhap"] == null)
            {
                ViewBag.tenDangNhap = null;
            }
            else
            {
                ViewBag.tenDangNhap = Session["tenDangNhap"];
            }

            var D_taikhoan = data.tb_TaiKhoans.Where(p => p.idTaiKhoan == id).First();
            return View(D_taikhoan);
        }

        public ActionResult Create()
        {
            if (Session["tenDangNhap"] == null)
            {
                ViewBag.tenDangNhap = null;
            }
            else
            {
                ViewBag.tenDangNhap = Session["tenDangNhap"];
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection, tb_TaiKhoan tk)
        {
            var tk_tendangnhap = collection["tenDangNhap"];
            var tk_matkhau = collection["matKhau"];
            var tk_matkhauXacNhan = collection["matKhauXacNhan"];
            var tk_hoten = collection["hoTen"];
            var tk_sodienthoai = collection["soDienThoai"];
            var tk_diachi = collection["diaChi"];
            var tk_email = collection["eMail"];
            var tk_ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["ngaySinh"]);



            if (string.IsNullOrEmpty(tk_matkhauXacNhan))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                if(!tk_matkhau.Equals(tk_matkhauXacNhan))
                {
                    ViewData["Error"] = "Don't empty!";
                }
                else
                {
                    tk.tenDangNhap = tk_tendangnhap.ToString();
                    tk.matKhau = tk_matkhau.ToString();
                    tk.hoTen = tk_hoten.ToString();
                    tk.soDienThoai = tk_sodienthoai.ToString();
                    tk.diaChi = tk_diachi.ToString();
                    tk.eMail = tk_email.ToString();
                    tk.ngaySinh = DateTime.Parse(tk_ngaysinh);
                    tk.laAdmin = false;

                    if (String.IsNullOrEmpty(tk_matkhauXacNhan))

                        data.tb_TaiKhoans.InsertOnSubmit(tk);
                    data.SubmitChanges();
                    return RedirectToAction("Index");
                }
                
            }
            return this.Create();
        }

        public ActionResult Delete(int id)
        {
            if (Session["tenDangNhap"] == null)
            {
                ViewBag.tenDangNhap = null;
            }
            else
            {
                ViewBag.tenDangNhap = Session["tenDangNhap"];
            }

            var D_TaiKhoan = data.tb_TaiKhoans.First(p => p.idTaiKhoan == id);
            return View(D_TaiKhoan);
        }
        [HttpPost]

        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_TaiKhoan = data.tb_TaiKhoans.Where(m => m.idTaiKhoan == id).First();
            data.tb_TaiKhoans.DeleteOnSubmit(D_TaiKhoan);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string tenDangNhap, string matKhau)
        {
            bool result = data.tb_TaiKhoans.Where(a => a.tenDangNhap == tenDangNhap && a.matKhau == matKhau).Count() > 0;
            tb_TaiKhoan account = data.tb_TaiKhoans.FirstOrDefault(a => a.tenDangNhap == tenDangNhap && a.matKhau == matKhau);
            if (account != null)
            {
                var admin = data.tb_TaiKhoans.Where(a => a.laAdmin == true && a.matKhau == matKhau && a.tenDangNhap == tenDangNhap).Count() > 0;
                if (admin)
                {
                    Session["tenDangNhap"] = tenDangNhap;
                    return RedirectToAction("Index", "TaiKhoan");
                }
                else
                {
                    Session["Taikhoan"] = account;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng!";
                return View();
            }
        }

        public ActionResult LoginPartial()
        {
            if (Session["Taikhoan"] != null)
            {
                tb_TaiKhoan tk = (tb_TaiKhoan)Session["Taikhoan"];
                ViewBag.username = tk.hoTen;

            }
            return PartialView();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "TaiKhoan");
        }
        [HttpPost]
        public ActionResult DangKy(string tenDangNhap, string matKhau, string hoTen, string soDienThoai, string diaChi, string eMail, tb_TaiKhoan tk)
        {
            var tk_tendangnhap = tenDangNhap;
            var tk_matkhau = matKhau;
            var tk_hoten = hoTen;
            var tk_sodienthoai = soDienThoai;
            var tk_diachi = diaChi;
            var tk_email = eMail;



            bool result = data.tb_TaiKhoans.Where(a => a.tenDangNhap == tenDangNhap && a.matKhau == matKhau).Count() > 0;
            if (result == true)
            {
                ViewBag.ErrorMessage2 = "Tài khoản đã tồn tại";
                return RedirectToAction("Login", "TaiKhoan");
            }
            else
            {
                tk.tenDangNhap = tk_tendangnhap.ToString();
                tk.matKhau = tk_matkhau.ToString();
                tk.hoTen = tk_hoten.ToString();
                tk.soDienThoai = tk_sodienthoai.ToString();
                tk.diaChi = tk_diachi.ToString();
                tk.eMail = tk_email.ToString();
                tk.laAdmin = false;
                data.tb_TaiKhoans.InsertOnSubmit(tk);
                data.SubmitChanges();
                return RedirectToAction("Login", "TaiKhoan");
            }
        }
    }
}