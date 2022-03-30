using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TECHNOLOGY_SHOP.Models
{
    public class HangSanPhamList
    {
        MyDataDataContext data = null;

        public HangSanPhamList()
        {
            data = new MyDataDataContext();
        }

        public List<tb_HangSanPham> ListAll()
        {
            return data.tb_HangSanPhams.ToList();
        }

    }
}