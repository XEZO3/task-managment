
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CV.DAL.Data
{
    public class dbContext : DbContext
    {
        public dbContext(DbContextOptions<dbContext> options) : base(options) { 
        
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            //DESKTOP-JD76U9C
            //LAPTOP-NNUMAB6J
            builder.UseSqlServer("Server=LAPTOP-NNUMAB6J;Database=Task;Trusted_Connection=True;Trust Server Certificate=true;");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Users>()
                .HasIndex(u => u.Email)
                .IsUnique();
           builder.Entity<Users>().HasKey(u => u.Id);
        }


        DbSet<Users> Users { get; set; }
        DbSet<Tasks> Task { get; set; }

    }
}
