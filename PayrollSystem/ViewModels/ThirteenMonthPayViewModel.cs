using DevExpress.Mvvm;
using DocumentFormat.OpenXml.Vml;
using PayrollSystem.Database;
using PayrollSystem.Misc;
using PayrollSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PayrollSystem.ViewModels
{
    class ThirteenMonthPayViewModel : Abstracts.RaisePropertyChanged
    {
        #region Properties

        private string _MonthCount;

        public string MonthCount
        {
            get { return _MonthCount; }
            set
            {
                _MonthCount = value;
                OnPropertyChanged();
            }
        }



        private ObservableCollection<Models.CutOff> _CutOffsCollection;

        public ObservableCollection<Models.CutOff> CutOffsCollection
        {
            get { return _CutOffsCollection; }
            set
            {
                _CutOffsCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Models.ThirteenMonthModel> _ThirteenMonthModelCollection;

        public ObservableCollection<Models.ThirteenMonthModel> ThirteenMonthModelCollection
        {
            get { return _ThirteenMonthModelCollection; }
            set
            {
                _ThirteenMonthModelCollection = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Models.Breakdown> _Breakdowns;

        public ObservableCollection<Models.Breakdown> Breakdowns
        {
            get { return _Breakdowns; }
            set
            {
                _Breakdowns = value;
                OnPropertyChanged();
            }
        }

        private decimal _GrandTotal;

        public decimal GrandTotal
        {
            get { return _GrandTotal; }
            set
            {
                _GrandTotal = value;
                OnPropertyChanged();
            }
        }

        private Visibility _BreakdownVisibility;

        public Visibility BreakdownVisibility
        {
            get { return _BreakdownVisibility; }
            set
            {
                _BreakdownVisibility = value;
                OnPropertyChanged();
            }
        }



        private Models.ThirteenMonthModel _SelectedThirteenMonth;

        public Models.ThirteenMonthModel SelectedThirteenMonth
        {
            get { return _SelectedThirteenMonth; }
            set
            {
                _SelectedThirteenMonth = value;
                OnPropertyChanged();
                if (SelectedThirteenMonth != null)
                {
                    Breakdowns = new ObservableCollection<Models.Breakdown>();
                    BreakdownVisibility = Visibility.Visible;

                    var payrolls = GetPayrolls(SelectedThirteenMonth.EmployeeNo).OrderBy(x => x.PayrollId).ToList();
                    var getpayrollPerMonth = GetPerMonth(payrolls);
                    var breakdowns = new List<ModelThriteenMonth>();

                    using (var payrollDbContext = new PayrollDbContext())
                    {
                        foreach (var payroll in payrolls)
                        {
                            var cutOff = payrollDbContext.CutOffs.Where(x => x.Id == payroll.PayrollId).FirstOrDefault();
                            if (cutOff != null)
                            {
                                breakdowns.Add(new ModelThriteenMonth
                                {
                                    PayrollId = cutOff.Id,
                                    StartDate = cutOff.StartDate,
                                    EndDate = cutOff.EndDate,
                                    DisplayStartDate = cutOff.DisplayStartDate,
                                    DisplayEndDate = cutOff.DisplayEndDate,
                                    RegularHolidayAmount = payroll.RegularHolidayAmount,
                                    SpecialHolidayAmount = payroll.SpecialHolidayAmount,
                                    WorkOvertimeAmount = payroll.WorkOvertimeAmount,
                                    OtSpecialHolidayAmount = payroll.OtSpecialHolidayAmount,
                                    WeekendDutyAmount = payroll.WeekendDutyAmount,
                                    GrossIncome = payroll.GrossIncome,
                                    TotalRegularHolidayAmount = payroll.TotalRegularHolidayAmount

                                });
                            }
                        }





                        int j = 0;
                        var isStartOdd = breakdowns[0];
                        if (isStartOdd.StartDate.Day == 11)
                        {
                            j = 1;
                            var totalDeduction =
                             TotalDeductions(isStartOdd.WorkOvertimeAmount, isStartOdd.TotalRegularHolidayAmount, isStartOdd.RegularHolidayAmount, isStartOdd.SpecialHolidayAmount,
                             isStartOdd.OtSpecialHolidayAmount, isStartOdd.WeekendDutyAmount);


                            decimal totalGross = isStartOdd.GrossIncome - totalDeduction;

                            var breakdown = new Breakdown
                            {
                                CutoffValue1 = 0,
                                CutoffValue2 = totalGross,
                                Cutoff2 = $"{isStartOdd.DisplayStartDate} - {isStartOdd.DisplayEndDate} - [{totalGross}]",
                                Month = $"{GetMonth(isStartOdd.StartDate.Month)}"
                            };
                            Breakdowns.Add(breakdown);
                        }

                        var breakdownCount = breakdowns.Count();

                        for (int i = j; i <= breakdownCount - 1; i += 2)
                        {
                            var index = i + 1;
                            var nextIndex = index >= breakdownCount ? breakdownCount - 1 : index;
                            Console.WriteLine($"Index:{i}, Next Index:{index}");


                            var item = breakdowns.ElementAtOrDefault(i);
                            var nextItem = breakdowns.ElementAtOrDefault(nextIndex);

                            var totalDeduction = TotalDeductions(
                                  item.WorkOvertimeAmount,
                                  item.TotalRegularHolidayAmount,
                                  item.RegularHolidayAmount,
                                  item.SpecialHolidayAmount,
                                  item.OtSpecialHolidayAmount,
                                  item.WeekendDutyAmount);

                            var totalDeduction2 =
                                TotalDeductions(
                                nextItem.WorkOvertimeAmount,
                                nextItem.TotalRegularHolidayAmount,
                                nextItem.RegularHolidayAmount,
                                nextItem.SpecialHolidayAmount,
                                nextItem.OtSpecialHolidayAmount,
                                nextItem.WeekendDutyAmount);

                            decimal totalGross = item.GrossIncome - totalDeduction;
                            decimal totalGross2 = nextItem.GrossIncome - totalDeduction2;

                            if (item.DisplayStartDate != nextItem.DisplayStartDate)
                            {
                                if (nextItem.StartDate.Day == 11)
                                {
                                    var breakdown = new Breakdown
                                    {
                                        CutoffValue1 = totalGross,
                                        CutoffValue2 = totalGross2,
                                        Cutoff1 = $"{item.DisplayStartDate} - {item.DisplayEndDate} - [{totalGross}]",
                                        Cutoff2 = $"{nextItem.DisplayStartDate} - {nextItem.DisplayEndDate} - [{totalGross2}]",
                                        Month = $"{GetMonth(item.StartDate.Month)} - {GetMonth(item.EndDate.Month)}"
                                    };
                                    Breakdowns.Add(breakdown);
                                }

                                else if (item.StartDate.Day == 26 || nextItem.EndDate.Day == 1)
                                {
                                    var breakdown = new Breakdown
                                    {
                                        CutoffValue1 = totalGross,
                                        CutoffValue2 = totalGross2,
                                        Cutoff1 = $"{item.DisplayStartDate} - {item.DisplayEndDate} - [{totalGross}]",
                                        Cutoff2 = $"{nextItem.DisplayStartDate} - {nextItem.DisplayEndDate} - [{totalGross2}]",
                                        Month = $"{GetMonth(item.StartDate.Month)} - {GetMonth(nextItem.EndDate.Month)}"

                                    };
                                    Breakdowns.Add(breakdown);
                                }
                                else
                                {
                                    var breakdown = new Breakdown
                                    {
                                        CutoffValue1 = totalGross2,
                                        CutoffValue2 = 0,
                                        Cutoff1 = $"{nextItem.DisplayStartDate} - {nextItem.DisplayEndDate} - [{totalGross2}]",
                                        Month = $"{GetMonth(nextItem.StartDate.Month)} - {GetMonth(nextItem.EndDate.Month)}"
                                    };
                                    Breakdowns.Add(breakdown);
                                }
                            }
                            else
                            {
                                var breakdown = new Breakdown
                                {
                                    CutoffValue1 = totalGross,
                                    CutoffValue2 = 0,
                                    Cutoff1 = $"{item.DisplayStartDate} - {item.DisplayEndDate} - [{totalGross}]",
                                    Month = $"{GetMonth(item.StartDate.Month)} - {GetMonth(item.EndDate.Month)}"
                                };
                                Breakdowns.Add(breakdown);
                            }


                        }
                        GrandTotal = Breakdowns.Sum(x => x.Total) * Breakdowns.Count() / 12;
                    }
                }

            }
        }



        #endregion
        private decimal TotalDeductions(params decimal[] decimals)
        {
            return decimals.Sum();
        }

        #region Get Month
        private string GetMonth(int month)
        {
            switch (month)
            {
                case 1:
                    return "Jan";
                case 2:
                    return "Feb";
                case 3:
                    return "Mar";
                case 4:
                    return "Apr";
                case 5:
                    return "May";
                case 6:
                    return "Jun";
                case 7:
                    return "Jul";
                case 8:
                    return "Aug";
                case 9:
                    return "Sept";
                case 10:
                    return "Oct";
                case 11:
                    return "Nov";
                case 12:
                    return "Dec";
            }
            return null;
        }
        #endregion

        #region Commands
        public DelegateCommand CloseBreakdownCommand { get; set; }
        public DelegateCommand PrintThirteenMonth { get; set; }
        public object EmployeeNo { get; private set; }
        #endregion

        public ThirteenMonthPayViewModel()
        {
            CutOffsCollection = new ObservableCollection<Models.CutOff>(GetCutOffs());
            ThirteenMonthModelCollection = new ObservableCollection<Models.ThirteenMonthModel>();
            Breakdowns = new ObservableCollection<Models.Breakdown>();

            using (var context = new Database.PayrollDbContext())
            {
                var employees = new List<Models.Employee>();

                if (UserAccount.UserType != "Employee" && UserAccount.UserType != "Manager")
                {
                    employees = context.Employees.Where(x => x.Status != "Resign" && x.Status != "NoPayroll").ToList();
                }
                else
                {
                    employees = context.Employees.Where(x => x.EmployeeNo == UserAccount.UserId).ToList();
                }

                foreach (var item in employees)
                {


                    var payrolls = GetPayrolls(item.EmployeeNo);
                    decimal totalGrossIncome = 0;

                    foreach (var x in payrolls)
                    {
                        var total = x.WorkOvertimeAmount + x.RegularHolidayAmount
                            + x.TotalRegularHolidayAmount + x.SpecialHolidayAmount + x.OtSpecialHolidayAmount +
                            x.WeekendDutyAmount;

                        decimal totalGross = x.GrossIncome - total;
                        totalGrossIncome += totalGross;
                    }

                    decimal numberOfMonths = 0M;

                    if (payrolls.Count() >= 1)
                    {
                        numberOfMonths = (decimal)(payrolls.Count() / 2.0);
                    }

                    var totalResult = (totalGrossIncome / 12);
                    decimal thirteenMonthValue = Decimal.Parse(totalResult.ToString("0.00"));


                    var model = new Models.ThirteenMonthModel()
                    {
                        EmployeeNo = item.EmployeeNo,
                        EmployeeName = item.Fullname,
                        Position = item.Position,
                        TotalGross = thirteenMonthValue,
                        NumberOfMonths = numberOfMonths
                    };

                    ThirteenMonthModelCollection.Add(model);
                }






            }
            BreakdownVisibility = Visibility.Hidden;
            float cutoffMonths = 0f;

            if (CutOffsCollection.Count() >= 1)
            {
                cutoffMonths = (float)((float)CutOffsCollection.Count() / 2.0);
            }
            MonthCount = $"Month Count:{cutoffMonths}";



            CloseBreakdownCommand = new DelegateCommand(CloseBreakdownEvent);
            PrintThirteenMonth = new DelegateCommand(PrintThirteenMontEvent);
        }


        private void CloseBreakdownEvent()
        {
            Breakdowns = new ObservableCollection<Models.Breakdown>();
            BreakdownVisibility = Visibility.Hidden;
            GrandTotal = 0;
        }

        private List<List<Models.Payroll>> GetPerMonth(List<Models.Payroll> payrolls)
        {
            var list = new List<List<Models.Payroll>>();

            for (int j = 0; j <= payrolls.Count() - 1; j++)
            {
                var lastCount = payrolls.Count() - 1;

                var item = payrolls[j];
                var nextItem = payrolls[j + (j >= lastCount ? 0 : 1)];

                if (j == lastCount)
                {
                    if (payrolls.Count() % 2 != 0)
                    {
                        list.Add(new List<Models.Payroll> {
                        item
                    });

                    }

                }
                else if (j % 2 == 0)
                {

                    list.Add(new List<Models.Payroll> {
                        item,
                        nextItem
                    });
                }
            }
            return list;
        }

        private List<Models.CutOff> GetCutOffs()
        {
            var yearToday = DateTime.UtcNow.Year;
            var lastYear = yearToday - 1;
            var novemberMonth = 11;
            var decemberMonth = 12;



            var isLastYearCutOff = DateTime.TryParse($"01/{decemberMonth}/{lastYear}", out DateTime lastYearCutOff);
            var isCurrentYearCutOff = DateTime.TryParse($"30/{novemberMonth}/{yearToday}", out DateTime CurrentYearCutOff);
            if (isLastYearCutOff && isCurrentYearCutOff)
            {
                using (var context = new Database.PayrollDbContext())
                {
                    var cutoffs = context.CutOffs.Where(x => (x.StartDate.Year == lastYear) || (x.EndDate.Year == yearToday)).ToList();
                    return cutoffs;

                }
            }
            return new List<Models.CutOff>();
        }

        private List<Models.Payroll> GetPayrolls(string employeeNo)
        {
            var list = new List<Models.Payroll>();

            using (var context = new Database.PayrollDbContext())
            {

                var payrolls = context.Payrolls.Where(x => x.EmployeeNo == employeeNo);
                foreach (var item in CutOffsCollection)
                {
                    var isPayrollExists = payrolls.Where(x => x.PayrollId == item.Id).FirstOrDefault();
                    if (isPayrollExists != null)
                    {
                        list.Add(isPayrollExists);
                    }
                }
            }

            return list;
        }


        private void PrintThirteenMontEvent()
        {
            if (SelectedThirteenMonth != null)
            {
                var breakdowns = Breakdowns.ToList();
                PrintRecord(SelectedThirteenMonth.EmployeeName, SelectedThirteenMonth.EmployeeNo, breakdowns);
            }
        }
        private int RowStart(Models.Breakdown breakdown)
        {
            var month = breakdown.Month;
            if (month.Contains("-"))
            {

                var startMonth = month.Split('-').FirstOrDefault().Trim();
                var endMonth = month.Split('-').LastOrDefault().Trim();
                if (breakdown.Cutoff1.Contains("-"))
                {
                    var cutOff1 = breakdown.Cutoff1.Split('-');
                    var cutOffDateStart1 = DateTime.Parse(cutOff1[0].Trim());
                    var cutoffDateEnd1 = DateTime.Parse(cutOff1[1].Trim());
                    if (cutOffDateStart1.Day == 26)
                    {
                        startMonth = endMonth;
                    }
                }

                switch (startMonth)
                {
                    case "Dec": return 6;
                    case "Jan": return 7;
                    case "Feb": return 8;
                    case "Mar": return 9;
                    case "Apr": return 10;
                    case "May": return 11;
                    case "Jun": return 12;
                    case "Jul": return 13;
                    case "Aug": return 14;
                    case "Sep": return 15;
                    case "Oct": return 16;
                    case "Nov": return 17;
                    default:
                        return 6;
                }
            }
            else
            {
                switch (month.Trim())
                {
                    case "Dec": return 6;
                    case "Jan": return 7;
                    case "Feb": return 8;
                    case "Mar": return 9;
                    case "Apr": return 10;
                    case "May": return 11;
                    case "Jun": return 12;
                    case "Jul": return 13;
                    case "Aug": return 14;
                    case "Sep": return 15;
                    case "Oct": return 16;
                    case "Nov": return 17;
                    default:
                        return 6;
                }
            }
        }

        private void PrintRecord(string employeeName, string employeeNo, List<Breakdown> breakdowns)
        {
            var path = Setting.ReportPath;
            var savingPath = $"{path}13thmonth\\13thmonth-{employeeName}.xlsx";
            var templatePath = $"{path}13thmonth\\13thmonth.xlsx";

            if (!System.IO.File.Exists(templatePath))
            {
                MessageBox.Show("You can't generate report!");
                return;

            }



            using (var slDocument = new SpreadsheetLight.SLDocument(templatePath, "Sheet1"))
            {
                slDocument.SetCellValue($"C4", employeeName);
                int startRow = RowStart(breakdowns.FirstOrDefault());
                decimal totalValue = 0;

                foreach (var item in breakdowns)
                {
                    var total = item.CutoffValue1 + item.CutoffValue2;
                    totalValue += total;

                    if (item.Month.Contains("Dec") && item.Month.Contains("Nov"))
                    {
                        slDocument.SetCellValue($"C6", total);
                    }
                    else if (item.Month.Contains("Dec") && item.Month.Contains("Jan"))
                    {
                        slDocument.SetCellValue($"C7", total);
                    }
                    else
                    {
                        slDocument.SetCellValue($"C{startRow}", total);
                    }
                    startRow++;
                }
                var grandTotal = totalValue / 12;

                slDocument.SetCellValue($"C2", DateTime.Now.Year);
                slDocument.SetCellValue($"C19", grandTotal);


                slDocument.SaveAs(savingPath);

                //  savingPath.GeneratePdf();

                savingPath.GeneratePdf(employeeNo);


                //  Process.Start(savingPath);
            }
        }
    }

    public class ModelThriteenMonth
    {
        public int PayrollId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal RegularHolidayAmount { get; set; }
        public decimal SpecialHolidayAmount { get; set; }
        public decimal WorkOvertimeAmount { get; set; }
        public decimal OtSpecialHolidayAmount { get; set; }
        public decimal WeekendDutyAmount { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal TotalRegularHolidayAmount { get; set; }
        public string DisplayStartDate { get; set; }
        public string DisplayEndDate { get; set; }
    }
}
