using Microsoft.EntityFrameworkCore;

namespace Todos.Models
{
    public class TodosDbContext : DbContext
    {
        public TodosDbContext(DbContextOptions<TodosDbContext> options)
           : base(options)
        {
        }

        public DbSet<Todo> Todo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<Todo>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
