using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    [Table("Leaves")]
    class LeaveModel
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public string Fullname { get; set; }
        public string Department { get; set; }
        public DateTime DateFiled { get; set; }
        public string LeaveType { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        [NotMapped]
        public string DisplayDateFiled { get => DateFiled != null ? DateFiled.ToShortDateString() : "";  }
    }
}
