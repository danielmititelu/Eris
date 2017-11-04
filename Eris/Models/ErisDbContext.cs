using Microsoft.EntityFrameworkCore;

namespace Eris.Models
{
    public partial class ErisDbContext : DbContext
    {
        public virtual DbSet<Task> Task { get; set; }

        public ErisDbContext(DbContextOptions<ErisDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).HasColumnType("nchar(100)");
            });
        }
    }
}
