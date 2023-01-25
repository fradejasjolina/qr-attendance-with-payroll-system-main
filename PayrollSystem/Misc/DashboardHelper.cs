using PayrollSystem.Models;
using System.Collections.Generic;

namespace PayrollSystem.Misc
{
    internal class DashboardHelper
    {
        public List<DashboardMenu> DashboardMenus(string userType)
        {
            var dashboardMenus = new List<DashboardMenu>();

            switch (userType)
            {
                case "Administrator":
                case "HR":
                    dashboardMenus = new List<DashboardMenu>(){
                            new DashboardMenu{ Title ="Home" ,Kind="ViewDashboard"},
                            new DashboardMenu{ Title ="Employees",Kind="Account" },
                            new DashboardMenu{ Title ="QR Code" ,Kind="QrcodeScan"},
                            new DashboardMenu{ Title ="Cut-Offs" ,Kind="Calendar"},
                            new DashboardMenu{ Title ="Attendance" ,Kind="ContentPaste"},
                            new DashboardMenu{ Title = "Overtime",Kind = "Account"},
                            new DashboardMenu{ Title = "Loans", Kind ="Cash"},
                            new DashboardMenu{ Title = "Cash Advance", Kind ="Cash"},
                            new DashboardMenu{ Title = "Leave", Kind ="Calendar"},
                            new DashboardMenu{ Title ="Payroll",Kind="Cash" },
                            new DashboardMenu{ Title = "Payslip",Kind = "Cash"},
                            new DashboardMenu{ Title = "13th Month Pay",Kind = "Account"},
                            new DashboardMenu{ Title = "User Accounts",Kind = "Account"},
                            new DashboardMenu{ Title = "Settings",Kind = "Settings"},

                        };
                    break;

                case "Accounting":
                    dashboardMenus = new List<DashboardMenu>(){
                            new DashboardMenu{ Title ="Home" ,Kind="ViewDashboard"},
                            new DashboardMenu{ Title ="Attendance" ,Kind="ContentPaste"},
                            new DashboardMenu{ Title = "Overtime",Kind = "Account"},
                            new DashboardMenu{ Title = "Cash Advance", Kind ="Cash"},
                            new DashboardMenu{ Title = "Leave", Kind ="Calendar"},
                            new DashboardMenu{ Title ="Payroll",Kind="Cash" },
                            new DashboardMenu{ Title = "Payslip",Kind = "Cash"},
                            new DashboardMenu{ Title = "13th Month Pay",Kind = "Account"}
                        };
                    break;

                case "Employee":
                case "Manager":
                    dashboardMenus = new List<DashboardMenu>(){
                            new DashboardMenu{ Title ="Employees",Kind="Account" },
                            new DashboardMenu{ Title ="Attendance" ,Kind="ContentPaste"},
                            new DashboardMenu{ Title = "Overtime",Kind = "Account"},
                            new DashboardMenu{ Title = "Loans", Kind ="Cash"},
                            new DashboardMenu{ Title = "Cash Advance", Kind ="Cash"},
                            new DashboardMenu{ Title = "Leave", Kind ="Calendar"},
                            new DashboardMenu{ Title = "Payslip", Kind ="Cash"},
                            new DashboardMenu{ Title = "13th Month Pay",Kind = "Account"}
                        };
                    break;

                case "AccountsPayable":
                    dashboardMenus = new List<DashboardMenu>(){
                        new DashboardMenu{ Title ="Cash Advance",Kind="Cash"}
                        };
                    break;

            }
            return dashboardMenus;
        }

    }
}
