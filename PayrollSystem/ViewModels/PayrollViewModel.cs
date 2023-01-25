using DevExpress.Mvvm;
using MySql.Data.MySqlClient;
using PayrollSystem.Database;
using PayrollSystem.Misc;
using PayrollSystem.Models;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PayrollSystem.ViewModels
{
    class PayrollViewModel : Abstracts.RaisePropertyChanged
    {
        #region Column Properties
        private bool _FiftheenDisplay;

        public bool FiftheenDisplay
        {
            get { return _FiftheenDisplay; }
            set { _FiftheenDisplay = value;
                OnPropertyChanged();
            }
        }

        private bool _ThirtyDisplay;

        public bool ThirtyDisplay
        {
            get { return _ThirtyDisplay; }
            set { _ThirtyDisplay = value;
                OnPropertyChanged();
            }
        }


        #region Payroll Properties
        private bool _IsSalaryEnabled;

        public bool IsSalaryEnabled
        {
            get { return _IsSalaryEnabled; }
            set
            {
                _IsSalaryEnabled = value;
                OnPropertyChanged();
            }
        }


        private bool _IsBasicPayEnabled;

        public bool IsBasicPayEnabled
        {
            get { return _IsBasicPayEnabled; }
            set
            {
                _IsBasicPayEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _IsPayrollPrintEnabled;

        public bool IsPayrollPrintEnabled
        {
            get { return _IsPayrollPrintEnabled; }
            set
            {
                _IsPayrollPrintEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _IsWorkDaysAmountEnabled;

        public bool IsWorkDaysAmountEnabled
        {
            get { return _IsWorkDaysAmountEnabled; }
            set
            {
                _IsWorkDaysAmountEnabled = value;
                OnPropertyChanged();
            }
        }


        private bool _IsOvertimeEnabled;

        public bool IsOvertimeEnabled
        {
            get { return _IsOvertimeEnabled; }
            set
            {
                _IsOvertimeEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _IsNightTimeEnabled;

        public bool IsNightTimeEnabled
        {
            get { return _IsNightTimeEnabled; }
            set
            {
                _IsNightTimeEnabled = value;
                OnPropertyChanged();
            }
        }



        private bool _IsRegularHolidayAmountEnabled;

        public bool IsRegularHolidayAmountEnabled
        {
            get { return _IsRegularHolidayAmountEnabled; }
            set
            {
                _IsRegularHolidayAmountEnabled = value;
                OnPropertyChanged();
            }
        }


        private bool _IsDailyRateEnabled;

        public bool IsDailyRateEnabled
        {
            get { return _IsDailyRateEnabled; }
            set
            {
                _IsDailyRateEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _IsRegularHolidayOtMinsAmountEnabled;

        public bool IsRegularHolidayOtMinsAmountEnabled
        {
            get { return _IsRegularHolidayOtMinsAmountEnabled; }
            set
            {
                _IsRegularHolidayOtMinsAmountEnabled = value;
                OnPropertyChanged();
            }
        }
        private bool _IsSpecialHolidayAmountEnabled;

        public bool IsSpecialHolidayAmountEnabled
        {
            get { return _IsSpecialHolidayAmountEnabled; }
            set
            {
                _IsSpecialHolidayAmountEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _IsSpecialHolidayOtMinsAmountEnabled;

        public bool IsSpecialHolidayOtMinsAmountEnabled
        {
            get { return _IsSpecialHolidayOtMinsAmountEnabled; }
            set
            {
                _IsSpecialHolidayOtMinsAmountEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _IsSaturdaySundayDutyAmountEnabled;

        public bool IsSaturdaySundayDutyAmountEnabled
        {
            get { return _IsSaturdaySundayDutyAmountEnabled; }
            set
            {
                _IsSaturdaySundayDutyAmountEnabled = value;
                OnPropertyChanged();
            }
        }
        private bool _IsTranspoAllowanceEnabled;

        public bool IsTranspoAllowanceEnabled
        {
            get { return _IsTranspoAllowanceEnabled; }
            set
            {
                _IsTranspoAllowanceEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _IsAllowanceEnabled;

        public bool IsAllowanceEnabled
        {
            get { return _IsAllowanceEnabled; }
            set
            {
                _IsAllowanceEnabled = value;
                OnPropertyChanged();
            }
        }


        #endregion


        #endregion
      
        #region Properties

        private ObservableCollection<Models.Payroll> _Payrolls;

        public ObservableCollection<Models.Payroll> Payrolls
        {
            get { return _Payrolls; }
            set
            {
                _Payrolls = value;
                OnPropertyChanged();

            }
        }

        private string _Title;

        public string Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                OnPropertyChanged();
            }
        }

        private string _PayrollEmployeeNo;

        public string PayrollEmployeeNo
        {
            get { return _PayrollEmployeeNo; }
            set
            {
                _PayrollEmployeeNo = value;
                OnPropertyChanged();
            }
        }

        private string _PayrollFullName;

        public string PayrollFullName
        {
            get { return _PayrollFullName; }
            set
            {
                _PayrollFullName = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollSalary;

        public decimal PayrollSalary
        {
            get { return _PayrollSalary; }
            set
            {
                _PayrollSalary = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollBasicPay;

        public decimal PayrollBasicPay
        {
            get { return _PayrollBasicPay; }
            set
            {
                _PayrollBasicPay = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollDailyRate;

        public decimal PayrollDailyRate
        {
            get { return _PayrollDailyRate; }
            set
            {
                _PayrollDailyRate = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollPhilhealth;

        public decimal PayrollPhilhealth
        {
            get { return _PayrollPhilhealth; }
            set
            {
                _PayrollPhilhealth = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollPagibig;

        public decimal PayrollPagibig
        {
            get { return _PayrollPagibig; }
            set
            {
                _PayrollPagibig = value;
                OnPropertyChanged();
            }
        }
        private decimal _PayrollLate;

        public decimal PayrollLate
        {
            get { return _PayrollLate; }
            set
            {
                _PayrollLate = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollAbsent;

        public decimal PayrollAbsent
        {
            get { return _PayrollAbsent; }
            set
            {
                _PayrollAbsent = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollVale;

        public decimal PayrollVale
        {
            get { return _PayrollVale; }
            set
            {
                _PayrollVale = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollAllowance;

        public decimal PayrollAllowance
        {
            get { return _PayrollAllowance; }
            set
            {
                _PayrollAllowance = value;
                OnPropertyChanged();
            }
        }


        private decimal _PayrollOthers;

        public decimal PayrollOthers
        {
            get { return _PayrollOthers; }
            set
            {
                _PayrollOthers = value;
                OnPropertyChanged();
            }
        }
        private decimal _PayrollAdjustments;

        public decimal PayrollAdjustments
        {
            get { return _PayrollAdjustments; }
            set
            {
                _PayrollAdjustments = value;
                OnPropertyChanged();
            }
        }



        private decimal _PayrollSSS;

        public decimal PayrollSSS
        {
            get { return _PayrollSSS; }
            set
            {
                _PayrollSSS = value;
                OnPropertyChanged();
            }
        }
        private decimal _PayrollTakeHomePay;

        public decimal PayrollTakeHomePay
        {
            get { return _PayrollTakeHomePay; }
            set
            {
                _PayrollTakeHomePay = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollTranspoAllowance;

        public decimal PayrollTranspoAllowance
        {
            get { return _PayrollTranspoAllowance; }
            set
            {
                _PayrollTranspoAllowance = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollOT;

        public decimal PayrollOT
        {
            get { return _PayrollOT; }
            set
            {
                _PayrollOT = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollWorkDays;

        public decimal PayrollWorkDays
        {
            get { return _PayrollWorkDays; }
            set
            {
                _PayrollWorkDays = value;
                OnPropertyChanged();
            }
        }
        private decimal _PayrollWorkDaysAmount;

        public decimal PayrollWorkDaysAmount
        {
            get { return _PayrollWorkDaysAmount; }
            set
            {
                _PayrollWorkDaysAmount = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollSssProfund;

        public decimal PayrollSssProfund
        {
            get { return _PayrollSssProfund; }
            set
            {
                _PayrollSssProfund = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollSssCalamityLoan;

        public decimal PayrollSssCalamityLoan
        {
            get { return _PayrollSssCalamityLoan; }
            set
            {
                _PayrollSssCalamityLoan = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollSssSalaryLoan;

        public decimal PayrollSssSalaryLoan
        {
            get { return _PayrollSssSalaryLoan; }
            set
            {
                _PayrollSssSalaryLoan = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollPagIbigCalLoan;

        public decimal PayrollPagIbigCalLoan
        {
            get { return _PayrollPagIbigCalLoan; }
            set
            {
                _PayrollPagIbigCalLoan = value;
                OnPropertyChanged();
            }
        }
        private decimal _PayrollPagibigSalLoan;

        public decimal PayrollPagibigSalLoan
        {
            get { return _PayrollPagibigSalLoan; }
            set
            {
                _PayrollPagibigSalLoan = value;
                OnPropertyChanged();
            }
        }
        private decimal _PayrollUndertime;

        public decimal PayrollUndertime
        {
            get { return _PayrollUndertime; }
            set
            {
                _PayrollUndertime = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollRegHol;

        public decimal PayrollRegHol
        {
            get { return _PayrollRegHol; }
            set
            {
                _PayrollRegHol = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollSpeHol;

        public decimal PayrollSpeHol
        {
            get { return _PayrollSpeHol; }
            set
            {
                _PayrollSpeHol = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollRegHolOT;

        public decimal PayrollRegHolOT
        {
            get { return _PayrollRegHolOT; }
            set
            {
                _PayrollRegHolOT = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollSpeHolOT;

        public decimal PayrollSpeHolOT
        {
            get { return _PayrollSpeHolOT; }
            set
            {
                _PayrollSpeHolOT = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollWeekend;

        public decimal PayrollWeekend
        {
            get { return _PayrollWeekend; }
            set
            {
                _PayrollWeekend = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollGrossIncome;

        public decimal PayrollGrossIncome
        {
            get { return _PayrollGrossIncome; }
            set
            {
                _PayrollGrossIncome = value;
                OnPropertyChanged();
            }
        }

        private decimal _PayrollWorkdays;

        public decimal PayrollWorkdays
        {
            get { return _PayrollWorkdays; }
            set
            {
                _PayrollWorkdays = value;
                OnPropertyChanged();
            }
        }


        private decimal _PayrollNightTime;

        public decimal PayrollNightTime
        {
            get { return _PayrollNightTime; }
            set
            {
                _PayrollNightTime = value;
                OnPropertyChanged();
            }
        }


        private Models.Payroll _SelectedPayroll;

        public Models.Payroll SelectedPayroll
        {
            get { return _SelectedPayroll; }
            set
            {
                _SelectedPayroll = value;
                OnPropertyChanged();

                if (SelectedPayroll == null) return;

                PayrollEmployeeNo = SelectedPayroll.EmployeeNo;
                PayrollFullName = SelectedPayroll.FullName;
                PayrollSalary = SelectedPayroll.Salary;
                PayrollBasicPay = SelectedPayroll.BasicPay;
                PayrollDailyRate = SelectedPayroll.DailyRate;
                PayrollPhilhealth = SelectedPayroll.PhilhealthContribution;
                PayrollPagibig = SelectedPayroll.PagIbigContribution;
                PayrollOthers = SelectedPayroll.Others;
                PayrollAdjustments = SelectedPayroll.Adjustments;
                PayrollVale = SelectedPayroll.Vale;
                PayrollWorkDaysAmount = SelectedPayroll.WorkDaysAmount;
                PayrollLate = SelectedPayroll.TotalMinutesOfLate;
                PayrollAbsent = SelectedPayroll.TotalMinutesOfAbsent;
                PayrollRegHol = SelectedPayroll.RegularHolidayDays;
                PayrollSpeHol = SelectedPayroll.SpecialHolidayDays;
                PayrollOT = SelectedPayroll.OverTimeMinutes;
                PayrollUndertime = SelectedPayroll.TotalMinutesOfUndertime;
                PayrollAllowance = SelectedPayroll.Allowance;
                PayrollSSS = SelectedPayroll.SssContribution;
                PayrollTranspoAllowance = SelectedPayroll.TranspoAllowance;
                PayrollSssCalamityLoan = SelectedPayroll.SssCalamityLoan;
                PayrollSssProfund = SelectedPayroll.SssProvidentFund;
                PayrollSssSalaryLoan = SelectedPayroll.SssSalaryLoan;
                PayrollPagIbigCalLoan = SelectedPayroll.PagibigCalamityLoan;
                PayrollPagibigSalLoan = SelectedPayroll.PagibigSalaryLoan;
                PayrollWeekend = SelectedPayroll.WeekendMinutes;
                PayrollTakeHomePay = SelectedPayroll.TotalSalary;
                PayrollGrossIncome = SelectedPayroll.GrossIncome;
                PayrollSpeHolOT = SelectedPayroll.OTSpecialHolidayMinutes;
                PayrollRegHolOT = SelectedPayroll.OverTimeRegularHolidayMinutes;
                PayrollAbsent = SelectedPayroll.TotalMinutesOfAbsent;
                PayrollLate = SelectedPayroll.TotalMinutesOfLate;
                PayrollPagibig = SelectedPayroll.PagIbigContribution;
                PayrollNightTime = SelectedPayroll.NightTimeMinutes;

                PayrollWorkDays = SelectedPayroll.WorkDays;
                SelectedPayroll.IsEdit = true;
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

        private Models.CutOff _SelectedCutOff;

        public Models.CutOff SelectedCutOff
        {
            get { return _SelectedCutOff; }
            set
            {
                _SelectedCutOff = value;
                OnPropertyChanged();


            }
        }

        private ObservableCollection<Models.CutOff> _CutOffs;

        public ObservableCollection<Models.CutOff> CutOffs
        {
            get { return _CutOffs; }
            set
            {
                _CutOffs = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _StatusCollection;

        public ObservableCollection<string> StatusCollection
        {
            get { return _StatusCollection; }
            set
            {
                _StatusCollection = value;
                OnPropertyChanged();
            }
        }

        private string _SelectedStatus;

        public string SelectedStatus
        {
            get { return _SelectedStatus; }
            set
            {
                _SelectedStatus = value;
                OnPropertyChanged();
                //if (SelectedStatus != null)
                //{
                //    GeneratePayroll();
                //}
            }
        }

        #endregion

        #region Commands

        public AsyncCommand UpdatePayrollCommandAsync { get; set; }
        public AsyncCommand PrintPayrollAsyncCommand { get; set; }
        public DelegateCommand CancelSelectionCommand { get; set; }
        public AsyncCommand GeneratePayrollCommandAsync { get; set; }


        #endregion

        #region Constructor
        public PayrollViewModel()
        {
            var dayToday = DateTime.Now;
            FiftheenDisplay = true;
            ThirtyDisplay = true;


            GenerateCutOff();
            SelectedCutOffIndex = 1;

            Payrolls = new ObservableCollection<Models.Payroll>();

            UpdatePayrollCommandAsync = new AsyncCommand(UpdatePayrollEvent);
            PrintPayrollAsyncCommand = new AsyncCommand(PrintPayrollEvent);
            CancelSelectionCommand = new DelegateCommand(CancelSelectionEvent);
            GeneratePayrollCommandAsync = new AsyncCommand(GeneratePayrollEvent);
            StatusCollection = new ObservableCollection<string>()
            {
             "Monthly",
             "Daily",
             "Managerial"
            };
            if (Setting.PayrollSettings != "0")
            {
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.PayrollSettings>(Setting.PayrollSettings);
                IsSalaryEnabled = obj.IsSalaryEnabled;
                IsBasicPayEnabled = obj.IsBasicPayEnabled;
                IsWorkDaysAmountEnabled = obj.IsWorkDaysAmountEnabled;
                IsOvertimeEnabled = obj.IsOvertimeEnabled;
                IsRegularHolidayAmountEnabled = obj.IsRegularHolidayAmountEnabled;
                IsRegularHolidayOtMinsAmountEnabled = obj.IsRegularHolidayOtMinsAmountEnabled;
                IsSpecialHolidayAmountEnabled = obj.IsSpecialHolidayAmountEnabled;
                IsSpecialHolidayOtMinsAmountEnabled = obj.IsSpecialHolidayOtMinsAmountEnabled;
                IsSaturdaySundayDutyAmountEnabled = obj.IsSaturdaySundayDutyAmountEnabled;
                IsTranspoAllowanceEnabled = obj.IsTranspoAllowanceEnabled;
                IsAllowanceEnabled = obj.IsAllowanceEnabled;
                IsDailyRateEnabled = obj.IsDailyRateEnabled;
                IsNightTimeEnabled = obj.IsNightTimeEnabled;
                IsPayrollPrintEnabled = obj.IsPayrollPrintEnabled;
                
            }
        }

        #endregion

        #region Methods


        private async Task GeneratePayrollEvent()
        {
            if (SelectedStatus == null)
            {
                MessageBox.Show("Please select status first!");
                return;
            }


            var result = MessageBox.Show("Do you want to generate payroll?", "Generating Payroll", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                GeneratePayroll();
                if (SelectedCutOff.StartDate.Day == 26 && SelectedCutOff.EndDate.Day == 10)
                {
                    FiftheenDisplay = true;
                    ThirtyDisplay = false;
                }
                else
                {
                    FiftheenDisplay = false;
                    ThirtyDisplay = true;
                }
            }
            await Task.CompletedTask;
        }

        private void CancelSelectionEvent()
        {
            SelectedCutOff = null;
            SelectedStatus = null;
        }
        private async Task PrintPayrollEvent()
        {
            await Task.Run(() =>
            {
                var date = DateTime.Now.ToShortDateString().Replace('/', '-');

                var savingPath = $"{Setting.ReportPath}Payroll\\{SelectedStatus}-{date}.xlsx";
                var row = 5;
                var templatePath = $"{Setting.ReportPath}Payroll\\payroll.xlsx";

                using (SLDocument sLDocument = new SLDocument(templatePath, "Sheet1"))
                {
                    var status = SelectedStatus;


                    sLDocument.SetCellValue("B3", status);
                    sLDocument.SetCellValue("C1", SelectedCutOff.DisplayDate);
                    sLDocument.SetCellValue("D2", DateTime.Now.ToString("MMMM dd, yyyy"));

                    decimal totalPayroll = 0;

                    //1
                    decimal workDaysTotalAmount = 0;
                    decimal wordOTTotalAmount = 0;
                    decimal workRegHolAmount = 0;
                    decimal workRegHolOTAmount = 0;
                    decimal workSpeHolAmount = 0;
                    decimal workSpeHolOTAmount = 0;
                    decimal workWeekendAmount = 0;
                    decimal workAllowance = 0;
                    decimal workAbsent = 0;
                    decimal workTardiness = 0;
                    decimal workUndertime = 0;
                    decimal workVale = 0;
                    decimal workGrossIncome = 0;
                    decimal workPhilhealth = 0;
                    decimal workPagIbig = 0;
                    decimal workSSS = 0;
                    decimal workSSSMPF = 0;
                    decimal workSSSSL = 0;
                    decimal workSSSCL = 0;
                    decimal workPagibigSL = 0;
                    decimal workPagibigCL = 0;
                    decimal workOthers = 0;
                    decimal workAdjustment = 0;
                    decimal nighttime = 0;

                    foreach (var item in Payrolls)
                    {

                        sLDocument.SetCellValue($"B{row}", item.FullName);
                        sLDocument.SetCellValue($"C{row}", item.Salary);
                        sLDocument.SetCellValue($"D{row}", item.BasicPay);
                        sLDocument.SetCellValue($"E{row}", item.WorkDays);
                        sLDocument.SetCellValue($"F{row}", item.WorkDaysAmount);

                        //2
                        workDaysTotalAmount += item.WorkDaysAmount;

                        sLDocument.SetCellValue($"G{row}", item.OverTimeMinutes);
                        sLDocument.SetCellValue($"H{row}", item.WorkOvertimeAmount);
                        wordOTTotalAmount += item.WorkOvertimeAmount;

                        sLDocument.SetCellValue($"I{row}", item.RegularHolidayDays);
                        sLDocument.SetCellValue($"J{row}", item.RegularHolidayAmount);
                        workRegHolAmount += item.RegularHolidayAmount;

                        sLDocument.SetCellValue($"K{row}", item.OverTimeRegularHolidayMinutes);
                        sLDocument.SetCellValue($"L{row}", item.TotalRegularHolidayAmount);
                        workRegHolOTAmount += item.TotalRegularHolidayAmount;

                        sLDocument.SetCellValue($"M{row}", item.SpecialHolidayDays);
                        sLDocument.SetCellValue($"N{row}", item.SpecialHolidayAmount);
                        workSpeHolAmount += item.SpecialHolidayAmount;

                        sLDocument.SetCellValue($"O{row}", item.OTSpecialHolidayMinutes);
                        sLDocument.SetCellValue($"P{row}", item.OtSpecialHolidayAmount);
                        workSpeHolOTAmount += item.OtSpecialHolidayAmount;

                        sLDocument.SetCellValue($"Q{row}", item.WeekendMinutes);
                        sLDocument.SetCellValue($"R{row}", item.WeekendDutyAmount);
                        workWeekendAmount += item.WeekendDutyAmount;

                        sLDocument.SetCellValue($"S{row}", item.Allowance);
                        workAllowance += item.Allowance;

                        sLDocument.SetCellValue($"T{row}", item.TotalMinutesOfAbsent);
                        sLDocument.SetCellValue($"U{row}", item.AbsentAmount);
                        workAbsent += item.AbsentAmount;

                        sLDocument.SetCellValue($"V{row}", item.TotalMinutesOfLate);
                        sLDocument.SetCellValue($"W{row}", item.MinutesAmount);
                        workTardiness += item.MinutesAmount;

                        sLDocument.SetCellValue($"X{row}", item.TotalMinutesOfUndertime);
                        sLDocument.SetCellValue($"Y{row}", item.UndertimeAmount);
                        workUndertime += item.UndertimeAmount;

                        sLDocument.SetCellValue($"Z{row}", item.Vale);
                        workVale += item.Vale;

                        sLDocument.SetCellValue($"AA{row}", item.GrossIncome);
                        workGrossIncome += item.GrossIncome;

                        sLDocument.SetCellValue($"AB{row}", item.PhilhealthContribution);
                        workPhilhealth += item.PhilhealthContribution;

                        sLDocument.SetCellValue($"AC{row}", item.PagIbigContribution);
                        workPagIbig += item.PagIbigContribution;

                        sLDocument.SetCellValue($"AD{row}", item.SssContribution);
                        workSSS += item.SssContribution;

                        sLDocument.SetCellValue($"AE{row}", item.SssProvidentFund);
                        workSSSMPF += item.SssProvidentFund;

                        sLDocument.SetCellValue($"AF{row}", item.SssSalaryLoan);
                        workSSSSL += item.SssSalaryLoan;

                        sLDocument.SetCellValue($"AG{row}", item.SssCalamityLoan);
                        workSSSCL += item.SssCalamityLoan;

                        sLDocument.SetCellValue($"AH{row}", item.PagibigSalaryLoan);
                        workPagibigSL += item.PagibigSalaryLoan;

                        sLDocument.SetCellValue($"AI{row}", item.PagibigCalamityLoan);
                        workPagibigCL += item.PagibigCalamityLoan;

                        sLDocument.SetCellValue($"AJ{row}", item.Others);
                        workOthers += item.Others;

                        sLDocument.SetCellValue($"AK{row}", item.Adjustments);
                        workAdjustment += item.Adjustments;

                        sLDocument.SetCellValue($"AL{row}", item.NightTimeAmount);
                        nighttime += item.NightTimeAmount;


                        sLDocument.SetCellValue($"AM{row}", item.TotalSalary);
                        totalPayroll += item.TotalSalary;
                        row++;
                    }
                    var lastRow = row;
                    sLDocument.SetCellValue($"AK{lastRow}", totalPayroll);

                    //3
                    sLDocument.SetCellValue($"F{lastRow}", workDaysTotalAmount);
                    sLDocument.SetCellValue($"H{lastRow}", wordOTTotalAmount);
                    sLDocument.SetCellValue($"J{lastRow}", workRegHolAmount);
                    sLDocument.SetCellValue($"L{lastRow}", workRegHolOTAmount);
                    sLDocument.SetCellValue($"N{lastRow}", workSpeHolAmount);
                    sLDocument.SetCellValue($"P{lastRow}", workSpeHolOTAmount);
                    sLDocument.SetCellValue($"R{lastRow}", workWeekendAmount);
                    sLDocument.SetCellValue($"S{lastRow}", workAllowance);
                    sLDocument.SetCellValue($"U{lastRow}", workAbsent);
                    sLDocument.SetCellValue($"W{lastRow}", workTardiness);
                    sLDocument.SetCellValue($"y{lastRow}", workUndertime);
                    sLDocument.SetCellValue($"Z{lastRow}", workVale);
                    sLDocument.SetCellValue($"AA{lastRow}", workGrossIncome);
                    sLDocument.SetCellValue($"AB{lastRow}", workPhilhealth);
                    sLDocument.SetCellValue($"AC{lastRow}", workPagIbig);
                    sLDocument.SetCellValue($"AD{lastRow}", workSSS);
                    sLDocument.SetCellValue($"AE{lastRow}", workSSSMPF);
                    sLDocument.SetCellValue($"AF{lastRow}", workSSSSL);
                    sLDocument.SetCellValue($"AG{lastRow}", workSSSCL);
                    sLDocument.SetCellValue($"AH{lastRow}", workPagibigSL);
                    sLDocument.SetCellValue($"AI{lastRow}", workPagibigCL);
                    sLDocument.SetCellValue($"AJ{lastRow}", workOthers);
                    sLDocument.SetCellValue($"AK{lastRow}", workAdjustment);
                    sLDocument.SetCellValue($"AL{lastRow}", nighttime);


                    sLDocument.SaveAs(savingPath);
                }

                savingPath.SecureExcelFile("597313");

            });
        }
        private void GenerateCutOff()
        {
            using (var context = new PayrollDbContext())
            {
                var cutOffS = context.CutOffs.ToList();

                CutOffs = new ObservableCollection<Models.CutOff>(context.CutOffs.ToList().OrderByDescending(x => x.Id));
            }
        }
        private async Task UpdatePayrollEvent()
        {
            if (SelectedPayroll == null) return;
            MessageBox.Show("Save Successfully");

            using (var context = new PayrollDbContext())
            {
                var itemExists = context.Payrolls.Where(x => x.EmployeeNo == SelectedPayroll.EmployeeNo
                && x.PayrollId == SelectedCutOff.Id).FirstOrDefault();

                if (itemExists == null) return;
                itemExists.Vale = PayrollVale;
                itemExists.Others = PayrollOthers;
                itemExists.Adjustments = PayrollAdjustments;
                itemExists.OverTimeMinutes = PayrollOT;
                itemExists.TotalMinutesOfUndertime = PayrollUndertime;
                itemExists.Allowance = PayrollAllowance;
                itemExists.RegularHolidayDays = PayrollRegHol;
                itemExists.SpecialHolidayDays = PayrollSpeHol;
                itemExists.TranspoAllowance = PayrollTranspoAllowance;
                itemExists.SssProvidentFund = PayrollSssProfund;
                itemExists.SssCalamityLoan = PayrollSssCalamityLoan;
                itemExists.SssSalaryLoan = PayrollSssSalaryLoan;
                itemExists.PagibigCalamityLoan = PayrollPagIbigCalLoan;
                itemExists.PagibigSalaryLoan = PayrollPagibigSalLoan;
                itemExists.WeekendMinutes = PayrollWeekend;
                itemExists.OTSpecialHolidayMinutes = PayrollSpeHolOT;
                itemExists.OverTimeRegularHolidayMinutes = PayrollRegHolOT;
                itemExists.PagIbigContribution = PayrollPagibig;
                itemExists.PhilhealthContribution = PayrollPhilhealth;
                itemExists.SssContribution = PayrollSSS;
                itemExists.WorkDays = PayrollWorkDays;
                itemExists.TotalMinutesOfLate = PayrollLate;
                itemExists.TotalMinutesOfAbsent = PayrollAbsent;
                itemExists.NightTimeMinutes = PayrollNightTime; 
               

                //itemExists.Adjustments = PayrollAdjustments;

                GetWorkDaysAmount(itemExists.EmployeeStatus, itemExists);

                UpdateRemainingBalance(itemExists.EmployeeNo, itemExists.Vale, context, SelectedCutOff.Id);


                context.SaveChanges();

                var list = context.Payrolls.Where(x => x.EmployeeStatus == SelectedStatus && x.PayrollId == SelectedCutOff.Id).ToList();
                Payrolls = new ObservableCollection<Models.Payroll>(list);


            }

            await Task.CompletedTask;
        }
        private void UpdateRemainingBalance(string employeeNo, decimal vale, PayrollDbContext context, int payrollId)
        {

            if (vale == 0)
            {
                return;
            }

            var tracker = context.CashAdvanceTracker.Where(x => x.EmployeeNo == employeeNo && x.PayrollId == payrollId).FirstOrDefault();
                
            if (tracker == null)
            {
                var trackerModel = new Models.CashAdvanceTracker
                {
                    EmployeeNo = employeeNo,
                    Amount = vale,
                    PayrollId = payrollId
                };

                context.CashAdvanceTracker.Add(trackerModel);

                var cashAdvance = context.CashAdvances.Where(x => x.EmployeeNo == employeeNo).FirstOrDefault();
                if (cashAdvance != null)
                {
                    cashAdvance.RemainingBalance -= vale;
                }
            }
        }
        private List<Models.Attendance> GetAttendance(string employeeNo)
        {
            using (var context = new PayrollDbContext())
            {
                List<Models.Attendance> attendances = new List<Models.Attendance>();

                foreach (var a in context.Attendance)
                {
                    if (a.STUDENTID != employeeNo) continue;

                    if (SelectedCutOff == null)
                    {
                        continue;
                    }

                    var day = $"{a.LOGDATE[0]}{a.LOGDATE[1]}";
                    var month = $"{a.LOGDATE[3]}{a.LOGDATE[4]}";
                    var year = a.LOGDATE.Substring(6);
                    var dateTime = DateTime.TryParse($"{day}/{month}/{year}", out DateTime result);


                    if (dateTime)
                    {
                        if (result.Date >= SelectedCutOff.StartDate.Date && result.Date <= SelectedCutOff.EndDate.Date)
                        {
                            //if the parsed date is Saturday or Sunday hindi ia-add sa attendance count
                            if (result.DayOfWeek != DayOfWeek.Saturday && result.DayOfWeek != DayOfWeek.Sunday)
                            {
                                attendances.Add(a);
                            }

                        }
                    }
                }
                return attendances;
            }
        }
        private List<Models.Attendance> GetWeekendValues(string employeeNo)
        {
            using (var context = new PayrollDbContext())
            {
                List<Models.Attendance> attendances = new List<Models.Attendance>();

                foreach (var a in context.Attendance)
                {
                    if (a.STUDENTID != employeeNo) continue;

                    if (SelectedCutOff == null)
                    {
                        continue;
                    }

                    var day = $"{a.LOGDATE[0]}{a.LOGDATE[1]}";
                    var month = $"{a.LOGDATE[3]}{a.LOGDATE[4]}";
                    var year = a.LOGDATE.Substring(6);
                    var dateTime = DateTime.TryParse($"{day}/{month}/{year}", out DateTime result);


                    if (dateTime)
                    {
                        if (result.Date >= SelectedCutOff.StartDate.Date && result.Date <= SelectedCutOff.EndDate.Date)
                        {
                            //if the parsed date is Saturday or Sunday hindi ia-add sa attendance count
                            if (result.DayOfWeek == DayOfWeek.Saturday || result.DayOfWeek == DayOfWeek.Sunday)
                            {
                                attendances.Add(a);
                            }

                        }
                    }
                }
                return attendances;
            }
        }
        private IEnumerable<TimeSpan> NumberOfLates(List<Models.Attendance> attendances)
        {
            foreach (var item in attendances)
            {
                DateTime.TryParse(item.TIMEIN, out DateTime timeIn);
                DateTime.TryParse("8:00 AM", out DateTime startShiftTime);
                DateTime.TryParse("8:11 AM", out DateTime lateTime);
                DateTime.TryParse("12:00 PM", out DateTime startBreak);
                DateTime.TryParse("1:00 PM", out DateTime endBreak);

                if(timeIn >= endBreak)
                {
                    //meaning pumasok ng half-day afternoon
                    continue;
                }
                else if (timeIn >= startBreak && timeIn <= endBreak)
                {
                    //walang late kung ang time in is from
                    //12PM - 1PM
                    continue;
                }
                else if(timeIn >= lateTime && timeIn <= startBreak)
                {
                    // 8:12AM >= 8:11AM and 8:12AM <= 12:00PM
                    yield return startShiftTime.Subtract(timeIn);
                }
            }
        }
        private IEnumerable<decimal> NumberOfUndertime(List<Models.Attendance> attendances)
        {
            foreach (var item in attendances)
            {
                var timeStartCorrect = DateTime.TryParse(item.TIMEIN, out DateTime startShiftDate);
                var timeEndCorrect = DateTime.TryParse(item.TIMEOUT, out DateTime endShiftDate);
                var time = DateTime.TryParse("6:00 PM", out DateTime dateTime);
                var morningShiftDateValid = DateTime.TryParse("08:00 AM", out DateTime morningShiftDate);
                var afternoonShiftValid = DateTime.TryParse("12:00 PM", out DateTime afternoonDateTime);

                if (endShiftDate >= dateTime)
                {
                    continue;
                }

                // 12:00PM >= 12:01 PM && 12:00PM <= 1:00PM
                if(endShiftDate >= afternoonDateTime && endShiftDate <= afternoonDateTime.AddHours(1))
                {
                    continue;
                }



                if (timeEndCorrect && timeStartCorrect)
                {
                    //kung yung startShiftDate ay less than 8am dapat, 8am ang value neto
                    var morningShiftValue = startShiftDate <= morningShiftDate ? morningShiftDate : startShiftDate;

                    //kung anong oras umuwi
                    var endShiftValue = endShiftDate;

                    //tanghali 12PM
                    var afternoonShiftValue = afternoonDateTime;


                    
                    var morningTime = afternoonShiftValue.Subtract(morningShiftValue);
                    var afternoonTime = endShiftValue.Subtract(afternoonShiftValue.AddHours(1));

                    var afternoonMinutes = afternoonTime.TotalMinutes < 0 ? 0 : afternoonTime.TotalMinutes;

                    var totalMinutes = morningTime.TotalMinutes + afternoonMinutes;
                    var additional = 540 - totalMinutes;

                    yield return Decimal.Parse(Math.Ceiling(additional).ToString("0.00"));
                }
                #region Commented Code
                //if (DateTime.TryParse(item.TIMEOUT, out DateTime t1))
                //{
                //    if (t1.TimeOfDay <= originalDateTime.TimeOfDay)
                //    {
                //        if (endShift)
                //        {
                //            if(t1 <= afternoonShiftTime)
                //            {
                //                var result = originalDateTime.Subtract(t1);

                //                if (result.TotalMinutes > 0)
                //                {
                //                    yield return result.TotalMinutes - 60;
                //                }
                //            }
                //            else
                //            else
                //            {
                //                var result = originalDateTime.Subtract(t1);

                //                if (result.TotalMinutes > 0)
                //                {
                //                    yield return result.TotalMinutes;
                //                }
                //            }
                //        }
                //    }
                //}
                #endregion

            }


        }
        public double TotalOfMinutes(List<Models.Attendance> attendances)
        {
            var results = new List<double>();

            double totalSpan = 0;
            foreach (var item in attendances)
            {
                var timeStartCorrect = DateTime.TryParse(item.TIMEIN, out DateTime startShiftDate);
                var timeEndCorrect = DateTime.TryParse(item.TIMEOUT, out DateTime endShiftDate);
                var time = DateTime.TryParse("6:00 PM", out DateTime dateTime);
                var morningShiftDateValid = DateTime.TryParse("08:00 AM", out DateTime morningShiftDate);
                var afternoonShiftValid = DateTime.TryParse("12:00 PM", out DateTime afternoonDateTime);


                if (timeEndCorrect && timeStartCorrect)
                {

                    var morningShiftValue = startShiftDate <= morningShiftDate ? morningShiftDate : startShiftDate;
                    var endShiftValue = endShiftDate;
                    var afternoonShiftValue = afternoonDateTime;

                    var morningTime = afternoonShiftValue.Subtract(morningShiftValue);
                    var afternoonTime = endShiftValue.Subtract(afternoonShiftValue.AddHours(1));
                    var additional = morningTime.TotalMinutes + afternoonTime.TotalMinutes;

                    var roundedValue = Math.Floor(additional);
                    totalSpan += roundedValue;
                }
            }

            return totalSpan;
        }
        private bool IsPayrollExists(string employeeNo, int cutOffId)
        {
            using (var newContext = new PayrollDbContext())
            {
                var isPayrollExists = newContext.Payrolls
                    .Where(x => x.EmployeeNo.Equals(employeeNo) && x.PayrollId == cutOffId).FirstOrDefault();
                if (isPayrollExists != null)
                {
                    return true;
                }

                return false;
            }
        }
        private Models.Payroll GetPayroll(string employeeNo, int id)
        {
            using (var context = new PayrollDbContext())
            {
                var existingItem = context.Payrolls.Where(x =>
                      x.EmployeeNo.Equals(employeeNo) && x.PayrollId == id).FirstOrDefault();
                return existingItem ?? null;
            }
        }
        private void GeneratePayroll()
        {
            Payrolls = new ObservableCollection<Models.Payroll>();
            using (var context = new PayrollDbContext())
            {
                var employees = context.Employees.OrderBy(x => x.Lastname).Where(y => y.Status.Equals(SelectedStatus));

                foreach (var item in employees)
                {
                    //if (item.EmployeeNo != "21-8054")
                    //{
                    //    continue;
                    //}

                    if (SelectedCutOff == null) continue;

                    #region Variables
                    var attendance = GetAttendance(item.EmployeeNo);
                    var weekEndAttendace = GetWeekendValues(item.EmployeeNo);
                    var totalWeekends = Decimal.Parse(TotalOfMinutes(weekEndAttendace).ToString("0.00"));
                    var lates = NumberOfLates(attendance).ToList();
                    var undertimes = NumberOfUndertime(attendance).ToList();
                    var totalOfLates = lates.TotalLates();
                    var totalOfUnderTime = undertimes.TotalUndertimes();
                    var workedDays = attendance.Count();
                    var realWorkingDays = attendance.GetWorkingDays();
                    var startCutOff = SelectedCutOff.StartDate.ToShortDateString();
                    var endCutOff = SelectedCutOff.EndDate.ToShortDateString();
                    var bussinessDays = new DayComputation(startCutOff, endCutOff).BussinessDays().ToList();
                    var absentCount = attendance.ActualAbsentCount(bussinessDays); 
                    var absentMinutes = Math.Floor(absentCount) * 540;
                    var totalAbsentMinutes = absentMinutes.GetAbsent(attendance);
                    double? sssSalaryLoan = 0;
                    double? sssCalamityLoan = 0;
                    double? pagibigSalaryLoan = 0;
                    double? pagibigCalamityLoan = 0;

                    #region Loans
                    var loans = HasLoans(item.EmployeeNo);
                    if (loans.Count() != 0)
                    {
                        foreach (var loan in loans)
                        {
                            if (loan.LoanStatus)
                            {
                                var loanStatus = GetLoanStatus(loan.Id);
                                if (loanStatus.RemainingTerms < loanStatus.Terms)
                                {
                                    int startDay = SelectedCutOff.StartDate.Day;
                                    int endDay = SelectedCutOff.EndDate.Day;

                                    if (startDay == 26 && endDay == 10)
                                    {
                                        if (loan.Type == "SSS Salary Loan")
                                        {
                                            sssSalaryLoan = loan.MonthlyAmortization;
                                            TrackLoans(loan.Id, sssSalaryLoan, SelectedCutOff.Id);
                                        }
                                        else if (loan.Type == "SSS Calamity Loan")
                                        {
                                            sssCalamityLoan = loan.MonthlyAmortization;
                                            TrackLoans(loan.Id, sssCalamityLoan, SelectedCutOff.Id);
                                        }
                                    }
                                    else
                                    {
                                        if (loan.Type == "Pagibig Salary Loan")
                                        {
                                            pagibigSalaryLoan = loan.MonthlyAmortization;
                                            TrackLoans(loan.Id, pagibigSalaryLoan, SelectedCutOff.Id);
                                        }
                                        else if (loan.Type == "Pagibig Calamity Loan")
                                        {
                                            pagibigCalamityLoan = loan.MonthlyAmortization;
                                            TrackLoans(loan.Id, pagibigCalamityLoan, SelectedCutOff.Id);
                                        }
                                    }
                                }
                                else
                                {
                                    UpdateLoanStatus(loan.Id);
                                    sssSalaryLoan = 0;
                                    sssCalamityLoan = 0;
                                    pagibigSalaryLoan = 0;
                                    pagibigCalamityLoan = 0;
                                }
                            }
                        }
                    }

                    #endregion


                    #endregion

                    var payroll = new Payroll
                    {
                        EmployeeNo = item.EmployeeNo,
                        FullName = $"{item.Lastname} {item.Firstname}",
                        Salary = (decimal)item.BasicPay,
                        EmployeeStatus = item.Status,
                        TotalMinutesOfAbsent = totalAbsentMinutes,
                        TotalMinutesOfLate = Decimal.Parse(totalOfLates.ToString("0.00")),
                        TotalMinutesOfUndertime = Decimal.Parse(totalOfUnderTime.ToString("0.00")),
                        WeekendMinutes = (decimal)totalWeekends,
                        PayrollId = SelectedCutOff.Id,
                        WorkDays = realWorkingDays,
                        SssSalaryLoan = (decimal)sssSalaryLoan,
                        SssCalamityLoan = (decimal)sssCalamityLoan,
                        PagibigSalaryLoan = (decimal)pagibigSalaryLoan,
                        PagibigCalamityLoan = (decimal)pagibigCalamityLoan
                    };

                    if (item.Status == "Daily")
                    {
                        GetDailyWorkDaysAmount(item.Status, payroll, attendance);
                    }

                    GetWorkDaysAmount(item.Status, payroll);


                    #region Contributions
                    if (SelectedCutOff.StartDate.Day == 26 && SelectedCutOff.EndDate.Day == 10)
                    {
                        var lastPayrollExists = GetPayroll(item.EmployeeNo, SelectedCutOff.Id - 1);
                        var lastLastPayrollExists = GetPayroll(item.EmployeeNo, SelectedCutOff.Id - 2);


                        if (lastPayrollExists != null && lastLastPayrollExists != null)
                        {
                            var totalGrossIncome = lastPayrollExists.GrossIncome + lastLastPayrollExists.GrossIncome;
                            payroll.SssContribution = GetSssContribution(totalGrossIncome);
                            payroll.SssProvidentFund = GetSssMandatoryFund(totalGrossIncome);
                        }
                        else
                        {
                            Console.WriteLine($"{payroll.EmployeeNo}");
                        }

                    }
                    else
                    {
                        var decimalPhilHealth = (decimal)(item.BasicPay * 0.04 / 2);
                        payroll.PhilhealthContribution = Decimal.Parse(decimalPhilHealth.ToString("0.00"));
                        payroll.PagIbigContribution = 100M;
                    }
                    #endregion



                   


                    #region Insertions of Payroll
                    if (!IsPayrollExists(item.EmployeeNo, SelectedCutOff.Id))
                    {
                        context.Payrolls.Add(payroll);
                        Payrolls.Add(payroll);
                    }
                    else
                    {
                        var existingPayroll = GetPayroll(item.EmployeeNo, SelectedCutOff.Id);

                        if (existingPayroll != null)
                        {
                            GetWorkDaysAmount(item.Status, existingPayroll);
                            Payrolls.Add(existingPayroll);
                        }
                    }
                    #endregion

                }

                context.SaveChanges();
            }
        }

        private void UpdateLoanStatus(int id)
        {
            using (var context = new PayrollDbContext())
            {
                var loan= context.Loans.Where(x => x.Id == id).FirstOrDefault();
                if(loan != null)
                {
                    loan.LoanStatus = false;
                    context.SaveChanges();
                }
            }
        }

        private decimal GetSssContribution(decimal totalGrossIncome)
        {
            if (totalGrossIncome != 0)
            {
                var x = new Models.ExcelSss().GetSssColumn(totalGrossIncome);
                return x != null ? (decimal)x.RSEEE : 0;
            }
            return 0;
        }
        private decimal GetSssMandatoryFund(decimal totalGrossIncome)
        {
            if (totalGrossIncome != 0)
            {
                var x = new Models.ExcelSss().GetSssColumn(totalGrossIncome);
                return x != null ? (decimal)x.MPEE : 0;
            }

            return 0;
        }
        private Models.CashAdvanceModel GetCashAdvance(string EmployeeNo)
        {
            using (var context = new PayrollDbContext())
            {
                var result = context.CashAdvances.Where(x => x.EmployeeNo == EmployeeNo).FirstOrDefault();
                return result;
            }
        }
        private List<ActualAttendance> GetActualAttendances(List<Models.Attendance> collection)
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
        public void GetDailyWorkDaysAmount(string status,Models.Payroll payroll,List<Models.Attendance> attendances)
        {
            if(status == "Daily")
            {
                var result = payroll.DailyRate.TotalWorkingDays(attendances);


                payroll.WorkDaysAmount = result;
            }
        }

        public void GetWorkDaysAmount(string status, Models.Payroll payroll)
        {
            switch (status)
            {
                case "Monthly":
                case "Managerial":
                    payroll.WorkDaysAmount = (decimal)payroll.BasicPay;
                    break;
              
            }
        }

        private List<Loan> HasLoans(string employeeNo)
        {
            using (var context = new PayrollDbContext())
            {
                var loan = context.Loans.Where(x => x.EmployeeNo == employeeNo).ToList();
                return loan;
            }
        }

        private LoanStatus GetLoanStatus(int id)
        {
            using (var context = new PayrollDbContext())
            {
                var param = new MySqlParameter {
                    ParameterName = "@loanId",
                    Value = id,
                    MySqlDbType = MySqlDbType.Int32
                };
                var loan = context.Database.SqlQuery<LoanStatus>("CALL sp_checkRemainingloan(?)",param).FirstOrDefault();
                return loan;
            }
        }


        private void TrackLoans(int loanId,double? amortization,int payrollId)
        {
            try
            {
                using (var context = new PayrollDbContext())
                {
                    var loanIdExits = context.LoanTrackers
                        .Where(x => x.LoanId == loanId && x.PayrollId == payrollId).FirstOrDefault();

                    if (loanIdExits == null)
                    {
                        context.LoanTrackers.Add(new LoanTracker
                        {
                            LoanId = loanId,
                            PayrollId = payrollId,
                            MonthlyAmortization = amortization
                        });

                        context.SaveChanges();
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        

        #endregion
    
    }
    class LoanStatus
    {
        public int RemainingTerms { get; set; }
        public int Terms { get; set; }
        public double? TotalLoans { get; set; }
    }
}