using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace AmarSomoy.Models
{
    public class TaskEntityConfiguration : EntityTypeConfiguration<TaskModel>
    {
        public TaskEntityConfiguration()
        {
            this.ToTable("T_Task");
            this.HasKey<int>(p => p.TaskId);
            this.Property(p => p.TaskId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.TaskOwnerId).HasMaxLength(36);
            this.Property(p => p.TaskDescription).HasMaxLength(250);
            this.Property(p => p.StartTime);
            this.Property(p => p.EndTime);
            this.Property(p => p.ProjectCode).HasMaxLength(36);
        }
    }
}