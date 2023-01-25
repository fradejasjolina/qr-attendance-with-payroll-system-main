using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Misc
{
    public class LeaveExtensions
    {

        public LeaveExtensions()
        {
            using (var context = new Database.PayrollDbContext())
            {
                var dateUtcNow= DateTime.UtcNow;

                var employees = context.Employees.Where(x => ((DateTime)x.HiringDate).Month == dateUtcNow.Month).ToList();
                foreach (var item in employees)
                {
                    var hiringDate = ((DateTime)item.HiringDate);

                    if (hiringDate.Day == dateUtcNow.Day)
                    {
                        var employee = context.EmployeeLeaves.Where(x => x.EmployeeNo == item.EmployeeNo).FirstOrDefault();

                        if (employee != null)
                        {

                            var totalCurrentLeave = GetCurrentLeave(item.HiringDate) + employee.CurrentLeave;
                            int totalLeaveCount = GetLeaveCount(item.HiringDate);
                            employee.CurrentLeave = totalCurrentLeave;
                            employee.LeaveCount = totalLeaveCount;
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Walang may tenure today!,{item.DisplayHiringDate}");
                    }
                }
                Console.WriteLine();
            }
        }

        private int GetLeaveCount(DateTime? hiringDate)
        {
            var leaveCount = 0;

            var years = DateTime.Now.Year - ((DateTime)hiringDate).Year;

            if (years == 0)
            {
                leaveCount = 0;
            }
            else if (years == 1 || years == 2)
            {
                leaveCount = 5;
            }
            else if (years >= 3 && years <= 4)
            {
                leaveCount = 10;
            }
            else if (years >= 5 && years <= 9)
            {
                leaveCount = 15;
            }
            else if (years >= 10 && years <= 14)
            {
                leaveCount = 20;
            }
            else if (years >= 15 && years <= 19)
            {
                leaveCount = 25;
            }
            else if (years >= 20)
            {
                leaveCount = 30;
            }

            return leaveCount;
        }

        private int GetCurrentLeave(DateTime? hiringDate)
        {
            var leaveCount = 0;

            var years = DateTime.Now.Year - ((DateTime)hiringDate).Year;

            if (years == 0)
            {
                leaveCount = 0;
            }
            else if (years == 1)
            {
                leaveCount = 5;
            }
            else if (years == 3)
            {
                leaveCount = 5;
            }
            else if (years == 5)
            {
                leaveCount = 5;
            }
            else if (years == 10 )
            {
                leaveCount = 5;
            }
            else if (years == 15)
            {
                leaveCount = 5;
            }
            else if (years == 20)
            {
                leaveCount = 5;
            }

            return leaveCount;
        }



    }
}
