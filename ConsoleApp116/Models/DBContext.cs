using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp116.Models
{
    class AutoDBContext :DbContext
    {
        public DbSet<ColorAuto> Colors { get; set; }
        public DbSet<ModelAuto> Models { get; set; }
        public DbSet<TypeAuto> Types { get; set; }
        public DbSet<Auto> Autos { get; set; }
        public AutoDBContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433; Database=auto_garage;User=SA; Password=mynameissasha87");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auto>()
            .HasIndex(p => new { p.ID_Color, p.ID_Model, p.ID_Type, p.Number}).IsUnique();
        }
    }
}
