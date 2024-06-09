using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuyenDung.Data.Model;

namespace TuyenDung.Data.DataContext
{
    public class MyDb : DbContext
    {
        public MyDb(DbContextOptions options) : base(options) 
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Applications> Applications { get; set; }
        public DbSet<Employers> Employers { get; set; }
        public DbSet<Job_seekers> Job_Seekers { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Messages> Messages { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }
        public DbSet<FormCv> FormCvs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Jobs>()
                .Property(x => x.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Jobs>()
                .Property(x => x.JobType)
                .HasConversion<string>();

            modelBuilder.Entity<Applications>()
                .Property(x => x.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Applications>()
                .Property(x => x.StatusSubmissionType)
                .HasConversion<string>();

            modelBuilder.Entity<Jobs>()
                .Property(x => x.Salary)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Messages>()
                .Property(x => x.Content)
                .HasColumnType("TEXT");
        }
    }
}
