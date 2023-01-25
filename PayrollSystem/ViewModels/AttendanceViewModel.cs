using DevExpress.Mvvm;
using Google.Protobuf.Collections;
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
using System.Windows.Threading;

namespace PayrollSystem.ViewModels
{
    class AttendanceViewModel : Abstracts.RaisePropertyChanged
    {
        #region Commands
        public DelegateCommand FilterAttendanceCommand { get; set; }
        public AsyncCommand PrintAttendanceAsyncCommand { get; set; }
        public DelegateCommand ManualAttendanceCommand { get; set; }
        public DelegateCommand DeleteRecordCommand { get; set; }
        #endregion

        #region Properties
        private ObservableCollection<Models.Attendance> _Attendance;

        public ObservableCollection<Models.Attendance> Attendance
        {
            get { return _Attendance; }
            set
            {
                _Attendance = value;
                OnPropertyChanged();
            }
        }

        private string _AttendanceCount;

        public string AttendanceCount
        {
            get { return _AttendanceCount; }
            set
            {
                _AttendanceCount = value;
                OnPropertyChanged();
            }
        }

        private string _EmployeeCount;

        public string EmployeeCount
        {
            get { return _EmployeeCount; }
            set
            {
                _EmployeeCount = value;
                OnPropertyChanged();
            }
        }

        private Models.Attendance _SelectedAttendance;

        public Models.Attendance SelectedAttendance
        {
            get { return _SelectedAttendance; }
            set
            {
                _SelectedAttendance = value;
                OnPropertyChanged();
                if (SelectedAttendance == null)
                {
                    return;
                }

            }
        }

        private DateTime _SelectedStartDate;

        public DateTime SelectedStartDate
        {
            get { return _SelectedStartDate; }
            set
            {
                _SelectedStartDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime _SelectedEndDate;

        public DateTime SelectedEndDate
        {
            get { return _SelectedEndDate; }
            set
            {
                _SelectedEndDate = value;
                OnPropertyChanged();
            }
        }

        private Models.Employee _SelectedEmployee;

        public Models.Employee SelectedEmployee
        {
            get { return _SelectedEmployee; }
            set
            {
                _SelectedEmployee = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Models.Employee> _Employees;

        public ObservableCollection<Models.Employee> Employees
        {
            get { return _Employees; }
            set
            {
                _Employees = value;
                OnPropertyChanged();
            }
        }


        private bool _IsPrintButtonEnabled;

        public bool IsPrintButtonEnabled
        {
            get { return _IsPrintButtonEnabled; }
            set
            {
                _IsPrintButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Models.BirthdateCelebrants> _BirthdateCelebrantsCollection;

        public ObservableCollection<Models.BirthdateCelebrants> BirthdateCelebrantsCollection
        {
            get { return _BirthdateCelebrantsCollection; }
            set
            {
                _BirthdateCelebrantsCollection = value;
                OnPropertyChanged();
            }
        }

        private decimal _TotalSalaryOfLastCutOff;

        public decimal TotalSalaryOfLastCutOff
        {
            get { return _TotalSalaryOfLastCutOff; }
            set
            {
                _TotalSalaryOfLastCutOff = value;
                OnPropertyChanged();
            }
        }

        private Visibility _NoBirthdateVisibility;

        public Visibility NoBirthdateVisibility
        {
            get { return _NoBirthdateVisibility; }
            set
            {
                _NoBirthdateVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility BirthdateCollectionDisplay;

        public Visibility _BirthdateCollectionDisplay
        {
            get { return BirthdateCollectionDisplay; }
            set
            {
                BirthdateCollectionDisplay = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Tenure Properties
        private ObservableCollection<Models.BirthdateCelebrants> _TenureCollection;

        public ObservableCollection<Models.BirthdateCelebrants> TenureCollection
        {
            get { return _TenureCollection; }
            set
            {
                _TenureCollection = value;
                OnPropertyChanged();
            }
        }

        private Visibility _NoTenureVisibility;

        public Visibility NoTenureVisibility
        {
            get { return _NoTenureVisibility; }
            set
            {
                _NoTenureVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility _TenureCollectionDisplay;

        public Visibility TenureCollectionDisplay
        {
            get { return _TenureCollectionDisplay; }
            set
            {
                _TenureCollectionDisplay = value;
                OnPropertyChanged();
            }
        }



        private int _NumberOfResignedEmployee;

        public int NumberOfResignedEmployee
        {
            get { return _NumberOfResignedEmployee; }
            set
            {
                _NumberOfResignedEmployee = value;
                OnPropertyChanged();
            }
        }

        private decimal _TotalRemainingBalance;

        public decimal TotalRemainingBalance
        {
            get { return _TotalRemainingBalance; }
            set
            {
                _TotalRemainingBalance = value;
                OnPropertyChanged();
            }
        }



        #endregion

        #region Additional Properties

        private double _CurrentLeaveCount;

        public double CurrentLeaveCount
        {
            get { return _CurrentLeaveCount; }
            set
            {
                _CurrentLeaveCount = value;
                OnPropertyChanged();
            }
        }

        private double GetCurrentLeave()
        {
            using (var context = new PayrollDbContext())
            {
                var currentLeave = context.EmployeeLeaves.Where(x => x.EmployeeNo == UserAccount.UserId).FirstOrDefault();
                return currentLeave != null ? currentLeave.CurrentLeave : 0;
            }
        }




        #endregion

        #region Constructor


        private int _SelectedTabIndex;

        public int SelectedTabIndex
        {
            get { return _SelectedTabIndex; }
            set { _SelectedTabIndex = value;
                OnPropertyChanged();

                GenerateData(SelectedTabIndex);
            }
        }


        public AttendanceViewModel()
        {

            DeleteRecordCommand = new DelegateCommand(DeleteRecordEvent);

            CurrentLeaveCount = GetCurrentLeave();

            #region Constructor Content
            IsPrintButtonEnabled = false;

            #region Attendance
            using (var context = new PayrollDbContext())
            {
                var list = context.Attendance.ToList();
                var lists = context.Attendance.OrderByDescending(x => x.ID).Take(200);

                Attendance = new ObservableCollection<Models.Attendance>();

                foreach (var attendance in lists)
                {
                    var day = $"{attendance.LOGDATE[0]}{attendance.LOGDATE[1]}";
                    var month = $"{attendance.LOGDATE[3]}{attendance.LOGDATE[4]}";
                    var year = attendance.LOGDATE.Substring(6);
                    var dateTime = DateTime.TryParse($"{day}/{month}/{year}", out DateTime result);

                    if (dateTime == true && result.Day == DateTime.Now.Day && result.Month == DateTime.Now.Month && result.Year == DateTime.Now.Year)
                    {
                        var isExists = Attendance.Any(y => y.STUDENTID.Equals(attendance.STUDENTID));
                        if (!isExists)
                        {
                            Attendance.Add(attendance);
                        }
                    }
                }
            }

            #endregion

            using (var context = new PayrollDbContext())
            {
                Employees = new ObservableCollection<Models.Employee>();
                
                if (UserAccount.UserType != "Employee" && UserAccount.UserType != "Manager")
                {
                    foreach (var employee in context.Employees.Where(x => x.Status != "Resign").OrderBy(x => x.Firstname))
                    {
                        Employees.Add(employee);
                    }

                }
                else
                {
                    foreach (var employee in context.Employees.Where(x => x.EmployeeNo == UserAccount.UserId).OrderBy(x => x.Firstname))
                    {
                        Employees.Add(employee);
                    }

                }

                var datetimeNow = DateTime.UtcNow.Month;

                var birthdayCelebrants = context.Employees.Where(x => ((DateTime)x.Birthdate).Month == datetimeNow && x.Status != "Resign")
                    .OrderBy(y => ((DateTime)y.Birthdate).Day)
                    .ToList()
                    .Select(x => new Models.BirthdateCelebrants
                    {
                        FullName = $"{x.Firstname} {x.Lastname}",
                        EmployeeNo = x.EmployeeNo,
                        Birthdate = (DateTime)x.Birthdate,
                        PicturePath = x.EmployeePicturePath,
                        Position = x.Position
                    });

                var tenures = context.Employees.Where(x => ((DateTime)x.HiringDate).Month == datetimeNow && x.Status != "Resign")
                   .ToList()
                   .Select(x => new Models.BirthdateCelebrants
                    {
                        FullName = $"{x.Firstname} {x.Lastname}",
                        EmployeeNo = x.EmployeeNo,
                        Birthdate = (DateTime)x.HiringDate,
                        PicturePath = x.EmployeePicturePath,
                        Position = x.Position
                    });


                BirthdateCelebrantsCollection = new ObservableCollection<Models.BirthdateCelebrants>(birthdayCelebrants.ToList());
                TenureCollection = new ObservableCollection<Models.BirthdateCelebrants>(tenures.ToList());


                var cutoffs = context.CutOffs.ToList().LastOrDefault();
                var payrolls = context.Payrolls.Where(x => x.PayrollId == cutoffs.Id).ToList();
                var sumTotal = payrolls.Sum(x => x.TotalSalary);

                TotalSalaryOfLastCutOff = sumTotal;
               
                if (BirthdateCelebrantsCollection.Count <= 0)
                {
                    NoBirthdateVisibility = Visibility.Visible;
                    BirthdateCollectionDisplay = Visibility.Hidden;

                }
                else
                {
                    NoBirthdateVisibility = Visibility.Hidden;
                    BirthdateCollectionDisplay = Visibility.Visible;
                }


                if (TenureCollection.Count <= 0)
                {
                    NoTenureVisibility = Visibility.Visible;
                    TenureCollectionDisplay = Visibility.Hidden;
                }
                else
                {
                    NoTenureVisibility = Visibility.Hidden;
                    TenureCollectionDisplay = Visibility.Visible;

                }

                NumberOfResignedEmployee = context.Employees.Where(x => x.Status == "Resign").Count();

                if (context.CashAdvances.Count() != 0)
                {
                    TotalRemainingBalance = context.CashAdvances.Sum(x => x.RemainingBalance);
                }
            }



            AttendanceCount = $"{Attendance.Count()}/{EmployeeCounter()}";
            EmployeeCount = $"{EmployeeCounter()}";


            SelectedStartDate = DateTime.Now;
            SelectedEndDate = DateTime.Now;
            FilterAttendanceCommand = new DelegateCommand(FilterAttendanceEvent);
            PrintAttendanceAsyncCommand = new AsyncCommand(PrintAttendance);

            #endregion


            ManualAttendanceCommand = new DelegateCommand(ManualAttendanceEvent);
            SelectedDateManual = DateTime.Now;
            TimeFrom = DateTime.Now;
            TimeTo = DateTime.Now;

            
            AddAttendanceVisibility = Visibility.Hidden;
            AttendaceMonitoringVisibility = Visibility.Hidden;
            if (UserAccount.UserType == "Administrator")
            {
                AddAttendanceVisibility = Visibility.Visible;
                AttendaceMonitoringVisibility = Visibility.Visible;
            }

        }
        private void GenerateData(int index)
        {
            switch (index)
            {
                case 1:
                    ManualList = new ObservableCollection<Models.Attendance>(GetEmployees2());
                    break;
            }

        }


        #endregion

        #region Methods
        private async Task PrintAttendance()
        {
            await Task.Run(() => {
                var path = @"\\SERVER\Users\Public\Payroll Templates\AttendanceRecord";

                var date = DateTime.Now.ToShortDateString().Replace('/', '-');

                var savingPath = $"{path}\\AttendanceRecord-{SelectedEmployee.Fullname}.xlsx";
                var row = 2;
                using (SLDocument sLDocument = new SLDocument($"{path}\\AttendanceRecord.xlsx", "Sheet1"))
                {
                    var x = Attendance.Count();

                    foreach (var item in Attendance)
                    {
                        sLDocument.SetCellValue($"A{row}", item.STUDENTID);
                        sLDocument.SetCellValue($"B{row}", item.Fullname);
                        sLDocument.SetCellValue($"C{row}", item.TIMEIN);
                        sLDocument.SetCellValue($"D{row}", item.TIMEOUT);
                        sLDocument.SetCellValue($"E{row}", item.LOGDATE);
                        sLDocument.SetCellValue($"F{row}", item.TOTAL_LATE);
                        sLDocument.SetCellValue($"G{row}", item.STATUS);
                        row++;
                    }
                    sLDocument.SaveAs(savingPath);
                }
                Process.Start(savingPath);
            });

        }

        private void FilterAttendance(string employeeNo)
        {
            using (var context = new PayrollDbContext())
            {
                var list = new List<Models.Attendance>();
                var attendances = context.Attendance.Where(x => x.STUDENTID.Equals(employeeNo));
                foreach (var attendance in attendances)
                {
                    var day = $"{attendance.LOGDATE[0]}{attendance.LOGDATE[1]}";
                    var month = $"{attendance.LOGDATE[3]}{attendance.LOGDATE[4]}";
                    var year = attendance.LOGDATE.Substring(6);
                    var dateTime = DateTime.TryParse($"{day}/{month}/{year}", out DateTime result);
                    var startDate = SelectedStartDate;
                    var endDate = SelectedEndDate;

                    if (result.IsBewteenTwoDates(startDate, endDate))
                    {
                        list.Add(attendance);
                    }
                }

                if (list.Count() != 0)
                {

                    Attendance = new ObservableCollection<Models.Attendance>(list);
                    IsPrintButtonEnabled = true;
                }
                else
                {
                    Attendance = new ObservableCollection<Models.Attendance>();
                }
            }
        }

        private void FilterAttendanceEvent()
        {

            if (SelectedEmployee == null) return;
            FilterAttendance(SelectedEmployee.EmployeeNo);
        }

        private int EmployeeCounter()
        {
            using (var context = new PayrollDbContext())
            {
                return context.Employees.Where(x => x.Status != "Resign" && x.Status != "NoPayroll").Count();
            }
        }
        #endregion

        #region Manual Methods
        private void ManualAttendanceEvent()
        {
         
            var attendanceModel = new Models.AttendanceModel()
            {
                EmployeeNo = SelectedEmployeeManual.EmployeeNo,
                TimeFrom = TimeFrom,
                TimeTo = TimeTo,
                Date = SelectedDateManual
            };
            var msgBoxResult = MessageBox.Show("Do you want to save this record?","Saving Record",MessageBoxButton.OKCancel,MessageBoxImage.Question);
            if (msgBoxResult == MessageBoxResult.OK)
            {
                using (var context = new Database.PayrollDbContext())
                {
                    var attendanceItem = new Models.Attendance
                    {
                        STUDENTID = attendanceModel.EmployeeNo,
                        LOGDATE = attendanceModel.DateString,
                        TIMEIN = attendanceModel.TimeFromString,
                        TIMEOUT = attendanceModel.TimeToString,
                        STATUS = "0",
                        TOTAL_LATE = "0",
                        ID = SelectedManual == null ? 0 : SelectedManual.ID
                    };
                    if (attendanceItem.ID == 0)
                    {

                        context.Attendance.Add(attendanceItem);
                        context.SaveChanges();
                        MessageBox.Show("Saving Successfully");
                        ResetValues();
                    }
                    else
                    {
                        var isRecordExists = context.Attendance.Where(x => x.ID == SelectedManual.ID).FirstOrDefault();
                        isRecordExists.TIMEIN = attendanceModel.TimeFromString;
                        isRecordExists.TIMEOUT = attendanceModel.TimeToString;
                        isRecordExists.LOGDATE = attendanceModel.DateString;
                        context.SaveChanges();
                        MessageBox.Show("Updated Successfully!");
                        ResetValues();
                    }
                    ManualList = new ObservableCollection<Models.Attendance>(GetEmployees2());
                }
            }
        }

        private Models.Employee _SelectedEmployeeManual;

        public Models.Employee SelectedEmployeeManual
        {
            get { return _SelectedEmployeeManual; }
            set
            {
                _SelectedEmployeeManual = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _SelectedDateManual;

        public DateTime? SelectedDateManual
        {
            get { return _SelectedDateManual; }
            set
            {
                _SelectedDateManual = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _TimeFrom;

        public DateTime? TimeFrom
        {
            get { return _TimeFrom; }
            set
            {
                _TimeFrom = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _TimeTo;

        public DateTime? TimeTo
        {
            get { return _TimeTo; }
            set
            {
                _TimeTo = value;
                OnPropertyChanged();
            }
        }


        public List<Models.Attendance> GetEmployees2()
        {
            var manualList = new List<Models.Attendance>();

            using (var context = new Database.PayrollDbContext())
            {
                var list = context.Attendance.ToList();

                var lists = context.Attendance.OrderByDescending(x=>x.ID).Take(200);

                foreach (var attendance in lists)
                {
                    var day = $"{attendance.LOGDATE[0]}{attendance.LOGDATE[1]}";
                    var month = $"{attendance.LOGDATE[3]}{attendance.LOGDATE[4]}";
                    var year = attendance.LOGDATE.Substring(6);
                    var dateTime = DateTime.TryParse($"{day}/{month}/{year}", out DateTime result);

                    var isExists = Attendance.Any(x => x.LOGDATE.Equals(result.ToString("dd-MM-yyyy")));
                    manualList.Add(attendance);
                }
            }

            var returnList = manualList.OrderByDescending(x => x.ID).ToList();
            return returnList;
        }

        private ObservableCollection<Models.Attendance> _ManualList;

        public ObservableCollection<Models.Attendance> ManualList
        {
            get { return _ManualList; }
            set
            {
                _ManualList = value;
                OnPropertyChanged();
            }
        }
        private Models.Attendance _SelectedManual;

        public Models.Attendance SelectedManual
        {
            get { return _SelectedManual; }
            set
            {
                _SelectedManual = value;
                OnPropertyChanged();
                SelectedValue();


            }
        }

        private Visibility _AddAttendanceVisibility;

        public Visibility AddAttendanceVisibility
        {
            get { return _AddAttendanceVisibility; }
            set
            {
                _AddAttendanceVisibility = value;
                OnPropertyChanged();
            }
        }
        private Visibility _AttendaceMonitoringVisibility;

        public Visibility AttendaceMonitoringVisibility
        {
            get { return _AttendaceMonitoringVisibility; }
            set
            {
                _AttendaceMonitoringVisibility = value;
                OnPropertyChanged();
            }
        }

        private void SelectedValue()
        {
            if (SelectedManual != null)
            {
                DateTime.TryParse(SelectedManual.TIMEIN, out DateTime timeInResult);
                DateTime.TryParse(SelectedManual.LOGDATE, out DateTime logDate);

                var selectedManual = Employees.Where(x => x.EmployeeNo == SelectedManual.STUDENTID).FirstOrDefault();

                SelectedDateManual = logDate;
                TimeFrom = timeInResult;
                if (!string.IsNullOrEmpty(SelectedManual.TIMEOUT))
                {
                    DateTime.TryParse(SelectedManual.TIMEOUT, out DateTime timeOutResult);
                    TimeTo = timeOutResult;
                }
                else
                {
                    TimeTo = null;
                }
                SelectedEmployeeManual = selectedManual;
            }
        }

        private void ResetValues()
        {
            SelectedEmployeeManual = null;
            TimeFrom = null;
            TimeTo = null;
            SelectedDateManual = null;
        }

        private void DeleteRecordEvent()
        {
            if (SelectedManual == null)
            {
                MessageBox.Show("Unable to delete record!");
                return;
            }

            var messageBoxResult = MessageBox.Show("Do you want to delete this record?", "Delete Records", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                using (var context = new Database.PayrollDbContext())
                {
                    var removedData = context.Attendance.Where(x => x.ID == SelectedManual.ID).FirstOrDefault();
                    context.Attendance.Remove(removedData);
                    context.SaveChanges();

                    MessageBox.Show("Deleted Successfully");
                    ManualList = new ObservableCollection<Models.Attendance>(GetEmployees2());
                    SelectedManual = null;
                }
            }
        }

#endregion
    }
}
