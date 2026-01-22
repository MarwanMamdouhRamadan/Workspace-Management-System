using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Workspace_Management_System.Entities;
using Workspace_Managment_System.identity;

namespace Workspace_Management_System.Data;

public partial class WorkSpaceSysContext : IdentityDbContext<ApplicationUser>
{
    public WorkSpaceSysContext()
    {
    }

    public WorkSpaceSysContext(DbContextOptions<WorkSpaceSysContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbBooking> TbBookings { get; set; }

    public virtual DbSet<TbBookingProduct> TbBookingProducts { get; set; }

    public virtual DbSet<TbInvoice> TbInvoices { get; set; }

    public virtual DbSet<TbInvoiceBooking> TbInvoiceBookings { get; set; }

    public virtual DbSet<TbMedium> TbMedia { get; set; }

    public virtual DbSet<TbPricingType> TbPricingTypes { get; set; }

    public virtual DbSet<TbProduct> TbProducts { get; set; }

    public virtual DbSet<TbRoom> TbRooms { get; set; }

    public virtual DbSet<TbSetting> TbSettings { get; set; }

    public virtual DbSet<TbStatus> TbStatuses { get; set; }

    public virtual DbSet<TbStatusType> TbStatusTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-PD9H1MG;Database=WorkSpaceSystem;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TbBooking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TbBookin__3214EC27BEC47BF2");

            entity.ToTable("TbBooking");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.RoomPrice).HasColumnType("money");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.Room).WithMany(p => p.TbBookings)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_Room");

            entity.HasOne(d => d.Status).WithMany(p => p.TbBookings)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booking_Status");
            entity.HasOne(d => d.User)           // الفاتورة لها مستخدم واحد
           .WithMany()                    // المستخدم يمكن أن يكون له فواتير متعددة (حتى لو لم نضف List في كلاس User)
           .HasForeignKey(d => d.UserId)  // المفتاح الخارجي
           .OnDelete(DeleteBehavior.Restrict); // يفضل Restrict مع الفواتير لعدم مسح الفاتورة لو حذف المستخدم
        });

        modelBuilder.Entity<TbBookingProduct>(entity =>
        {
            entity.HasKey(e => new { e.BookingId, e.ProductId }).HasName("PK_BookingProduct");

            entity.ToTable("TbBookingProduct");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductTotalPrice).HasColumnType("money");
            entity.Property(e => e.ProductUnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Booking).WithMany(p => p.TbBookingProducts)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingProduct_Booking");

            entity.HasOne(d => d.Product).WithMany(p => p.TbBookingProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BookingProduct_Product");
        });

        modelBuilder.Entity<TbInvoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TbInvoic__3214EC27D1866D83");

            entity.ToTable("TbInvoice");

            entity.HasIndex(e => e.InvoiceNumber, "UQ__TbInvoic__D776E9816693A655").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DiscountAmount).HasColumnType("money");
            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.GrandTotal).HasColumnType("money");
            entity.Property(e => e.InvoiceDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.InvoiceNumber).HasMaxLength(50);
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.SubTotal).HasColumnType("money");
            entity.Property(e => e.TaxAmount).HasColumnType("money");
            entity.Property(e => e.TaxPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TotalAfterDiscount).HasColumnType("money");
            entity.Property(e => e.TotalProductPrice).HasColumnType("money");
            entity.Property(e => e.TotalRoomPrice).HasColumnType("money");

            entity.HasOne(d => d.Status).WithMany(p => p.TbInvoices)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Status");
            entity.HasOne(d => d.User)           // الفاتورة لها مستخدم واحد
          .WithMany()                    // المستخدم يمكن أن يكون له فواتير متعددة (حتى لو لم نضف List في كلاس User)
          .HasForeignKey(d => d.UserId)  // المفتاح الخارجي
          .OnDelete(DeleteBehavior.Restrict); // يفضل Restrict مع الفواتير لعدم مسح الفاتورة لو حذف المستخدم

        });

        modelBuilder.Entity<TbInvoiceBooking>(entity =>
        {
            entity.HasKey(e => new { e.InvoiceId, e.BookingId }).HasName("PK_InvoiceBooking");

            entity.ToTable("TbInvoiceBooking");

            entity.HasIndex(e => e.BookingId, "UQ_InvoiceBooking_BookingID").IsUnique();

            entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.BookingProductPrice).HasColumnType("money");
            entity.Property(e => e.BookingRoomPrice).HasColumnType("money");
            entity.Property(e => e.BookingTotal).HasColumnType("money");

            entity.HasOne(d => d.Booking).WithOne(p => p.TbInvoiceBooking)
                .HasForeignKey<TbInvoiceBooking>(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceBooking_Booking");

            entity.HasOne(d => d.Invoice).WithMany(p => p.TbInvoiceBookings)
                .HasForeignKey(d => d.InvoiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceBooking_Invoice");
        });

        modelBuilder.Entity<TbMedium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TbMedia__3214EC2720E6FCBA");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RelativePathFile).HasMaxLength(500);
            entity.Property(e => e.RoomId).HasColumnName("RoomID");

            entity.HasOne(d => d.Room).WithMany(p => p.TbMedia)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Media_Room");
        });

        modelBuilder.Entity<TbPricingType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TbPricin__3214EC27D0AA45A7");

            entity.ToTable("TbPricingType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<TbProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TbProduc__3214EC27683B6026");

            entity.ToTable("TbProduct");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ProductName).HasMaxLength(200);
        });

        modelBuilder.Entity<TbRoom>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TbRoom__3214EC27F54565C8");

            entity.ToTable("TbRoom");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.PricingTypeId).HasColumnName("PricingTypeID");
            entity.Property(e => e.RoomName).HasMaxLength(200);

            entity.HasOne(d => d.PricingType).WithMany(p => p.TbRooms)
                .HasForeignKey(d => d.PricingTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Room_PricingType");
        });

        modelBuilder.Entity<TbSetting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TbSettin__3214EC2784368E9B");

            entity.ToTable("TbSetting");

            entity.HasIndex(e => e.KeyName, "UQ__TbSettin__F0A2A3372F699BDC").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.KeyName).HasMaxLength(100);
            entity.Property(e => e.KeyValue).HasMaxLength(500);
        });

        modelBuilder.Entity<TbStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TbStatus__3214EC272F2BE8D6");

            entity.ToTable("TbStatus");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.StatusName).HasMaxLength(100);
            entity.Property(e => e.StatusTypeId).HasColumnName("StatusTypeID");

            entity.HasOne(d => d.StatusType).WithMany(p => p.TbStatuses)
                .HasForeignKey(d => d.StatusTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Status_StatusType");
        });

        modelBuilder.Entity<TbStatusType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TbStatus__3214EC27AFD78D69");

            entity.ToTable("TbStatusType");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TypeName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
