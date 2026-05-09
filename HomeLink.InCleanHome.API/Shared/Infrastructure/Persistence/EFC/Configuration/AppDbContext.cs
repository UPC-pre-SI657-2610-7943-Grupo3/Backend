using HomeLink.InCleanHome.API.Booking.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.IAM.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Payments.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Profiles.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.ReviewsAndEvaluation.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.SearchAndCatalog.Domain.Model.Aggregates;
using HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HomeLink.InCleanHome.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context for the InCleanHome Platform.
/// </summary>
/// <remarks>
///     This context aggregates the persistence configuration of every Bounded Context
///     of the InCleanHome platform: IAM, Profiles, SearchAndCatalog, Booking,
///     Payments and ReviewsAndEvaluation.
/// </remarks>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    ///     On configuring the database context.
    ///     Adds the created/updated date interceptor for audit trails.
    /// </summary>
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    /// <summary>
    ///     On creating the database model.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // ====================================================================
        // IAM Bounded Context
        // ====================================================================
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Email).IsRequired().HasMaxLength(120);
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        builder.Entity<User>().Property(u => u.Role).IsRequired().HasMaxLength(20);
        builder.Entity<User>().Property(u => u.IsVerified).HasDefaultValue(false);
        builder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        // ====================================================================
        // Profiles Bounded Context
        // ====================================================================
        builder.Entity<ClientProfile>().HasKey(c => c.Id);
        builder.Entity<ClientProfile>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ClientProfile>().Property(c => c.UserId).IsRequired();
        builder.Entity<ClientProfile>().OwnsOne(c => c.Name, n =>
        {
            n.WithOwner().HasForeignKey("Id");
            n.Property(p => p.FirstName).HasColumnName("FirstName").HasMaxLength(60);
            n.Property(p => p.LastName).HasColumnName("LastName").HasMaxLength(60);
        });
        builder.Entity<ClientProfile>().OwnsOne(c => c.Phone, p =>
        {
            p.WithOwner().HasForeignKey("Id");
            p.Property(x => x.Number).HasColumnName("PhoneNumber").HasMaxLength(20);
        });
        builder.Entity<ClientProfile>().OwnsOne(c => c.Address, a =>
        {
            a.WithOwner().HasForeignKey("Id");
            a.Property(s => s.Street).HasColumnName("AddressStreet").HasMaxLength(120);
            a.Property(s => s.District).HasColumnName("AddressDistrict").HasMaxLength(80);
            a.Property(s => s.City).HasColumnName("AddressCity").HasMaxLength(80);
            a.Property(s => s.Latitude).HasColumnName("AddressLatitude");
            a.Property(s => s.Longitude).HasColumnName("AddressLongitude");
        });

        builder.Entity<WorkerProfile>().HasKey(w => w.Id);
        builder.Entity<WorkerProfile>().Property(w => w.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<WorkerProfile>().Property(w => w.UserId).IsRequired();
        builder.Entity<WorkerProfile>().Property(w => w.Biography).HasMaxLength(500);
        builder.Entity<WorkerProfile>().Property(w => w.Experience).HasMaxLength(60);
        builder.Entity<WorkerProfile>().Property(w => w.HourlyRate).HasPrecision(10, 2);
        builder.Entity<WorkerProfile>().Property(w => w.AverageRating).HasPrecision(3, 2);
        builder.Entity<WorkerProfile>().Property(w => w.VerificationStatus).IsRequired().HasMaxLength(20);
        builder.Entity<WorkerProfile>().OwnsOne(w => w.Name, n =>
        {
            n.WithOwner().HasForeignKey("Id");
            n.Property(p => p.FirstName).HasColumnName("FirstName").HasMaxLength(60);
            n.Property(p => p.LastName).HasColumnName("LastName").HasMaxLength(60);
        });
        builder.Entity<WorkerProfile>().OwnsOne(w => w.Phone, p =>
        {
            p.WithOwner().HasForeignKey("Id");
            p.Property(x => x.Number).HasColumnName("PhoneNumber").HasMaxLength(20);
        });
        builder.Entity<WorkerProfile>().OwnsOne(w => w.Address, a =>
        {
            a.WithOwner().HasForeignKey("Id");
            a.Property(s => s.Street).HasColumnName("AddressStreet").HasMaxLength(120);
            a.Property(s => s.District).HasColumnName("AddressDistrict").HasMaxLength(80);
            a.Property(s => s.City).HasColumnName("AddressCity").HasMaxLength(80);
            a.Property(s => s.Latitude).HasColumnName("AddressLatitude");
            a.Property(s => s.Longitude).HasColumnName("AddressLongitude");
        });

        // ====================================================================
        // SearchAndCatalog Bounded Context
        // ====================================================================
        builder.Entity<ServiceCategory>().HasKey(c => c.Id);
        builder.Entity<ServiceCategory>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ServiceCategory>().Property(c => c.Name).IsRequired().HasMaxLength(60);
        builder.Entity<ServiceCategory>().Property(c => c.Description).HasMaxLength(240);

        builder.Entity<WorkerService>().HasKey(s => s.Id);
        builder.Entity<WorkerService>().Property(s => s.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<WorkerService>().Property(s => s.WorkerProfileId).IsRequired();
        builder.Entity<WorkerService>().Property(s => s.ServiceCategoryId).IsRequired();
        builder.Entity<WorkerService>().Property(s => s.Title).IsRequired().HasMaxLength(80);
        builder.Entity<WorkerService>().Property(s => s.Description).HasMaxLength(500);
        builder.Entity<WorkerService>().Property(s => s.Price).HasPrecision(10, 2);
        builder.Entity<WorkerService>().Property(s => s.IsActive).HasDefaultValue(true);

        builder.Entity<AvailabilitySlot>().HasKey(a => a.Id);
        builder.Entity<AvailabilitySlot>().Property(a => a.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<AvailabilitySlot>().Property(a => a.WorkerProfileId).IsRequired();
        builder.Entity<AvailabilitySlot>().Property(a => a.DayOfWeek).IsRequired();
        builder.Entity<AvailabilitySlot>().Property(a => a.StartTime).IsRequired();
        builder.Entity<AvailabilitySlot>().Property(a => a.EndTime).IsRequired();

        // ====================================================================
        // Booking Bounded Context
        // ====================================================================
        builder.Entity<BookingRequest>().HasKey(b => b.Id);
        builder.Entity<BookingRequest>().Property(b => b.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<BookingRequest>().Property(b => b.ClientId).IsRequired();
        builder.Entity<BookingRequest>().Property(b => b.WorkerId).IsRequired();
        builder.Entity<BookingRequest>().Property(b => b.WorkerServiceId).IsRequired();
        builder.Entity<BookingRequest>().Property(b => b.ScheduledAt).IsRequired();
        builder.Entity<BookingRequest>().Property(b => b.Status).IsRequired().HasMaxLength(30);
        builder.Entity<BookingRequest>().Property(b => b.Notes).HasMaxLength(500);
        builder.Entity<BookingRequest>().Property(b => b.AgreedPrice).HasPrecision(10, 2);

        // ====================================================================
        // Payments Bounded Context
        // ====================================================================
        builder.Entity<PaymentMethod>().HasKey(p => p.Id);
        builder.Entity<PaymentMethod>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<PaymentMethod>().Property(p => p.UserId).IsRequired();
        builder.Entity<PaymentMethod>().Property(p => p.Type).IsRequired().HasMaxLength(30);
        builder.Entity<PaymentMethod>().Property(p => p.Reference).HasMaxLength(60);

        builder.Entity<MonthlyCommission>().HasKey(m => m.Id);
        builder.Entity<MonthlyCommission>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<MonthlyCommission>().Property(m => m.WorkerId).IsRequired();
        builder.Entity<MonthlyCommission>().Property(m => m.Year).IsRequired();
        builder.Entity<MonthlyCommission>().Property(m => m.Month).IsRequired();
        builder.Entity<MonthlyCommission>().Property(m => m.TotalServices).IsRequired();
        builder.Entity<MonthlyCommission>().Property(m => m.TotalEarnings).HasPrecision(12, 2);
        builder.Entity<MonthlyCommission>().Property(m => m.CommissionAmount).HasPrecision(12, 2);
        builder.Entity<MonthlyCommission>().Property(m => m.Status).IsRequired().HasMaxLength(20);

        // ====================================================================
        // ReviewsAndEvaluation Bounded Context
        // ====================================================================
        builder.Entity<Review>().HasKey(r => r.Id);
        builder.Entity<Review>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Review>().Property(r => r.BookingRequestId).IsRequired();
        builder.Entity<Review>().Property(r => r.ClientId).IsRequired();
        builder.Entity<Review>().Property(r => r.WorkerId).IsRequired();
        builder.Entity<Review>().Property(r => r.Rating).IsRequired();
        builder.Entity<Review>().Property(r => r.Comment).HasMaxLength(500);

        builder.Entity<ProfileReport>().HasKey(r => r.Id);
        builder.Entity<ProfileReport>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ProfileReport>().Property(r => r.ReportedUserId).IsRequired();
        builder.Entity<ProfileReport>().Property(r => r.ReporterUserId).IsRequired();
        builder.Entity<ProfileReport>().Property(r => r.Reason).IsRequired().HasMaxLength(120);
        builder.Entity<ProfileReport>().Property(r => r.Description).HasMaxLength(500);
        builder.Entity<ProfileReport>().Property(r => r.Status).IsRequired().HasMaxLength(20);

        // Apply snake_case naming convention for the whole model
        builder.UseSnakeCaseNamingConvention();
    }
}
