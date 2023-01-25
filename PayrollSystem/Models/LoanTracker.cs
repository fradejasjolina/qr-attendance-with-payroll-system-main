using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    [Table("loantracker")]
    internal class LoanTracker
    {
        [Key]
        public int Id { get; set; }
        public int LoanId { get; set; }
        public double? MonthlyAmortization { get; set; }
        public int PayrollId { get; set; }
    }
}
