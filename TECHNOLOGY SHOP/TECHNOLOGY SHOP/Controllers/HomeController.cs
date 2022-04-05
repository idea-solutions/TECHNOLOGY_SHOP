using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TECHNOLOGY_SHOP.Models;

namespace TECHNOLOGY_SHOP.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            var allSanPham1 = from sanpham in data.tb_SanPhams where sanpham.idLoaiSP == 10 select sanpham;
            ViewBag.lst1 = allSanPham1.Take(5);
            var allSanPham2 = from sanpham in data.tb_SanPhams where sanpham.idLoaiSP == 25 select sanpham;
            ViewBag.lst2 = allSanPham2.Take(5);
            Session["idLoai"] = allSanPham2.First().idLoaiSP;
            var allSanPham3 = from sanpham in data.tb_SanPhams where sanpham.idLoaiSP == 26 select sanpham;
            ViewBag.lst3 = allSanPham3.Take(5);
            var loaisanpham = from loai in data.tb_LoaiSanPhams select loai;
            ViewBag.loaisanphamAll = loaisanpham;
            var hangsanpham = from hang in data.tb_HangSanPhams select hang;
            ViewBag.hangsanphamAll = hangsanpham.Take(9);
            return View(allSanPham1);
        }

        public ActionResult SanPham(int id)
        {
            var sanpham = from sp in data.tb_SanPhams where sp.idLoaiSP==id select sp;
            ViewBag.loaisanphamAll = sanpham;
            return View(sanpham);
        }

        public ActionResult SanPhamByHang(int id)
        {
            var sanpham = from sp in data.tb_SanPhams where sp.idHang == id select sp;
            ViewBag.loaisanphamAll = sanpham;

            ViewBag.loaisanphamAll = sanpham;
            return View(sanpham);
        }

        public ActionResult SanPhamByTen(string idSPP)
        {
            var links = from l in data.tb_SanPhams
                        select l;

            if (!String.IsNullOrEmpty(idSPP))
            {
                links = links.Where(s => s.tenSP.Contains(idSPP));
            }
            var sanpham = from sp in data.tb_SanPhams where sp.tenSP.Contains(idSPP) select sp;
            ViewBag.sanpham = sanpham;
            return View(links);
        }

        public ActionResult SanPhamByGia(int giatu, int giaden)
        {
            int idLoai = (int)Session["idLoai"];
            var links = from l in data.tb_SanPhams
                        select l;
                links = links.Where(s => s.giaBan >= giatu && s.giaBan <= giaden && s.idLoaiSP == idLoai);

            return View(links);
        }
    }
}