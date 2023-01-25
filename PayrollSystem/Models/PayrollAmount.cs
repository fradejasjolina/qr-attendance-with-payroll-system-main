using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    public class PayrollAmount
    {
        [Key]
        public int Id { get; set; }
        public int PayrollId { get; set; }
        public string  EmployeeNo { get; set; }
        public int GrossIncomeAmount { get; set; }
        public int TakeHomePayAmount { get; set; }
    }
}

