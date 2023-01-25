using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    class QrCode
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public string QrCodePath { get; set; }
        public string QrCodeValue { get; set; }

    }
}
