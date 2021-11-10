using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QRAttendanceSystem.Models
{
    public class UserLog
    {
        [Key]
        public int LogId { get; set; }
        public Guid UserId  { get; set; }
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public bool IsLate { get; set; }
    }
}
