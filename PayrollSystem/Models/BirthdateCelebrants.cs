using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    class BirthdateCelebrants
    {
        public string EmployeeNo { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public DateTime Birthdate { get; set; }
        public string PicturePath { get; set; }

        public string BirthdateDisplay { get {
                if(Birthdate == null)
                {
                    return null;
                }

                return Birthdate.ToLongDateString();
            }
        }

        public string YearsOfService { get {
                if (Birthdate == null)
                {
                    return null;
                }
                var yearsOfService = DateTime.Now.Year  - Birthdate.Year;

                var result = $"{yearsOfService} Year(s)";
                return result;
            }
        
        }


    }
}
