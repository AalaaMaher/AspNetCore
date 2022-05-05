using Microsoft.EntityFrameworkCore;
using lab3.Models;

namespace lab3.Data
{
    public class ITIdbcontextcs :DbContext
    {
        public virtual DbSet<department> Departments { get; set; }
        public virtual DbSet<student> Students { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=ITIlab3 ; Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         //  modelBuilder.Entity<department>().HasKey(d=>d.ID);

            base.OnModelCreating(modelBuilder);
        }
    }
}
