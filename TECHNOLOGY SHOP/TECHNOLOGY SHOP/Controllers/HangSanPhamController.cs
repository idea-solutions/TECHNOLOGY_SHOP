using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TECHNOLOGY_SHOP.Models;

namespace TECHNOLOGY_SHOP.Controllers
{
    public class HangSanPhamController : Controller
    {
        // GET: HangSanPham

        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index()
        {
            var all_hangsanpham = from hsp in data.tb_HangSanPhams select hsp;
            return View(all_hangsanpham);
        }

        public ActionResult Detail(int id)
        {
            var D_hang = data.tb_HangSanPhams.Where(p => p.idHang == id).First();
            return View(D_hang);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection, tb_HangSanPham s)
        {
            var s_tenhang = collection["tenHang"];
            var s_logo = collection["logo"];
            var s_linhvuc = collection["linhVuc"];
            var s_quocgia = collection["quocGia"];
            var s_namthanhlap = Convert.ToInt32(collection["namThanhLap"]);
            if (string.IsNullOrEmpty(s_tenhang))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.tenHang = s_tenhang.ToString();
                s.logo = s_logo.ToString();
                s.linhVuc = s_linhvuc.ToString();
                s.quocGia = s_quocgia.ToString();
                s.namThanhLap = s_namthanhlap;

                data.tb_HangSanPhams.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
    }
}