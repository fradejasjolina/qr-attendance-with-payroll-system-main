using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    class CutOff
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
        [NotMapped]
        public string DisplayStartDate { get=>$"{StartDate.ToShortDateString()}"; }

        [NotMapped]
        public string DisplayEndDate { get => $"{EndDate.ToShortDateString()}"; }

        [NotMapped]
        public string DisplayDate { get=>$"{StartDate.ToShortDateString() } - {EndDate.ToShortDateString() }"; }

    }
}
