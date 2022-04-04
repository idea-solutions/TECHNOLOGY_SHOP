using TECHNOLOGY_SHOP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TECHNOLOGY_SHOP.Controllers
{
    public class DonHangController : Controller
    {
        // GET: DonHang
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            if (Session["Taikhoan"] != null)
            {
                tb_TaiKhoan tk = (tb_TaiKhoan)Session["Taikhoan"];
                ViewBag.ten = tk.hoTen;
            }
            ViewBag.listTen = new SelectList(data.tb_TaiKhoans, "idTaiKhoan", "hoTen");
            var allDonHang = from donhang in data.tb_DonHangs select donhang;
            return View(allDonHang);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.lstTrangThai = new SelectList(data.tb_DonHangs, "idDonHang", "trangThai");
            var e_donhang = data.tb_DonHangs.First(m => m.idDonHang == id);
            return View(e_donhang);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var e_donhang = data.tb_DonHangs.First(m => m.idDonHang == id);
            var e_trangThai = collection["trangThai"];
            e_donhang.idDonHang = id;
            if (string.IsNullOrEmpty(e_trangThai))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                e_donhang.trangThai = e_trangThai.ToString();
                UpdateModel(e_donhang);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
    }
}