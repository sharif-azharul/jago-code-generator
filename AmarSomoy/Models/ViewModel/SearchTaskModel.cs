using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AmarSomoy.Models.ViewModel
{
    public class SearchTaskModel
    {
        [DisplayName("Project")]

        public string ProjectCode { get; set; }
        [UIHint("DateOnly")]
        [DisplayName("Start Time")]
        public DateTime StartTime { get; set; }
        [DisplayName("End Time")]

        [UIHint("DateOnly")]

        public DateTime EndTime { get; set; }
    }
}