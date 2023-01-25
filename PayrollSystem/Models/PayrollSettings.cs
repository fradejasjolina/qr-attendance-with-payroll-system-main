using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Models
{
    class PayrollSettings
    {
        public bool IsSalaryEnabled { get; set; }
        public bool IsBasicPayEnabled { get; set; }
        public bool IsWorkDaysAmountEnabled { get; set; }
        public bool IsOvertimeEnabled { get; set; }
        public bool IsRegularHolidayAmountEnabled { get; set; }
        public bool IsRegularHolidayOtMinsAmountEnabled { get; set; }
        public bool IsSpecialHolidayAmountEnabled { get; set; }
        public bool IsSpecialHolidayOtMinsAmountEnabled { get; set; }
        public bool IsSaturdaySundayDutyAmountEnabled { get; set; }
        public bool IsTranspoAllowanceEnabled { get; set; }
        public bool IsAllowanceEnabled { get; set; }
        public bool IsDailyRateEnabled { get; set; }

        public bool IsPayrollPrintEnabled { get; set; }
        public bool IsNightTimeEnabled { get; set; }
    }
}
