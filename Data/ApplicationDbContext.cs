using System;
using Microsoft.EntityFrameworkCore;
using DISFinalProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DISFinalProject.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Park> Parks { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<ParkActivity> ParkActivities { get; set; }
        public DbSet<ParkTopic> ParkTopics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParkActivity>()
            .Property(f => f.ID)
            .ValueGeneratedOnAdd();
            modelBuilder.Entity<ParkTopic>()
            .Property(f => f.ID)
            .ValueGeneratedOnAdd();
        }
    }
}
