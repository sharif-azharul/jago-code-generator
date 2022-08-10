using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmarSomoy.Models
{
    public class CompanyModel : BaseModel
    {
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public ICollection<TaskModel> Projects { get; set; }

        public ICollection<ProfileModel> Profiles{ get; set; }

        

    }
}