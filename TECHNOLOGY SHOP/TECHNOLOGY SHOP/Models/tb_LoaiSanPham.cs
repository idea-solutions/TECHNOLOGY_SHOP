namespace TECHNOLOGY_SHOP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_LoaiSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_LoaiSanPham()
        {
            tb_SanPham = new HashSet<tb_SanPham>();
        }

        [Key]
        public int idLoaiSP { get; set; }

        public int idHang { get; set; }

        [Required]
        [StringLength(100)]
        public string tenLoaiSP { get; set; }

        public bool trangThai { get; set; }

        public virtual tb_HangSanPham tb_HangSanPham { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_SanPham> tb_SanPham { get; set; }
    }
}
