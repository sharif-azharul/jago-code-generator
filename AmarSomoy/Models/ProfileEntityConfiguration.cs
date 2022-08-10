using System;
using System.Collections.Generic;
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
            this.HasKey<int>(p => p.ProfileId);
            this.Property(p => p.FirstName).HasMaxLength(30);
            this.Property(p => p.LastName).HasMaxLength(30);
            this.Property(p => p.Address).HasMaxLength(250);
            this.Property(p => p.EmailId).HasMaxLength(60);
            this.Property(p => p.MobileNo).HasMaxLength(15);
            this.HasRequired<CompanyModel>(p => p.Company)
                .WithMany(p => p.Profiles)
                .HasForeignKey(p => p.CompanyCode);

        }


    }
}