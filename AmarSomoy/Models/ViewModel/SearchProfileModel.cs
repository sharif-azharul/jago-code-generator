using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AmarSomoy.Models.ViewModel
{
    public class SearchProfileModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [UIHint("DateOnly")]
        public DateTime? DoB { get; set; }
    }
}