using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QRAttendanceSystem.Models
{
    public class Profile
    {
        [Key]
        public Guid ProfileId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        //Regular    = 8-6 fixed sched
        //Irregular = Hourly Rate
        public string EmploymentStatus { get; set; }
    }
}
