using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QRAttendanceSystem.Models
{
    public class UserAccount
    {
        [Key]
        public Guid UserId { get; set; }
        public Guid ProfileId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


    }
}
