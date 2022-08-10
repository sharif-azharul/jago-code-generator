using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmarSomoy.Models.ViewModel
{
    public class TaskReportViewModel
    {
        public ProfileModel profile { get; set; }
        public List<TaskViewModel> tasks{ get; set; }

        public TimeSpan TSTotalWorkingHour
        {
            get;
            set;
        }
        public string TotalWorkingHour
        {
            get;
            set;
        }

    }
}