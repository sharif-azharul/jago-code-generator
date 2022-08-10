using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmarSomoy.Models
{
    public class ProfileModel : BaseModel
    {
        public string ProfileCode { get; set; }
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string CompanyCode { get; set; }
        public CompanyModel Company { get; set; }
        public string UserId { get; set; }

    }
}