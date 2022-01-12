using Microsoft.EntityFrameworkCore;
using PaymentService.Models;

namespace PaymentService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) :
        base(opt)
        {

        }

        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
            .HasMany(p => p.Payments)
            .WithOne(p => p.Enrollment!)
            .HasForeignKey(p => p.EnrollmentId);

            modelBuilder.Entity<Payment>()
            .HasOne(p => p.Enrollment)
            .WithMany(p => p.Payments)
            .HasForeignKey(p => p.EnrollmentId);
        }
    }
}