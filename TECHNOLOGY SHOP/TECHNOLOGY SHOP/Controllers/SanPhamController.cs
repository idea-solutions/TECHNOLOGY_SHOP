using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TECHNOLOGY_SHOP.Models;

namespace TECHNOLOGY_SHOP.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            var allSanPham = from sanpham in data.tb_SanPhams select sanpham;
            return View(allSanPham);
        }
        public ActionResult Detail(int id)
        {
            var detaiSanPham = data.tb_SanPhams.Where(m => m.idSP == id).First();
            return View(detaiSanPham);
        }
        public ActionResult Create()
        {
            ViewBag.lstLoaiSP = new SelectList(data.tb_LoaiSanPhams, "idLoaiSP", "tenLoaiSP");
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection, tb_SanPham sp)
        {
            bool c_trangThai;
            var c_idLoaiSP = collection["idLoaiSP"];
            var c_tenSP = collection["tenSP"];
            var c_giaban= collection["giaBan"];
            var c_mota = collection["moTa"];
            var c_hinh = collection["hinh"];
            var c_soluongton = collection["soLuongTon"];
            var c_ngaycapnhat = collection["ngayCapNhat"];

            if (collection["trangThai"] == null)
                c_trangThai = Convert.ToBoolean(collection["trangThai"]);
            else
                c_trangThai = true;

            if (string.IsNullOrEmpty(c_tenSP))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                sp.idLoaiSP = Convert.ToInt32(c_idLoaiSP);
                sp.tenSP = c_tenSP.ToString();
                sp.trangThai = c_trangThai;
                sp.giaBan = Convert.ToInt32(c_giaban);
                sp.moTa = c_mota.ToString();
                sp.hinh = c_hinh.ToString();
                sp.soLuongTon = Convert.ToInt32(c_soluongton);
                sp.ngayCapNhat = DateTime.Now;

                data.tb_SanPhams.InsertOnSubmit(sp);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        public ActionResult Edit(int id)
        {
            ViewBag.lstLoaiSP = new SelectList(data.tb_LoaiSanPhams, "idLoaiSP", "tenLoaiSP");
            var e_SP = data.tb_SanPhams.First(m => m.idSP == id);
            return View(e_SP);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var e_SP = data.tb_SanPhams.First(m => m.idSP == id);
            bool e_trangThai;
            var e_idLoaiSP = collection["idLoaiSP"];
            var e_tenSP = collection["tenSP"];
            var e_giaban = collection["giaBan"];
            var e_mota = collection["moTa"];
            var e_hinh = collection["hinh"];
            var e_soluongton = collection["soLuongTon"];
            var e_ngaycapnhat = collection["ngayCapNhat"];
            if (collection["trangThai"] == null)
                e_trangThai = Convert.ToBoolean(collection["trangThai"]);
            else
                e_trangThai = true;

            e_SP.idSP = id;
            if (string.IsNullOrEmpty(e_tenSP))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                e_SP.idLoaiSP = Convert.ToInt32(e_idLoaiSP);
                e_SP.tenSP = e_tenSP.ToString();
                e_SP.trangThai = e_trangThai;
                e_SP.giaBan = Convert.ToDecimal(e_giaban);
                e_SP.moTa = e_mota.ToString();
                e_SP.hinh = e_hinh.ToString();
                e_SP.soLuongTon = Convert.ToInt32(e_soluongton);
                e_SP.ngayCapNhat = DateTime.Now;
                UpdateModel(e_SP);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var d_SP = data.tb_SanPhams.First(m => m.idSP == id);
            return View(d_SP);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var d_SP = data.tb_SanPhams.Where(m => m.idSP == id).First();
            data.tb_SanPhams.DeleteOnSubmit(d_SP);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}