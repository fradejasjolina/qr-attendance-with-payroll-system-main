using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PayrollSystem.Models
{
    [Table("attendance")]
    public class Attendance
    {
        [Key]
        public int ID { get; set; }
        public string STUDENTID { get; set; }

        public string TIMEIN { get; set; }

        public string TIMEOUT { get; set; }

        public string LOGDATE { get; set; }

        public string TOTAL_LATE { get; set; }

        public string STATUS { get; set; }


        public string Fullname { get => !string.IsNullOrEmpty(STUDENTID) ? GetFullname(STUDENTID) : null;  }
        


        private string GetFullname(string STUDENTID)
        {
            using (var context = new Database.PayrollDbContext())
            {
                var user = context.Employees.Where(x => x.EmployeeNo == STUDENTID).FirstOrDefault();
                if (user != null)
                {
                    return $"{user.Lastname} {user.Firstname}";
                }
            }
            return null;
        }

    }
}
