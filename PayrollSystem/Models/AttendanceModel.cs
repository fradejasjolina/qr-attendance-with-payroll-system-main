using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    class AttendanceModel
    {
        public string EmployeeNo { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? TimeFrom { get; set; }
        public DateTime? TimeTo { get; set; }

        public string DateString
        {
            get => Date == null ? "" :  ((DateTime)Date).ToString("dd-MM-yyyy");
        }

        public string TimeFromString
        {
            get => TimeFrom == null ? "" :  ((DateTime)TimeFrom).ToString("HH:mm:ss"); 
        }

        public string TimeToString
        {
            get =>TimeTo == null? "" : ((DateTime)TimeTo).ToString("HH:mm:ss");
        }

    }
}
