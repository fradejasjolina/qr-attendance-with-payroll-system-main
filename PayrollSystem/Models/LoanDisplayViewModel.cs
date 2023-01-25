using DocumentFormat.OpenXml.Drawing.Charts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    internal class LoanDisplayViewModel
    {

        public int LoanId { get; set; }
        public int PayrollId { get; set; }
        public double MonthlyAmortization { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        [NotMapped]
        public string StartDateDisplay { get => StartDate.ToShortDateString(); }

        [NotMapped]
        public string EndDateDisplay { get => EndDate.ToShortDateString(); }
    }
}
