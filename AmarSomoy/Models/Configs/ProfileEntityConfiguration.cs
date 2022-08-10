using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AmarSomoy.Models
{
    public class ProfileEntityConfiguration : EntityTypeConfiguration<ProfileModel>
    {
        public ProfileEntityConfiguration()
        {
            this.ToTable("T_Profile");
            this.HasKey<string>(p => p.ProfileCode);
            this.Property(p => p.ProfileCode).HasMaxLength(36);
            this.Property(p => p.ProfileId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.FirstName).HasMaxLength(50);
            this.Property(p => p.LastName).HasMaxLength(50);
            this.Property(p => p.Address).HasMaxLength(255);
            this.Property(p => p.CompanyCode).HasMaxLength(36);
            this.Property(p => p.DateOfBirth);
            this.Property(p => p.EmailId).HasMaxLength(128);
            this.Property(p => p.MobileNo).HasMaxLength(16);
            this.Property(p => p.UserId).HasMaxLength(36);
        }

    }
}