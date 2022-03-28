namespace TECHNOLOGY_SHOP.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tb_TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tb_TaiKhoan()
        {
            tb_DonHang = new HashSet<tb_DonHang>();
        }

        [Key]
        public int idTaiKhoan { get; set; }

        [Required]
        [StringLength(20)]
        public string tenDangNhap { get; set; }

        [Required]
        [StringLength(20)]
        public string matKhau { get; set; }

        [Required]
        [StringLength(50)]
        public string hoTen { get; set; }

        [Required]
        [StringLength(10)]
        public string soDienThoai { get; set; }

        [Required]
        [StringLength(100)]
        public string diaChi { get; set; }

        [Required]
        [StringLength(50)]
        public string eMail { get; set; }

        public bool laAdmin { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tb_DonHang> tb_DonHang { get; set; }
    }
}
