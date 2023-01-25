using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    class Breakdown
    {
        public string Month { get; set; }
        public string Cutoff1 { get; set; }
        public decimal CutoffValue1 { get; set; }
        public string Cutoff2 { get; set; }
        public decimal CutoffValue2 { get; set; }
        public decimal Total { get => CutoffValue1 + CutoffValue2; }
    }
}
