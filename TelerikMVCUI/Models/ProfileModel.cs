using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TelerikMVCUI.Models
{
    public class ProfileModel
    {
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //[DataType(DataType.Date)]
        [UIHint("DateOnly")]
        public DateTime? DateOfBirth { get; set; }
        [UIHint("Boolean")]
        public Boolean IsActive { get; set; }
    }
}