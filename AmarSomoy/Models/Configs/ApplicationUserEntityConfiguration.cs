using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AmarSomoy.Models
{
    public class ApplicationUserEntityConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserEntityConfiguration()
        {
            //this.ToTable("StudentInfo");

            //this.HasKey<int>(s => s.StudentKey);


            //this.Property(p => p.DateOfBirth)
            //        .HasColumnName("DoB")
            //        .HasColumnOrder(3)
            //        .HasColumnType("datetime2");

            //this.Property(p => p.StudentName)
            //        .HasMaxLength(50);

            //this.Property(p => p.StudentName)
            //        .IsConcurrencyToken();

            //this.HasMany<Course>(s => s.Courses)
            //    .WithMany(c => c.Students)
            //    .Map(cs =>
            //    {
            //        cs.MapLeftKey("StudentId");
            //        cs.MapRightKey("CourseId");
            //        cs.ToTable("StudentCourse");
            //    });

            //===============
            this.ToTable("User");
            this.HasKey<string>(p => p.Id);
            this.Property(p => p.Id).HasColumnName("UserId").HasMaxLength(36);
            this.Property(p => p.UserName).HasMaxLength(128);
            this.Property(p => p.FirstName).HasMaxLength(50);
            this.Property(p => p.LastName).HasMaxLength(50);
            this.Property(p => p.AccessFailedCount);

            this.Property(p => p.Email).HasMaxLength(128);
            this.Property(p => p.EmailConfirmed);
            this.Property(p => p.PasswordHash).HasMaxLength(128);
            this.Property(p => p.PhoneNumber).HasMaxLength(16);
            this.Property(p => p.PhoneNumberConfirmed);
            this.Property(p => p.TwoFactorEnabled);
            this.Property(p => p.SecurityStamp);
            this.Property(p => p.LockoutEnabled);
            this.Property(p => p.LockoutEndDateUtc);

            this.HasMany(c => c.Logins).WithOptional().HasForeignKey(c => c.UserId);
            this.HasMany(c => c.Claims).WithOptional().HasForeignKey(c => c.UserId);
            this.HasMany(c => c.Roles).WithRequired().HasForeignKey(c => c.UserId);
        }
    }
}