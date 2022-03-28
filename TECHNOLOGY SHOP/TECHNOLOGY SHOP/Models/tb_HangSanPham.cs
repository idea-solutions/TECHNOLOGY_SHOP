namespace TECHNOLOGY_SHOP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_HangSanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_HangSanPham()
        {
            tb_LoaiSanPham = new HashSet<tb_LoaiSanPham>();
        }

        [Key]
        public int idHang { get; set; }

        [Required]
        [StringLength(50)]
        public string tenHang { get; set; }

        [Required]
        [StringLength(100)]
        public string logo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_LoaiSanPham> tb_LoaiSanPham { get; set; }
    }
}
