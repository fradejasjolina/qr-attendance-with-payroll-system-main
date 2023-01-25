using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    internal class Loan
    { 

        [Key]
        public int Id { get; set; }
        public string EmployeeNo { get; set; }

        public string Type { get; set; }
        public int Terms { get; set; }

        public double? LoanAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? MonthlyAmortization { get; set; }
        public bool LoanStatus { get; set; }
    }

    internal class LoanViewModel
    {
        public int Id { get; set; }
        public string EmployeeNo { get; set; }

        public string Fullname { get; set; }
        public string Type { get; set; }
        public int Terms { get; set; }
        public double? LoanAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? MonthlyAmortization { get; set; }
    }
}
