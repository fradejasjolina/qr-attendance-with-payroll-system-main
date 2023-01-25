using ControlzEx.Theming;
using DevExpress.Mvvm;
using PayrollSystem.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PayrollSystem.ViewModels
{
    class SettingsViewModel : Abstracts.RaisePropertyChanged
    {
        #region Commands
        public AsyncCommand SaveSettingsCommandAsync { get; set; }
        #endregion

        #region Properties
        private string _ReportPath;

        public string ReportPath
        {
            get { return _ReportPath; }
            set { _ReportPath = value;
                OnPropertyChanged();
            }
        }

        #region Payroll Properties
        private bool _IsSalaryEnabled;

        public bool IsSalaryEnabled
        {
            get { return _IsSalaryEnabled; }
            set { _IsSalaryEnabled = value;
                OnPropertyChanged();
            }
        }


        private bool _IsBasicPayEnabled;

        public bool IsBasicPayEnabled
        {
            get { return _IsBasicPayEnabled; }
            set { _IsBasicPayEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _IsWorkDaysAmountEnabled;

        public bool IsWorkDaysAmountEnabled
        {
            get { return _IsWorkDaysAmountEnabled; }
            set { _IsWorkDaysAmountEnabled = value;
                OnPropertyChanged();
            }
        }


        private bool _IsOvertimeEnabled;

        public bool IsOvertimeEnabled
        {
            get { return _IsOvertimeEnabled; }
            set { _IsOvertimeEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _IsRegularHolidayAmountEnabled;

        public bool IsRegularHolidayAmountEnabled
        {
            get { return _IsRegularHolidayAmountEnabled; }
            set { _IsRegularHolidayAmountEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _IsDailyRateEnabled;

        public bool IsDailyRateEnabled
        {
            get { return _IsDailyRateEnabled; }
            set { _IsDailyRateEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _IsRegularHolidayOtMinsAmountEnabled;

        public bool IsRegularHolidayOtMinsAmountEnabled
        {
            get { return _IsRegularHolidayOtMinsAmountEnabled; }
            set { _IsRegularHolidayOtMinsAmountEnabled = value;
                OnPropertyChanged();
            }
        }
        private bool _IsSpecialHolidayAmountEnabled;

        public bool IsSpecialHolidayAmountEnabled
        {
            get { return _IsSpecialHolidayAmountEnabled; }
            set { _IsSpecialHolidayAmountEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _IsSpecialHolidayOtMinsAmountEnabled;

        public bool IsSpecialHolidayOtMinsAmountEnabled
        {
            get { return _IsSpecialHolidayOtMinsAmountEnabled; }
            set { _IsSpecialHolidayOtMinsAmountEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _IsSaturdaySundayDutyAmountEnabled;

        public bool IsSaturdaySundayDutyAmountEnabled
        {
            get { return _IsSaturdaySundayDutyAmountEnabled; }
            set { _IsSaturdaySundayDutyAmountEnabled = value;
                OnPropertyChanged();
            }
        }
        private bool _IsTranspoAllowanceEnabled;

        public bool IsTranspoAllowanceEnabled
        {
            get { return _IsTranspoAllowanceEnabled; }
            set { _IsTranspoAllowanceEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _IsAllowanceEnabled;

        public bool IsAllowanceEnabled
        {
            get { return _IsAllowanceEnabled; }
            set { _IsAllowanceEnabled = value;
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


        #endregion


        #endregion


        public SettingsViewModel()
        {
            SaveSettingsCommandAsync = new AsyncCommand(SaveSettingsEvent);
            ReportPath = Setting.ReportPath;
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
                IsPayrollPrintEnabled = obj.IsPayrollPrintEnabled;
            }
        }

        private async Task SaveSettingsEvent()
        {
            using (var context = new PayrollDbContext())
            {
                var isExists = context.Settings.FirstOrDefault();
                if (isExists != null)
                {
                    Models.PayrollSettings payrollSettings = new Models.PayrollSettings
                    {
                        IsSalaryEnabled = IsSalaryEnabled,
                        IsBasicPayEnabled = IsBasicPayEnabled,
                        IsWorkDaysAmountEnabled = IsWorkDaysAmountEnabled,
                        IsOvertimeEnabled = IsOvertimeEnabled,
                        IsRegularHolidayAmountEnabled = IsRegularHolidayAmountEnabled,
                        IsRegularHolidayOtMinsAmountEnabled = IsRegularHolidayOtMinsAmountEnabled,
                        IsSpecialHolidayAmountEnabled = IsSpecialHolidayAmountEnabled,
                        IsSpecialHolidayOtMinsAmountEnabled = IsSpecialHolidayOtMinsAmountEnabled,
                        IsSaturdaySundayDutyAmountEnabled = IsSaturdaySundayDutyAmountEnabled,
                        IsTranspoAllowanceEnabled = IsTranspoAllowanceEnabled,
                        IsAllowanceEnabled = IsAllowanceEnabled,
                        IsDailyRateEnabled = IsDailyRateEnabled,
                        IsPayrollPrintEnabled = IsPayrollPrintEnabled
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(payrollSettings);

                    isExists.ReportPath = ReportPath;
                    isExists.PayrollSettings = json;

                    await context.SaveChangesAsync();
                    MessageBox.Show("Save Successfully!");
                }
            }
        }
    }
}
