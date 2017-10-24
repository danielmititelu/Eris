using Microsoft.EntityFrameworkCore;

namespace Eris.Models
{
    public partial class ErisDbContext : DbContext
    {
        public virtual DbSet<FirstTable> FirstTable { get; set; }

        public ErisDbContext(DbContextOptions<ErisDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FirstTable>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("nchar(100)");
            });
        }
    }
}
