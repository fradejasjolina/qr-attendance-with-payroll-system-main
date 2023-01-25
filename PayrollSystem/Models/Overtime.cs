using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    [Table("Overtimes")]
    class Overtime
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public DateTime DateOverTime { get; set; }
        public DateTime OvertimeFrom { get; set; }
        public DateTime OvertimeTo { get; set; }
        public string Reasons { get; set; }
        public string Status { get; set; }
        public string Fullname { get; set; }

        [NotMapped]
        public TimeSpan TotalHours 
        { 
            get
            {
                return OvertimeTo.Subtract(OvertimeFrom);
            }
        }

        [NotMapped]
        public string DisplayDateOvertime
        {
            get
            {
                return DateOverTime.ToShortDateString();
                //return OvertimeTo.Subtract(OvertimeFrom);
            }
        }


        //hh:mm:ss tt

        [NotMapped]
        public string DisplayTo
        {
            get
            {
                return OvertimeTo.ToString("hh:mm:ss tt");
                //return OvertimeTo.Subtract(OvertimeFrom);
            }
        }

        [NotMapped]
        public string DisplayFrom
        {
            get
            {
                return OvertimeFrom.ToString("hh:mm:ss tt");
                //return OvertimeTo.Subtract(OvertimeFrom);
            }
        }

        [NotMapped]
        public double DisplayDurations
        {
            get
            {
                return TotalHours.TotalMinutes;

            }

        }





    }
}
