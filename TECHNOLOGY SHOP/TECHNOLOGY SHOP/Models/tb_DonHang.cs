namespace TECHNOLOGY_SHOP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_DonHang()
        {
            tb_DonHang_SanPham = new HashSet<tb_DonHang_SanPham>();
        }

        [Key]
        public int idDonHang { get; set; }

        public int idTaiKhoan { get; set; }

        public DateTime ngayDat { get; set; }

        public DateTime ngayGiao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_DonHang_SanPham> tb_DonHang_SanPham { get; set; }

        public virtual tb_TaiKhoan tb_TaiKhoan { get; set; }
    }
}
