using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    class Employee
    {
        [Key]
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public DateTime? Birthdate { get; set; }
        public string Position { get; set; }
        public DateTime? HiringDate { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string Address { get; set; }
        public double BasicPay { get; set; }
        public string SssNumber { get; set; }
        public string Philhealth { get; set; }
        public string Pagibig { get; set; }
        public string AccountNo { get; set; }

        public string TinNo { get; set; }
        public string Allowance { get; set; }
        public string MedicalAllowance { get; set; }

        public string DentalAllowance { get; set; }

        public string VisionAllowance { get; set; }

        public string TranspoAllowance { get; set; }
        public string EmployeePicturePath { get; set; }
        public string Department { get; set; }


        [NotMapped]
        public string Fullname { get => $"{Firstname} {Lastname}" ; }
        
        [NotMapped]
        public string DisplayBirthDate { get => Birthdate != null  ? ((DateTime)Birthdate).ToShortDateString() : "" ; }

        [NotMapped]
        public string DisplayHiringDate { get => HiringDate != null ? ((DateTime)HiringDate).ToShortDateString() : ""; }
   }
}
