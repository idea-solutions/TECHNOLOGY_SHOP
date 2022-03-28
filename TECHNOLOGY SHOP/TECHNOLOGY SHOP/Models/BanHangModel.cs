using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TECHNOLOGY_SHOP.Models
{
    public partial class BanHangModel : DbContext
    {
        public BanHangModel()
            : base("name=BanHangModel")
        {
        }

        public virtual DbSet<tb_DonHang> tb_DonHang { get; set; }
        public virtual DbSet<tb_DonHang_SanPham> tb_DonHang_SanPham { get; set; }
        public virtual DbSet<tb_HangSanPham> tb_HangSanPham { get; set; }
        public virtual DbSet<tb_LoaiSanPham> tb_LoaiSanPham { get; set; }
        public virtual DbSet<tb_SanPham> tb_SanPham { get; set; }
        public virtual DbSet<tb_TaiKhoan> tb_TaiKhoan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tb_DonHang>()
                .HasMany(e => e.tb_DonHang_SanPham)
                .WithRequired(e => e.tb_DonHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_DonHang_SanPham>()
                .Property(e => e.donGia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tb_DonHang_SanPham>()
                .Property(e => e.thanhTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tb_HangSanPham>()
                .Property(e => e.logo)
                .IsUnicode(false);

            modelBuilder.Entity<tb_HangSanPham>()
                .HasMany(e => e.tb_LoaiSanPham)
                .WithRequired(e => e.tb_HangSanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_LoaiSanPham>()
                .HasMany(e => e.tb_SanPham)
                .WithRequired(e => e.tb_LoaiSanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_SanPham>()
                .Property(e => e.giaBan)
                .HasPrecision(19, 4);

            modelBuilder.Entity<tb_SanPham>()
                .HasMany(e => e.tb_DonHang_SanPham)
                .WithRequired(e => e.tb_SanPham)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tb_TaiKhoan>()
                .Property(e => e.tenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<tb_TaiKhoan>()
                .Property(e => e.matKhau)
                .IsUnicode(false);

            modelBuilder.Entity<tb_TaiKhoan>()
                .Property(e => e.soDienThoai)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<tb_TaiKhoan>()
                .Property(e => e.eMail)
                .IsUnicode(false);

            modelBuilder.Entity<tb_TaiKhoan>()
                .HasMany(e => e.tb_DonHang)
                .WithRequired(e => e.tb_TaiKhoan)
                .WillCascadeOnDelete(false);
        }
    }
}
