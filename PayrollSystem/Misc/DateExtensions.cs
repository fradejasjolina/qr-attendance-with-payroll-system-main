using DocumentFormat.OpenXml.Drawing.Diagrams;
using PayrollSystem.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Misc
{
    public class ActualAttendance
    {
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public DateTime LogDate { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }


    }

    public static class DateExtensions
    {
        public static IEnumerable<DateTime> EachDay(this DateTime from, DateTime thru)
        {
            while (from.AddDays(1) <= thru.Date)
            {
                yield return from.AddDays(1);
            }
        }

        public static int GetWorkingDays(this DateTime from, DateTime to)
        {
            var dayDifference = (int)to.Subtract(from).TotalDays;
            return Enumerable
                .Range(1, dayDifference)
                .Select(x => from.AddDays(x))
                .Count(x => x.DayOfWeek != DayOfWeek.Saturday && x.DayOfWeek != DayOfWeek.Sunday);
        }
        public static bool IsBewteenTwoDates(this DateTime dt, DateTime start, DateTime end)
        {
            return dt >= start && dt <= end;
        }
        private static List<ActualAttendance> GetActualAttendances(List<Models.Attendance> collection)
        {
            var attendance = new List<ActualAttendance>();

            foreach (var item in collection)
            {
                if (string.IsNullOrEmpty(item.TIMEOUT)) continue;

                var isLogDateValid = DateTime.TryParse(item.LOGDATE, out DateTime logDate);
                var isTimeInValid = DateTime.TryParse(item.TIMEIN, out DateTime timeIn);
                var isTimeOutValid = DateTime.TryParse(item.TIMEOUT, out DateTime timeOut);

                var model = new ActualAttendance()
                {
                    Id = item.ID,
                    EmployeeNo = item.STUDENTID,
                    LogDate = logDate,
                    TimeIn = timeIn,
                    TimeOut = timeOut
                };
                attendance.Add(model);
            }
            return attendance;

        }

        /// <summary>
        /// Etong code na to para to bilangin kung ano yung total working days nya
        /// if half day 0.5 if whole day 1 working day agad
        /// </summary>
        /// <param name="collection"></param>
        /// <returns>Actual Working Days</returns>
        public static decimal GetWorkingDays(this List<Models.Attendance> collection)
        {
            double workingDays = 0;
            DateTime.TryParse("08:00 AM", out DateTime startShift);
            DateTime.TryParse("6:00 PM", out DateTime endShift);
            DateTime.TryParse("12:00 PM", out DateTime startBreak);
            DateTime.TryParse("1:00 PM", out DateTime endBreak);

            var attendances = GetActualAttendances(collection);
            foreach (var attendance in attendances)
            {
                var actualStartShift = attendance.TimeIn;
                var actualEndShift = attendance.TimeOut;

                var halfDayValid = IsHalfDayValid(actualStartShift, actualEndShift, startBreak, endBreak, startShift, endShift);
                if (halfDayValid == 0)
                {
                    workingDays++;
                }
                else
                {
                    workingDays += 0.5;
                }
            }
            return (decimal)workingDays;
        }

        private static decimal IsHalfDayValid(
            DateTime actualStartShift,
            DateTime actualEndShift,
            DateTime breakStart,
            DateTime breakEnd,
            DateTime startShift,
            DateTime endShift
            )
        {

            //check mo to 
            //actualShift = 8AM
            //breakEnd = 1PM
            //


            //12:01 PM
            var afternoonShift = breakStart.AddMinutes(1);

            //wait


            //7:01 AM <= 12:00 PM

            var isMorningShift = (actualStartShift <= breakStart);

            if (isMorningShift)
            {
                //morning shift
                //meaning ang uwi nya is 12:00PM - 01:00 PM
                if (actualEndShift >= breakStart && actualEndShift <= breakEnd)
                {
                    return 300;
                }

            }
            else
            {
                //afternoon shift

                if (actualEndShift >= endShift)
                {
                    return 240;
                }
            }
            return 0;
        }

        public static decimal GetAbsent(this decimal currentAttendanceMinutes, List<Models.Attendance> collection)
        {
            decimal additionalAbsentMinutes = 0;
            DateTime.TryParse("6:00 PM", out DateTime endShift);
            DateTime.TryParse("8:00 AM", out DateTime startShift);
            DateTime.TryParse("12:00 PM", out DateTime startBreak);
            DateTime.TryParse("1:00 PM", out DateTime endBreak);

            var attendance = GetActualAttendances(collection);

            foreach (var item in attendance)
            {
                var actualStartShift = item.TimeIn;
                var actualEndShift = item.TimeOut;
                var halfdayValid = IsHalfDayValid(actualStartShift, actualEndShift, startBreak, endBreak, startShift, endShift);

                additionalAbsentMinutes += halfdayValid;

                Console.WriteLine();

            }
            var result = additionalAbsentMinutes + currentAttendanceMinutes;

            return result;
        }

        private static decimal ComputedPayBasedOnRate(decimal dailyRate, int value)
        {
            var computation = Math.Round(((dailyRate / 540) * value), 2);
            return computation;
        }
        public static decimal TotalWorkingDays(this decimal dailyRate, List<Models.Attendance> collection)
        {
            decimal totalWorkingDays = 0;
            DateTime.TryParse("6:00 PM", out DateTime endShift);
            DateTime.TryParse("08:00 AM", out DateTime startShift);
            DateTime.TryParse("12:00 PM", out DateTime breakTimeStart);
            DateTime.TryParse("1:00 PM", out DateTime breakTimeEnd);

            var attendance = GetActualAttendances(collection).OrderBy(x => x.LogDate);

            #region Compute the daily rate for the whole day of work
            foreach (var item in attendance)
            {
                var timeIn = item.TimeIn <= startShift ? startShift : item.TimeIn;
                var timeOut = item.TimeOut;

                //pag pumasok ka ng 8am - 12pm
                // half-day morning (+240)
                if (timeOut >= breakTimeStart && timeOut <= breakTimeEnd)
                {
                    var result = ComputedPayBasedOnRate(dailyRate, 240);
                    totalWorkingDays += result;
                }
                ////half-day afrernoon(300);
                
                // 1:00:44 PM >= 1:00:00 PM && 6:01 PM >= 6:00
                else if (timeIn >= breakTimeEnd && timeOut >= endShift)
                {
                    //dito dapat sya babagsak
                    var result = ComputedPayBasedOnRate(dailyRate, 300);
                    totalWorkingDays += result;
                }
                else if (timeOut >= endShift)
                {
                    totalWorkingDays += dailyRate;
                }
                else if(timeOut <= endShift)
                {
                    totalWorkingDays += dailyRate;
                }



            }
            #endregion


            return totalWorkingDays;
        }


        public static decimal ActualAbsentCount(this List<Models.Attendance> collection, List<DateTime> bussinessDays)
        {
            decimal days = 0M;
            var attendance = GetActualAttendances(collection).OrderBy(x => x.LogDate).ToList();


            foreach (var item in bussinessDays)
            {
                var result = attendance.Where(x => x.LogDate == item).FirstOrDefault();
                if (result == null) days++;

            }

            return days;
        }










        public static int GetUndertime()
        {
            return 0;
        }
    }
}
