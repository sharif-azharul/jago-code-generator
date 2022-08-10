using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmarSomoy.Models.ViewModel
{
    public class TaskViewModel
    {
        public int TaskId { get; set; }
        public string TaskDescription { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public TimeSpan TSWorkingHour
        {
            get
            {
                TimeSpan diff = EndTime - StartTime;
                return diff;
            }
            set { }
        }
        public string WorkingHour
        {
            get
            {
                TimeSpan diff = EndTime - StartTime;
                return diff.ToString("h'h 'm'm 's's'");
                //double hours = diff.TotalHours;
                //return hours;
            }
            set { }
        }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }

        public Boolean IsSelectedReport { get; set; }
        public string trColor { get; set; }
    }
}