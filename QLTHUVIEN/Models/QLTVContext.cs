using Microsoft.EntityFrameworkCore;

namespace QLTHUVIEN.Models;

public partial class QLTVContext : DbContext
{
    public QLTVContext()
    {
    }

    public QLTVContext( DbContextOptions<QLTVContext> options )
        : base(options)
    {
    }

    public virtual DbSet<Chitiethoadon> Chitiethoadons { get; set; }

    public virtual DbSet<Hoadon> Hoadons { get; set; }

    public virtual DbSet<Kho> Khos { get; set; }

    public virtual DbSet<Linhvuc> Linhvucs { get; set; }

    public virtual DbSet<Loaisach> Loaisaches { get; set; }

    public virtual DbSet<Nhaxuatban> Nhaxuatbans { get; set; }

    public virtual DbSet<Sach> Saches { get; set; }

    public virtual DbSet<Tacgia> Tacgias { get; set; }

    public virtual DbSet<Taikhoan> Taikhoans { get; set; }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-UE1CJ6O\\\\\\\\MSSQLSERVER1,1433;Initial Catalog=QLNS;User Id=sa;Password=12345678;Encrypt=True;TrustServerCertificate=True");

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        modelBuilder.Entity<Chitiethoadon>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("CHITIETHOADON");

            entity.Property(e => e.Giatien).HasColumnName("GIATIEN");
            entity.Property(e => e.Mahoadon)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MAHOADON");
            entity.Property(e => e.Masach)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MASACH");
            entity.Property(e => e.Soluong).HasColumnName("SOLUONG");
            entity.Property(e => e.Thanhtien).HasColumnName("THANHTIEN");

            entity.HasOne(d => d.MahoadonNavigation).WithMany()
                .HasForeignKey(d => d.Mahoadon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CHITIETHOADON_HOADON");

            entity.HasOne(d => d.MasachNavigation).WithMany()
                .HasForeignKey(d => d.Masach)
                .HasConstraintName("FK_CHITIETHOADON_SACH");
        });

        modelBuilder.Entity<Hoadon>(entity =>
        {
            entity.HasKey(e => e.Mahoadon).HasName("PK__HOADON__A4999DF50D1796C0");

            entity.ToTable("HOADON");

            entity.Property(e => e.Mahoadon)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MAHOADON");
            entity.Property(e => e.Ngaylap)
                .HasColumnType("datetime")
                .HasColumnName("NGAYLAP");
            entity.Property(e => e.Tenkhachhang)
                .HasMaxLength(50)
                .HasColumnName("TENKHACHHANG");
            entity.Property(e => e.Tongtien)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("TONGTIEN");
        });

        modelBuilder.Entity<Kho>(entity =>
        {
            entity.HasKey(e => e.Masach).HasName("PK__KHO__3FC48E4C1754EB71");

            entity.ToTable("KHO");

            entity.Property(e => e.Masach)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MASACH");
            entity.Property(e => e.Soluong).HasColumnName("SOLUONG");

            entity.HasOne(d => d.MasachNavigation).WithOne(p => p.Kho)
                .HasForeignKey<Kho>(d => d.Masach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KHO_SACH");
        });

        modelBuilder.Entity<Linhvuc>(entity =>
        {
            entity.HasKey(e => e.Tenlinhvuc).HasName("PK__LINHVUC__A928AA112F69B2AD");

            entity.ToTable("LINHVUC");

            entity.Property(e => e.Tenlinhvuc)
                .HasMaxLength(30)
                .HasColumnName("TENLINHVUC");
        });

        modelBuilder.Entity<Loaisach>(entity =>
        {
            entity.HasKey(e => e.Maloaisach);

            entity.ToTable("LOAISACH");

            entity.Property(e => e.Maloaisach).HasColumnName("MALOAISACH");
            entity.Property(e => e.Maloai)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Mota).HasMaxLength(255);
            entity.Property(e => e.Tenloaisach)
                .HasMaxLength(30)
                .HasColumnName("TENLOAISACH");
        });

        modelBuilder.Entity<Nhaxuatban>(entity =>
        {
            entity.HasKey(e => e.Tennhaxuatban).HasName("PK__NHAXUATB__FD6BF96009517C9B");

            entity.ToTable("NHAXUATBAN");

            entity.Property(e => e.Tennhaxuatban)
                .HasMaxLength(50)
                .HasColumnName("TENNHAXUATBAN");
        });

        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.Masach).HasName("PK__SACH__3FC48E4CE437BC27");

            entity.ToTable("SACH");

            entity.Property(e => e.Masach)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MASACH");
            entity.Property(e => e.Giaban).HasColumnName("GIABAN");
            entity.Property(e => e.Giamgia).HasColumnName("GIAMGIA");
            entity.Property(e => e.Giamua).HasColumnName("GIAMUA");
            entity.Property(e => e.Lantaiban).HasColumnName("LANTAIBAN");
            entity.Property(e => e.Maloaisach).HasColumnName("MALOAISACH");
            entity.Property(e => e.Matg)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MATG");
            entity.Property(e => e.Namxuatban)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("NAMXUATBAN");
            entity.Property(e => e.Tenlinhvuc)
                .HasMaxLength(30)
                .HasColumnName("TENLINHVUC");
            entity.Property(e => e.Tennhaxuatban)
                .HasMaxLength(50)
                .HasColumnName("TENNHAXUATBAN");
            entity.Property(e => e.Tensach)
                .HasMaxLength(100)
                .HasColumnName("TENSACH");

            entity.HasOne(d => d.MaloaisachNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.Maloaisach)
                .HasConstraintName("FK_SACH_LOAI");

            entity.HasOne(d => d.MatgNavigation).WithMany(p => p.Saches)
                .HasForeignKey(d => d.Matg)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SACH_TACGIA");
        });

        modelBuilder.Entity<Tacgia>(entity =>
        {
            entity.HasKey(e => e.Matg).HasName("PK__TACGIA__6023721AB26084C3");

            entity.ToTable("TACGIA");

            entity.Property(e => e.Matg)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MATG");
            entity.Property(e => e.Nammat).HasColumnName("NAMMAT");
            entity.Property(e => e.Namsinh).HasColumnName("NAMSINH");
            entity.Property(e => e.Quequan)
                .HasMaxLength(20)
                .HasColumnName("QUEQUAN");
            entity.Property(e => e.Tentg)
                .HasMaxLength(40)
                .HasColumnName("TENTG");
        });

        modelBuilder.Entity<Taikhoan>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__TAIKHOAN__B15BE12F6731B6E0");

            entity.ToTable("TAIKHOAN");

            entity.Property(e => e.Username)
                .HasMaxLength(20)
                .HasColumnName("USERNAME");
            entity.Property(e => e.PassWord)
                .HasMaxLength(100)
                .HasColumnName("PASS_WORD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial( ModelBuilder modelBuilder );
}
