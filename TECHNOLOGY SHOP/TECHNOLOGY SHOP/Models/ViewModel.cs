using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TECHNOLOGY_SHOP.Models
{
    public class ViewModel
    {
        public tb_DonHang donhang { get; set; }
        public tb_DonHang_SanPham donhangsanpham { get; set; }
        public tb_SanPham sanpham { get; set; }
    }
}