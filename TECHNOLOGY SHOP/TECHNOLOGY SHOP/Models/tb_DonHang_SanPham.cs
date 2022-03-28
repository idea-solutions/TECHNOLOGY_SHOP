namespace TECHNOLOGY_SHOP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_DonHang_SanPham
    {
        [Key]
        [Column(Order = 0)]
        public int idDonHang { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idSP { get; set; }

        public int soLuong { get; set; }

        [Column(TypeName = "money")]
        public decimal donGia { get; set; }

        [Column(TypeName = "money")]
        public decimal thanhTien { get; set; }

        public virtual tb_DonHang tb_DonHang { get; set; }

        public virtual tb_SanPham tb_SanPham { get; set; }
    }
}
