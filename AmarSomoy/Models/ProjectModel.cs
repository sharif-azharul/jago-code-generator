using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace AmarSomoy.Models
{
    public class ProjectModel : BaseModel
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public string CompanyCode { get; set; }
        public CompanyModel Company { get; set; }

        public ICollection<TaskModel> Tasks { get; set; }
    }
}