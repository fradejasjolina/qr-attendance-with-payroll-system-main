using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    [Table("CashAdvanceTracker")]
    class CashAdvanceTracker
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public int PayrollId { get; set; }
        public decimal Amount { get; set; }

    }
}
