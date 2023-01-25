using DevExpress.Mvvm;
using PayrollSystem.Database;
using PayrollSystem.Misc;
using SpreadsheetLight;
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
    class PayslipViewModel : Abstracts.RaisePropertyChanged
    {

        private ObservableCollection<string> _StatusCollection;

        public ObservableCollection<string> StatusCollection
        {
            get { return _StatusCollection; }
            set { _StatusCollection = value;
                OnPropertyChanged();
            }
        }

        private string _SelectedStatus;

        public string SelectedStatus
        {
            get { return _SelectedStatus; }
            set { _SelectedStatus = value;
                OnPropertyChanged();
            }
        }

        private string _SearchEmployeeText;

        public string SearchEmployeeText
        {
            get { return _SearchEmployeeText; }
            set { _SearchEmployeeText = value;
                OnPropertyChanged();
                SearchEmployee(SearchEmployeeText);
            }
        }




        #region Properties
        public object PayrollId { get; private set; }
        public object EmployeeNo { get; private set; }

        private ObservableCollection<Models.Payroll> _PayrollList;

        public ObservableCollection<Models.Payroll> PayrollList
        {
            get { return _PayrollList; }
            set
            {
                _PayrollList = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Models.CutOff> _CutOffList;

        public ObservableCollection<Models.CutOff> CutOffList
        {
            get { return _CutOffList; }
            set
            {
                _CutOffList = value;
                OnPropertyChanged();
            }
        }

        private Models.CutOff _SelectedCutOff;

        public Models.CutOff SelectedCutOff
        {
            get { return _SelectedCutOff; }
            set
            {
                _SelectedCutOff = value;
                OnPropertyChanged();
                if (SelectedCutOff == null)
                {
                    return;
                }
                GeneratePayslip(SelectedCutOff.Id);
            }
        }

        private int _SelectedCutOffIndex;

        public int SelectedCutOffIndex
        {
            get { return _SelectedCutOffIndex; }
            set
            {
                _SelectedCutOffIndex = value;
                OnPropertyChanged();
            }
        }
        #endregion
        
        #region Commands

        public AsyncCommand<string> PrintPayslipCommand { get; set; }
        #endregion

        #region Constructor
        public PayslipViewModel()
        {
            PrintPayslipCommand = new AsyncCommand<string>(PrintPayslipEvent);
            CutOffList = new ObservableCollection<Models.CutOff>();
            StatusCollection = new ObservableCollection<string>() { 
                "Monthly",
                "Daily",
                "Managerial"
            };
            GenerateCutOffs();
            //SelectedCutOffIndex = 1;
        }
        #endregion

        #region Methods
        private void SearchEmployee(string searchText)
        {
            GeneratePayslip(SelectedCutOff.Id);

            var result = PayrollList.Where(x =>
                   x.EmployeeNo.Contains(searchText)
                   || x.FullName.Contains(searchText)
                   );
           PayrollList = new ObservableCollection<Models.Payroll>(result);

        }
        private void GeneratePayslip(int id)
        {
            PayrollList = new ObservableCollection<Models.Payroll>();

            using (var context = new PayrollSystem.Database.PayrollDbContext())
            {
                switch (UserAccount.UserType)
                {
                    case "Administrator":
                    case "HR":
                    case "Accounting":
                        var list = context.Payrolls.Where(x => x.PayrollId == id);
                     
                        foreach (var item in list)
                        {
                            if (item.EmployeeStatus != "Daily")
                            {
                                item.WorkDaysAmount = item.BasicPay;
                            }
                            PayrollList.Add(item);
                        }
                        break;
                    case "Employee":
                    case "Manager":
                        var lists = context.Payrolls.Where(x => x.EmployeeNo == UserAccount.UserId && x.PayrollId == id);

                        foreach (var item in lists)
                        {
                            item.WorkDaysAmount = item.BasicPay;
                            PayrollList.Add(item);
                        }
                        break;
                }


            }

        }
        private void GenerateCutOffs()
        {
            using (var context = new PayrollDbContext())
            {
                foreach (var item in context.CutOffs.OrderByDescending(x=>x.Id))
                {
                    CutOffList.Add(item);
                }
            }

        }
        private async Task PrintPayslipEvent(string employeeNo)
        {
            await Task.Run(() => {
                #region Generate Payslip
                if (SelectedCutOff == null)
                {
                    return;
                }

                var cashAdvanceValue = GetCashAdvance(employeeNo);

                var path = Setting.ReportPath + "Payslip";

                var payroll = GetPayroll(employeeNo, SelectedCutOff.Id);
                var savingPath = $"{path}\\{payroll.FullName}.xlsx";

                var endDateValue = "";
                if (SelectedCutOff.EndDate.Day == 10)
                {
                    endDateValue = $"{SelectedCutOff.EndDate.Month}-15-{SelectedCutOff.EndDate.Year}";
                }
                else
                {
                    endDateValue = $"{SelectedCutOff.EndDate.Month}-30-{SelectedCutOff.EndDate.Year}";
                }


                using (SLDocument sLDocument = new SLDocument($"{path}\\payslip.xlsx", "Sheet1"))
                {

                    if (payroll.EmployeeStatus == "Daily")
                    {
                        sLDocument.SetCellValue("E10", payroll.WorkDays);
                    }

                    sLDocument.SetCellValue("C6", payroll.FullName);
                    sLDocument.SetCellValue("C10", payroll.WorkDaysAmount);
                    sLDocument.SetCellValue("C7", SelectedCutOff.DisplayDate);
                    sLDocument.SetCellValue("H6", endDateValue);
                    sLDocument.SetCellValue("C11", payroll.WorkOvertimeAmount);
                    sLDocument.SetCellValue("E11", payroll.OverTimeMinutes);
                    sLDocument.SetCellValue("C12", payroll.RegularHolidayAmount);
                    sLDocument.SetCellValue("E12", payroll.RegularHolidayDays);
                    sLDocument.SetCellValue("C13", payroll.TotalRegularHolidayAmount);
                    sLDocument.SetCellValue("E13", payroll.OverTimeRegularHolidayMinutes);
                    sLDocument.SetCellValue("C14", payroll.SpecialHolidayAmount);
                    sLDocument.SetCellValue("E14", payroll.SpecialHolidayDays);
                    sLDocument.SetCellValue("C15", payroll.OtSpecialHolidayAmount);
                    sLDocument.SetCellValue("E15", payroll.OTSpecialHolidayMinutes);
                    sLDocument.SetCellValue("C16", payroll.WeekendDutyAmount);
                    sLDocument.SetCellValue("E16", payroll.WeekendMinutes);
                    sLDocument.SetCellValue("C17", payroll.Allowance);
                   
                    var status = GetStatus(payroll.EmployeeNo);
                    if (status == "Managerial" || status == "Monthly")
                    {
                        sLDocument.SetCellValue("H10", payroll.AbsentAmount);
                    }
                    else
                    {
                       
                    }

                    sLDocument.SetCellValue("J10", payroll.TotalMinutesOfAbsent);
                    sLDocument.SetCellValue("H11", payroll.MinutesAmount);
                    sLDocument.SetCellValue("J11", payroll.TotalMinutesOfLate);
                    sLDocument.SetCellValue("H12", payroll.UndertimeAmount);
                    sLDocument.SetCellValue("J12", payroll.TotalMinutesOfUndertime);
                    sLDocument.SetCellValue("H13", payroll.Vale);
                    sLDocument.SetCellValue("H14", payroll.PhilhealthContribution);
                    sLDocument.SetCellValue("H15", payroll.PagIbigContribution);
                    sLDocument.SetCellValue("H16", payroll.PagibigSalaryLoan);
                    sLDocument.SetCellValue("H17", payroll.PagibigCalamityLoan);
                    sLDocument.SetCellValue("H18", payroll.SssContribution);
                    sLDocument.SetCellValue("H19", payroll.SssProvidentFund);
                    sLDocument.SetCellValue("H20", payroll.SssCalamityLoan);
                    sLDocument.SetCellValue("H21", payroll.SssSalaryLoan);
                    sLDocument.SetCellValue("H22", payroll.Others);
                    sLDocument.SetCellValue("C18", payroll.NightTimeAmount);
                    sLDocument.SetCellValue("C19", payroll.Adjustments);

                    var status1 = GetStatus(payroll.EmployeeNo);

                    if (status1 == "Daily")
                    {
                       
                        var deduction = 
                           // payroll.AbsentAmount +  
                            payroll.UndertimeAmount + 
                            payroll.MinutesAmount + 
                            payroll.Vale + 
                            payroll.PhilhealthContribution +
                            payroll.PagIbigContribution + payroll.PagibigCalamityLoan +
                            payroll.PagibigSalaryLoan + payroll.SssContribution + 
                            payroll.SssCalamityLoan + payroll.SssProvidentFund + 
                            payroll.SssSalaryLoan +
                            //payroll.Adjustments
                            payroll.Others;

                        var GrossIncome1 = payroll.WorkDaysAmount + payroll.Allowance + payroll.WorkOvertimeAmount
                         + payroll.RegularHolidayAmount + payroll.TotalRegularHolidayAmount + payroll.OtSpecialHolidayAmount
                         + payroll.SpecialHolidayAmount + payroll.WeekendDutyAmount + payroll.NightTimeAmount + payroll.Adjustments;

                        var totalSalary = GrossIncome1 - deduction;

                        sLDocument.SetCellValue("H25", deduction);
                        sLDocument.SetCellValue("C25", GrossIncome1);
                        sLDocument.SetCellValue("C27", totalSalary);
                    }
                    else
                    {
                        //pang monthly to
                       var GrossIncome1 = payroll.BasicPay + payroll.Allowance + payroll.WorkOvertimeAmount
                           + payroll.RegularHolidayAmount + payroll.TotalRegularHolidayAmount + payroll.OtSpecialHolidayAmount
                           + payroll.SpecialHolidayAmount + payroll.WeekendDutyAmount + payroll.NightTimeAmount + payroll.Adjustments;

                        var deduction = payroll.AbsentAmount + payroll.MinutesAmount + payroll.UndertimeAmount + payroll.Vale + payroll.PhilhealthContribution
                      + payroll.PagIbigContribution + payroll.PagibigCalamityLoan
                      + payroll.PagibigSalaryLoan + payroll.SssContribution
                      + payroll.SssCalamityLoan + payroll.SssProvidentFund + payroll.SssSalaryLoan
                      /*+ payroll.Adjustments*/ + payroll.Others;



                        var totalSalary = GrossIncome1 - deduction;

                        sLDocument.SetCellValue("C25", GrossIncome1);
                        sLDocument.SetCellValue("H25", deduction);
                        sLDocument.SetCellValue("C27", totalSalary);
                    }


                    if (cashAdvanceValue != null)
                    {
                        sLDocument.SetCellValue("H27", cashAdvanceValue.RemainingBalance);
                    }
                    sLDocument.SaveAs(savingPath);
                }

                //Save as pdf file

                savingPath.GeneratePdf(payroll.EmployeeNo);
                #endregion
            });

            //Process.Start(savingPath);
        }
        private string GetStatus(string employeeNo)
        {
            using (var context = new PayrollDbContext())
            {
                var result = context.Employees.Where(x => x.EmployeeNo == employeeNo).FirstOrDefault();
                return result.Status;
            }
        }     
        private Models.CashAdvanceModel GetCashAdvance(string employeeNo)
        {
            using (var context = new PayrollDbContext())
            {
                var result = context.CashAdvances.Where(x => x.EmployeeNo == employeeNo).FirstOrDefault();
                return result;
            }
        }
        private Models.Payroll GetPayroll(string employeeNo, int payrollId)
        {
            using (var context = new PayrollDbContext())
            {
                var result = context.Payrolls.Where(x => x.PayrollId == payrollId && x.EmployeeNo.Equals(employeeNo)).FirstOrDefault();
                var status = GetStatus(employeeNo);

                result.WorkDaysAmount = status == "Monthly" ? result.BasicPay: result.WorkDaysAmount;
                return result ?? null;
            }
        }

        #endregion
    }

}
