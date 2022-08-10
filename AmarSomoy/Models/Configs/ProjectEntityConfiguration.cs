using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AmarSomoy.Models
{
    public class ProjectEntityConfiguration : EntityTypeConfiguration<ProjectModel>
    {
        public ProjectEntityConfiguration()
        {
            this.ToTable("T_Project");
            this.HasKey<string>(p => p.ProjectCode);
            this.Property(p => p.ProjectCode).HasMaxLength(36);
            this.Property(p => p.ProjectDescription).HasMaxLength(250);
            this.Property(p => p.CompanyCode).HasMaxLength(36);

        }

    }
}