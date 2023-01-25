using DocumentFormat.OpenXml.Drawing;
using PayrollSystem.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    class Payroll
    {
        [Key]
        public int Id { get; set; }

        #region Employee Details
        public string EmployeeNo { get; set; }
        public string FullName { get; set; }
        public string EmployeeStatus { get; set; }
        #endregion

        #region Employee Salary

        public decimal Salary { get; set; }
        //D
        [NotMapped]
        public decimal BasicPay
        {
            get
            {
                var salary = decimal.Parse(s: Salary.ToString("0.00"));

                var result = salary / 2;

                return result;

                // (float)Decimal.Round(( Convert.ToDecimal(Salary.ToString()) / 2), 2);
            }
        }
        //E
        [NotMapped]
        public decimal DailyRate { get => (decimal)Decimal.Round(((Decimal)BasicPay / 11), 2); }

        #endregion

        #region Work Days
        //G

        public decimal WorkDays { get; set; }

        public decimal WorkDaysAmount { get; set; }

        #endregion

        #region Overtime, Regular Holiday, Special Holiday

        //INPUT
        //H
        public decimal OverTimeMinutes { get; set; }

        //I
        public decimal WorkOvertimeAmount
        {
            get
            {
                var x = Math.Round((((DailyRate * 1.3M / 9M / 60)) * OverTimeMinutes), 2);
                return x;
            }
        }

        //INPUT
        //J
        public decimal RegularHolidayDays { get; set; }

        //K
        public decimal RegularHolidayAmount { get => (decimal)(RegularHolidayDays * DailyRate); }

        //INPUT
        //L
        public decimal OverTimeRegularHolidayMinutes { get; set; }

        //M
        public decimal TotalRegularHolidayAmount
        {
            get
            {

                var y = (decimal)(((((DailyRate * 2) * 1.3M) / 9M) / 60) * OverTimeRegularHolidayMinutes);
                return Decimal.Parse(y.ToString("0.00"));
            }
        }

        //INPUT
        //N
        public decimal SpecialHolidayDays { get; set; }

        //O
        public decimal SpecialHolidayAmount
        {
            get
            {
                var dailyRate = Salary / 22;
                //var s = 570 * SpecialHolidayDays;
                var result = (dailyRate / 9M) * 0.3M * (SpecialHolidayDays / 60);

                return Math.Round(result, 2);
            }
        }

        //INPUT
        //P
        public decimal OTSpecialHolidayMinutes { get; set; }

        //Q
        public decimal OtSpecialHolidayAmount
        {
            get
            {
                var x = Math.Round((((DailyRate * 1.3M * 1.3M / 9M / 60)) * OTSpecialHolidayMinutes), 2);
                return x;
            }
        }
        public decimal NightTimeMinutes { get; set; }

        //R
        public decimal NightTimeAmount
        {
            get
            {
                var z = Math.Round((((DailyRate / 9 * 0.10M * 9M / 60 )) * NightTimeMinutes), 2);
                return z;
            }
        }

        #endregion

        #region Weekend Duty

        //INPUT
        public decimal WeekendMinutes { get; set; }

        [NotMapped]
        public decimal WeekendDutyAmount
        {
            get
            {
                var comp = (decimal)((((DailyRate * 1.3M) / 9M) / 60) * WeekendMinutes);

                return Decimal.Parse(comp.ToString("0.00"));
            }
        }

        #endregion

        #region Transportation and Allowance
        public decimal TranspoAllowance { get; set; }
        public decimal Allowance { get; set; }


        #endregion

        #region Absent

        public decimal TotalMinutesOfAbsent { get; set; }

        [NotMapped]
        public decimal AbsentAmount
        {
            get
            {
                //if (EmployeeStatus.Equals("Daily"))
                //{
                //    return 0;
                //}
                
                var absentAmount = (decimal)(((DailyRate / 540)) * TotalMinutesOfAbsent);

                return Decimal.Parse(absentAmount.ToString("0.00"));
            }
        }
        #endregion

        #region Tardiness
        public decimal TotalMinutesOfLate { get; set; }

        public decimal MinutesAmount
        {
            get
            {
                if (EmployeeStatus.Equals("Managerial"))
                {
                    return 0;
                }

                var lateAmount = (decimal)(((DailyRate / 540)) * TotalMinutesOfLate);


                return Decimal.Parse(lateAmount.ToString("0.00"));

            }
        }


        #endregion

        #region Undertime
        public decimal TotalMinutesOfUndertime { get; set; }


        public decimal UndertimeAmount
        {
            get
            {
                if (EmployeeStatus.Equals("Managerial"))
                {
                    return 0;
                }

                var x = (decimal)((((DailyRate / 540))) * TotalMinutesOfUndertime);
                return Decimal.Parse(x.ToString("0.00"));

            }
        }


        #endregion

        #region Vale
        public decimal Vale { get; set; }

        #endregion

        #region Gross Income

        public decimal GrossIncome
        {
            get
            {

                if (EmployeeStatus == "Daily")
                {
                    var amount = WorkDaysAmount;

                    decimal x = amount
                    + WorkOvertimeAmount
                    + RegularHolidayAmount
                    + TotalRegularHolidayAmount
                    + SpecialHolidayAmount
                    + OtSpecialHolidayAmount
                    + WeekendDutyAmount
                    //- AbsentAmount
                    - MinutesAmount
                   - UndertimeAmount ;
                

                    //x = x > BasicPay ? BasicPay : x;


                    var result = Decimal.Parse(x.ToString("0.00"));

                    return x > 0 ? result : 0;
                }
                else
                {
                    var amount = BasicPay;
                    decimal x = amount

                   + WorkOvertimeAmount
                   + RegularHolidayAmount
                   + TotalRegularHolidayAmount
                   + SpecialHolidayAmount
                   + OtSpecialHolidayAmount
                   + WeekendDutyAmount
                   - MinutesAmount
                   - AbsentAmount
                   - UndertimeAmount;


                    //x = x > BasicPay ? BasicPay : x;


                    var result = Decimal.Parse(x.ToString("0.00"));

                    return x > 0 ? result : 0;
                }

               
            }
        }




        #endregion

        #region Philhealth 

        public decimal PhilhealthContribution { get; set; }
        #endregion

        #region Pagibig
        public decimal PagIbigContribution { get; set; }
        public decimal PagibigSalaryLoan { get; set; }
        public decimal PagibigCalamityLoan { get; set; }

        #endregion

        #region SSS

        public decimal SssContribution { get; set; }
        public decimal SssProvidentFund { get; set; }
        public decimal SssSalaryLoan { get; set; }
        public decimal SssCalamityLoan { get; set; }


        #endregion

        #region Others
        public decimal Others { get; set; }

        #endregion

        #region Total Salary


        public decimal TotalSalary
            {
            get
            {
                var x = GrossIncome + Allowance - Vale - PagIbigContribution - PhilhealthContribution - Others
                    - PagibigCalamityLoan - PagibigSalaryLoan - SssCalamityLoan - SssContribution - SssProvidentFund 
                    - SssSalaryLoan + Adjustments + NightTimeAmount;

                return x > 0 ? x : 0;
            }
        }



        #endregion




        public int PayrollId { get; set; }

        public decimal Adjustments { get; set; }


        [NotMapped]
        public bool IsEdit { get; set; }

    }
}
