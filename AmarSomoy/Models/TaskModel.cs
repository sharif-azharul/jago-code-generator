using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AmarSomoy.Models
{
    public class TaskModel : BaseModel
    {
        [DisplayName("Id")]
        public int TaskId { get; set; }
        [DisplayName("Company")]
        public string CompanyCode { get; set; }
        [NotMapped]
        public string CompanyName { get; set; }
        public CompanyModel Company { get; set; }
        [DisplayName("Project")]
        public string ProjectCode { get; set; }
        [NotMapped]
        public string ProjectName { get; set; }
        [UIHint("DateTime")]
        [DisplayName("Start Time")]
        public DateTime StartTime { get; set; }
        [UIHint("DateTime")]
        [DisplayName("End Time")]
        public DateTime EndTime { get; set; }
        [DisplayName("Description")]
        public string TaskDescription { get; set; }
        [DisplayName("Owner")]
        public string TaskOwnerId { get; set; }
        [NotMapped]
        public string TaskOwner { get; set; }

    }
}