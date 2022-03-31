using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TECHNOLOGY_SHOP.Models
{
    public class Giohang
    {
        MyDataDataContext data = new MyDataDataContext();
        [Display(Name = "ID sản phẩm")]
        public int idSP { get; set; }

        [Display(Name = "Tên sản phẩm")]
        public string tenSP { get; set; }

        [Display(Name = "Hình ảnh")]
        public string hinh { get; set; }

        [Display(Name = "Giá bán")]
        public Double giaBan { get; set; }

        [Display(Name = "Số lượng")]
        public int iSoluong { get; set; }

        [Display(Name = "Thành tiền")]
        public Double dThanhtien
        {
            get { return iSoluong * giaBan; }
        }
        public Giohang(int id)
        {
            idSP = id;
            tb_SanPham sanpham = data.tb_SanPhams.Single(p => p.idSP == idSP);
            tenSP = sanpham.tenSP;
            hinh = sanpham.hinh;
            giaBan = double.Parse(sanpham.giaBan.ToString());
            iSoluong = 1;
        }
    }
}