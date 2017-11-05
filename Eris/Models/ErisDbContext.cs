using Microsoft.EntityFrameworkCore;

namespace Eris.Models
{
    public partial class ErisDbContext : DbContext
    {
        public virtual DbSet<TodoItem> ToDoItem { get; set; }

        public ErisDbContext(DbContextOptions<ErisDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.Property(e => e.Name).HasColumnType("varchar(100)");
            });
        }
    }
}
