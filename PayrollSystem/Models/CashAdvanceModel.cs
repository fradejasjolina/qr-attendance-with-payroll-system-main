using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    [Table("CashAdvances")]
    class CashAdvanceModel
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public string Fullname { get; set; }
        public decimal Amount { get; set; }
        public decimal RemainingBalance { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
