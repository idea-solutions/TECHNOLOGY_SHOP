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



            List<tb_DonHang> donhang1 = data.tb_DonHangs.ToList();
            List<tb_DonHang_SanPham> donhangsanpham = data.tb_DonHang_SanPhams.ToList();
            List<tb_SanPham> sanpham = data.tb_SanPhams.ToList();
            var lstAll = from e in donhang1
                         join d in donhangsanpham on e.idDonHang equals d.idDonHang into table1
                         from d in table1.ToList()
                         join i in sanpham on d.idSP equals i.idSP into table2
                         from i in table2.ToList()
                         select new ViewModel
                         {
                             donhang = e,
                             donhangsanpham = d,
                             sanpham = i
                         };

            ViewBag.lstAll = lstAll;

            return View(allDonHang);


        }

        public ActionResult LichSuDatHangAdmin()
        {
            List<tb_DonHang> donhang = data.tb_DonHangs.ToList();
            List<tb_DonHang_SanPham> donhangsanpham = data.tb_DonHang_SanPhams.ToList();
            List<tb_SanPham> sanpham = data.tb_SanPhams.ToList();
            var lstAll = from e in donhang
                         join d in donhangsanpham on e.idDonHang equals d.idDonHang into table1
                         from d in table1.ToList()
                         join i in sanpham on d.idSP equals i.idSP into table2
                         from i in table2.ToList()
                         select new ViewModel
                         {
                             donhang = e,
                             donhangsanpham = d,
                             sanpham = i
                         };
            return View(lstAll);
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