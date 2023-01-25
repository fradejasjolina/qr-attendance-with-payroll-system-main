using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    [Table("EmployeeLeaves")]
    class EmployeeLeave
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public string Fullname { get; set; }
        public DateTime DateHired { get; set; }
        public int LeaveCount { get; set; }
        public int YearsOfService { get; set; }
        public double CurrentLeave { get; set; }

        [NotMapped]
        public string DisplayDateFiled { get => DateHired != null ? DateHired.ToShortDateString() : ""; }
    }
}
