namespace TECHNOLOGY_SHOP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_SanPham()
        {
            tb_DonHang_SanPham = new HashSet<tb_DonHang_SanPham>();
        }

        [Key]
        public int idSP { get; set; }

        public int idLoaiSP { get; set; }

        [Required]
        [StringLength(100)]
        public string tenSP { get; set; }

        public bool trangThai { get; set; }

        [Column(TypeName = "money")]
        public decimal giaBan { get; set; }

        [Required]
        [StringLength(1000)]
        public string moTa { get; set; }

        [Required]
        [StringLength(100)]
        public string hinh { get; set; }

        public int soLuongTon { get; set; }

        public DateTime ngayCapNhat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_DonHang_SanPham> tb_DonHang_SanPham { get; set; }

        public virtual tb_LoaiSanPham tb_LoaiSanPham { get; set; }
    }
}
