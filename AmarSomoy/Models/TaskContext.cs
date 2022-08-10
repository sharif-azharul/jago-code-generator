using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AmarSomoy.Models
{
    public class TaskContext : DbContext
    {
        public TaskContext()
            : base("name=TaskDbConnectionString")
        {
        }
        public DbSet<CompanyModel> CompanyModels { get; set; }
        public DbSet<ProjectModel> ProjectModels { get; set; }
        public DbSet<ProfileModel> ProfileModels { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyModel>().ToTable("T_Company").HasKey(p => p.CompanyCode);
            modelBuilder.Entity<CompanyModel>().Property(c => c.CompanyCode)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ProjectModel>().ToTable("T_Project").HasKey(b => b.ProjectCode);
            modelBuilder.Entity<ProjectModel>().Property(b => b.ProjectCode)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ProjectModel>().HasRequired(p => p.Company)
                .WithMany(b => b.Projects).HasForeignKey(b => b.CompanyCode);

            modelBuilder.Entity<ProfileModel>().ToTable("T_Profile").HasKey(b => b.ProfileId);
            modelBuilder.Entity<ProfileModel>().Property(b => b.ProfileId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<ProfileModel>().HasRequired(p => p.Company)
                .WithMany(b => b.Profiles).HasForeignKey(b => b.CompanyCode);

          

            modelBuilder.Configurations.Add(new ProfileEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}