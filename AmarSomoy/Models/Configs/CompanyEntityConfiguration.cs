using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AmarSomoy.Models
{
    public class CompanyEntityConfiguration : EntityTypeConfiguration<CompanyModel>
    {
        public CompanyEntityConfiguration()
        {
            this.ToTable("T_Company");
            this.HasKey<string>(p => p.CompanyCode);
            this.Property(p => p.CompanyCode).HasMaxLength(36);
            this.Property(p => p.CompanyName).HasMaxLength(50);
            this.Property(p => p.CompanyAddress).HasMaxLength(250);
            
        }

    }
}