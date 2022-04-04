using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TECHNOLOGY_SHOP.Models;

namespace TECHNOLOGY_SHOP.Controllers
{
    public class GioHangController : Controller
    {
        // GET: Giohang
        MyDataDataContext data = new MyDataDataContext();

        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }

        public ActionResult LichSuDatHang(int id)
        {
            List<tb_DonHang> donhang = data.tb_DonHangs.Where(cc => cc.idTaiKhoan == id).ToList();
            List<tb_DonHang> donhangT = data.tb_DonHangs.Where(cc => cc.idTaiKhoan == id && cc.trangThai == "Chờ xử lý").ToList();
            List<tb_DonHang> donhangTT = data.tb_DonHangs.Where(cc => cc.idTaiKhoan == id && cc.daThanhToan == true).ToList();
            List<tb_DonHang> donhangTTT = data.tb_DonHangs.Where(cc => cc.idTaiKhoan == id && cc.trangThai == "Đã giao" && cc.daThanhToan == true).ToList();
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
            var lstAllT = from e in donhangT
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
            var lstAllTT = from e in donhangTT
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
            var hoantat = from e in donhangTTT
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
            ViewBag.lstAllT = lstAllT;
            ViewBag.lstAllTT = lstAllTT;
            ViewBag.lstAllhoantat = hoantat;
            return View(lstAll);
        }

        public ActionResult danhGia(int idSP, int idDH, int sao)
        {
            var donHang = data.tb_DonHang_SanPhams.First(m => m.idDonHang == idDH && m.idSP == idSP);
            var sanpham = data.tb_SanPhams.First(m => m.idSP == idSP);
            sanpham.danhGia = (sanpham.soNguoiDanhGia * sanpham.danhGia + sao) / (sanpham.soNguoiDanhGia + 1);
            sanpham.soNguoiDanhGia += 1;
            
            donHang.danhGia = sao;
            UpdateModel(donHang);
            UpdateModel(sanpham);
            data.SubmitChanges();
            tb_TaiKhoan tk = (tb_TaiKhoan)Session["Taikhoan"];
            return RedirectToAction("LichSuDatHang/"+tk.idTaiKhoan);
        }

        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.Find(p => p.idSP == id);
            if (sanpham == null)
            {
                sanpham = new Giohang(id);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong += 1;
                return Redirect(strURL);
            }
        }

        public ActionResult Muangay(int id, string strURL)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.Find(p => p.idSP == id);
            if (sanpham == null)
            {
                sanpham = new Giohang(id);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong += 1;
                return Redirect(@Url.Action("GioHang", "GioHang"));
            }
        }

        private int TongSoLuong()
        {
            int tsl = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Sum(p => p.iSoluong);
            }
            return tsl;
        }

        private int TongSoLuongSanPham()
        {
            int tsl = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tsl = lstGiohang.Count;
            }
            return tsl;
        }

        private double TongTien()
        {
            double tt = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                tt = lstGiohang.Sum(p => p.dThanhtien);
            }
            return tt;
        }

        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGiohang);
        }

        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return PartialView();
        }

        public ActionResult XoaGiohang(int id)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(p => p.idSP == id);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(p => p.idSP == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapnhatGiohang(int id, FormCollection collection)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(p => p.idSP == id);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(collection["txtSoLg"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaTatCaGioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("GioHang");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "TaiKhoan");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGiohang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            tb_DonHang dh = new tb_DonHang();
            tb_TaiKhoan kh = (tb_TaiKhoan)Session["TaiKhoan"];
            tb_SanPham s = new tb_SanPham();

            List<Giohang> gh = Laygiohang();
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["ngayGiao"]);

            dh.idTaiKhoan = kh.idTaiKhoan;
            dh.ngayDat = DateTime.Now;
            dh.ngayGiao = DateTime.Parse(ngaygiao);
            dh.trangThai = "Chờ xử lý";
            dh.daThanhToan = false;

            data.tb_DonHangs.InsertOnSubmit(dh);
            data.SubmitChanges();

            foreach (var item in gh)
            {
                tb_DonHang_SanPham ctdh = new tb_DonHang_SanPham();
                ctdh.idDonHang = dh.idDonHang;
                ctdh.idSP = item.idSP;
                ctdh.soLuong = item.iSoluong;
                ctdh.donGia = (decimal)item.giaBan;
                ctdh.thanhTien = (decimal)TongTien();
                s = data.tb_SanPhams.Single(n => n.idSP == item.idSP);
                s.soLuongTon -= ctdh.soLuong;
                data.SubmitChanges();

                data.tb_DonHang_SanPhams.InsertOnSubmit(ctdh);
            }
            data.SubmitChanges();
            Session["Giohang"] = null;
            return RedirectToAction("XacnhanDonhang", "GioHang");

        }

        public ActionResult XacnhanDonhang()
        {
            return View();
        }
    }
}