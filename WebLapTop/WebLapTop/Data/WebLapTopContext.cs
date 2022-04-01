using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebLapTop.Models;

#nullable disable

namespace WebLapTop.Data
{
    public partial class WebLapTopContext : DbContext
    {
        public WebLapTopContext()
        {
        }

        public WebLapTopContext(DbContextOptions<WebLapTopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Baiviet> Baiviets { get; set; }
        public virtual DbSet<Binhluan> Binhluans { get; set; }
        public virtual DbSet<Chitiethoadon> Chitiethoadons { get; set; }
        public virtual DbSet<Danhmucsanpham> Danhmucsanphams { get; set; }
        public virtual DbSet<Dongsanpham> Dongsanphams { get; set; }
        public virtual DbSet<Hoadon> Hoadons { get; set; }
        public virtual DbSet<ImagesSanPham> ImagesSanPhams { get; set; }
        public virtual DbSet<Khachhang> Khachhangs { get; set; }
        public virtual DbSet<Loaiphukien> Loaiphukiens { get; set; }
        public virtual DbSet<Nhanvien> Nhanviens { get; set; }
        public virtual DbSet<Noisanxuat> Noisanxuats { get; set; }
        public virtual DbSet<Sanpham> Sanphams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-R5QB9QIO;Database=WebLapTop;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Vietnamese_CI_AS");

            modelBuilder.Entity<Binhluan>(entity =>
            {
                entity.HasOne(d => d.IdsanPhamNavigation)
                    .WithMany(p => p.Binhluans)
                    .HasForeignKey(d => d.IdsanPham)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BINHLUAN__IDSanP__21B6055D");
            });

            modelBuilder.Entity<Chitiethoadon>(entity =>
            {
                entity.HasOne(d => d.IdhoaDonNavigation)
                    .WithMany(p => p.Chitiethoadons)
                    .HasForeignKey(d => d.IdhoaDon)
                    .HasConstraintName("FK__CHITIETHO__IDHoa__22AA2996");

                entity.HasOne(d => d.IdsanPhamNavigation)
                    .WithMany(p => p.Chitiethoadons)
                    .HasForeignKey(d => d.IdsanPham)
                    .HasConstraintName("FK__CHITIETHO__IDSan__239E4DCF");
            });

            modelBuilder.Entity<Dongsanpham>(entity =>
            {
                entity.HasOne(d => d.IddanhMucNavigation)
                    .WithMany(p => p.Dongsanphams)
                    .HasForeignKey(d => d.IddanhMuc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DONGSANPH__IDDan__24927208");
            });

            modelBuilder.Entity<Hoadon>(entity =>
            {
                entity.HasOne(d => d.IdkhachHangNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.IdkhachHang)
                    .HasConstraintName("FK__HOADON__IDKhachH__25869641");

                entity.HasOne(d => d.IdnhanVienNavigation)
                    .WithMany(p => p.Hoadons)
                    .HasForeignKey(d => d.IdnhanVien)
                    .HasConstraintName("FK__HOADON__IDNhanVi__267ABA7A");
            });

            modelBuilder.Entity<ImagesSanPham>(entity =>
            {
                entity.HasOne(d => d.IdSanphamNavigation)
                    .WithMany(p => p.ImagesSanPhams)
                    .HasForeignKey(d => d.IdSanpham)
                    .HasConstraintName("FK__ImagesSan__IdSan__49C3F6B7");
            });

            modelBuilder.Entity<Loaiphukien>(entity =>
            {
                entity.HasOne(d => d.IddanhMucNavigation)
                    .WithMany(p => p.Loaiphukiens)
                    .HasForeignKey(d => d.IddanhMuc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LOAIPHUKI__IDDan__276EDEB3");
            });

            modelBuilder.Entity<Sanpham>(entity =>
            {
                entity.HasOne(d => d.IddongSanPhamNavigation)
                    .WithMany(p => p.Sanphams)
                    .HasForeignKey(d => d.IddongSanPham)
                    .HasConstraintName("FK__SANPHAM__IDDongS__286302EC");

                entity.HasOne(d => d.IdloaiPhuKienNavigation)
                    .WithMany(p => p.Sanphams)
                    .HasForeignKey(d => d.IdloaiPhuKien)
                    .HasConstraintName("FK__SANPHAM__IDLoaiP__29572725");

                entity.HasOne(d => d.IdnoiSanXuatNavigation)
                    .WithMany(p => p.Sanphams)
                    .HasForeignKey(d => d.IdnoiSanXuat)
                    .HasConstraintName("FK__SANPHAM__IDNoiSa__2A4B4B5E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
