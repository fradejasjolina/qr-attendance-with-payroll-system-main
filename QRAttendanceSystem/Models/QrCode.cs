using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRAttendanceSystem.Models
{
    public class QrCode
    {
        [Key]
        public Guid QrCodeId { get; set; }
        public string QrCodeValue { get; set; }
        public string QrPath { get; set; }
    }
}
