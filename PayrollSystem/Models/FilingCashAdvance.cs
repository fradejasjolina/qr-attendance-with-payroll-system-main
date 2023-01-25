using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    [Table("FilingCashAdvances")]
    class FilingCashAdvance
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public string EmployeeName { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateFiled { get; set; }
        public string Status { get; set; }

    }
}
