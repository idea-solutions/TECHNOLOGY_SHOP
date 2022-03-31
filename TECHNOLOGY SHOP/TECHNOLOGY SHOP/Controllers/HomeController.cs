using System;
using System.Collections.Generic;
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
            var allSanPham = from sanpham in data.tb_SanPhams select sanpham;
            return View(allSanPham);
        }
    }
}