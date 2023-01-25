using DevExpress.Mvvm;
using Microsoft.Win32;
using PayrollSystem.Database;
using PayrollSystem.Misc;
using PayrollSystem.Models;
using PayrollSystem.Views;
using PayrollSystem.Windows;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PayrollSystem.ViewModels
{
    class MainViewModel : Abstracts.RaisePropertyChanged
    {
        private string _WindowsTitle;

        public string WindowsTitle
        {
            get { return _WindowsTitle; }
            set { _WindowsTitle = value;
                OnPropertyChanged();
            }
        }
        private string _ClockTimer;

        public string ClockTimer
        {
            get { return _ClockTimer; }
            set
            {
                _ClockTimer = value;
                OnPropertyChanged(ClockTimer);
            }
        }



        #region Employee Section
        public AsyncCommand OnSaveEmployeeCommand { get; set; }
        public AsyncCommand OnDeleteEmployeeCommand { get; set; }
        public DelegateCommand BrowsePictureCommand { get; set; }
        public DelegateCommand<string> PrintEmployeeRecordCommand { get; set; }

        public DelegateCommand OpenProfileCommand { get; set; }

        #endregion

        #region Qr Section 
        public AsyncCommand OnGenerateQrCodeCommandAsync { get; set; }
        public AsyncCommand OnSaveQrCommandAsync { get; set; }

        #endregion

        #region "Properties"

        private bool _IsFieldEnabled;

        public bool IsFieldEnabled
        {
            get { return _IsFieldEnabled; }
            set { _IsFieldEnabled = value;
                OnPropertyChanged();
            }
        }



        private string _SearchEmployeeText;

        public string SearchEmployeeText
        {
            get { return _SearchEmployeeText; }
            set
            {
                _SearchEmployeeText = value;
                OnPropertyChanged();
                SearchEmployee(SearchEmployeeText);
            }
        }


        private string _VersionUI;

        public string VersionUI
        {
            get { return _VersionUI; }
            set
            {
                _VersionUI = value;
                OnPropertyChanged();
            }
        }

        #region Employee Section

        private ObservableCollection<string> _GenderCollection;

        public ObservableCollection<string> GenderCollection
        {
            get { return _GenderCollection; }
            set
            {
                _GenderCollection = value;
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


        private object _CurrentView;

        public object CurrentView
        {
            get { return _CurrentView; }
            set
            {
                _CurrentView = value;
                OnPropertyChanged();
            }
        }
        private string _Tabtitle;

        public string Tabtitle
        {
            get { return _Tabtitle; }
            set
            {
                _Tabtitle = value;
                OnPropertyChanged();
            }
        }


        private string _EmployeeNo;

        public string EmployeeNo
        {
            get { return _EmployeeNo; }
            set
            {
                _EmployeeNo = value;
                OnPropertyChanged();
            }
        }


        private string _Firstname;

        public string Firstname
        {
            get { return _Firstname; }
            set
            {
                _Firstname = value;
                OnPropertyChanged();
            }
        }

        private string _Lastname;

        public string Lastname
        {
            get { return _Lastname; }
            set
            {
                _Lastname = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _Birthdate;

        public DateTime? Birthdate
        {
            get { return _Birthdate; }
            set
            {
                _Birthdate = value;
                OnPropertyChanged();
            }
        }

        private string _Position;

        public string Position
        {
            get { return _Position; }
            set
            {
                _Position = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _HiringDate;

        public DateTime? HiringDate
        {
            get { return _HiringDate; }
            set
            {
                _HiringDate = value;
                OnPropertyChanged();
            }
        }

        private string _Gender;

        public string Gender
        {
            get { return _Gender; }
            set
            {
                _Gender = value;
                OnPropertyChanged();
            }
        }


        private string _Status;

        public string Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                OnPropertyChanged();
            }
        }

        private string _Address;

        public string Address
        {
            get { return _Address; }
            set
            {
                _Address = value;
                OnPropertyChanged();
            }
        }
        private string _Allowance;

        public string Allowance
        {
            get { return _Allowance; }
            set
            {
                _Allowance = value;
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

        private string _EmployeeSaveButtonText;

        public string EmployeeSaveButtonText
        {
            get { return _EmployeeSaveButtonText; }
            set
            {
                _EmployeeSaveButtonText = value;
                OnPropertyChanged();
            }
        }

        private string _EmployeePicturePath;

        public string EmployeePicturePath
        {
            get { return _EmployeePicturePath; }
            set
            {
                _EmployeePicturePath = value;
                OnPropertyChanged();
                if (EmployeePicturePath == null)
                {
                    return;
                }
            }
        }

        private string _MedicalAllowance;

        public string MedicalAllowance
        {
            get { return _MedicalAllowance; }
            set
            {
                _MedicalAllowance = value;
                OnPropertyChanged();
            }
        }
        private string _DentalAllowance;

        public string DentalAllowance
        {
            get { return _DentalAllowance; }
            set
            {
                _DentalAllowance = value;
                OnPropertyChanged();
            }
        }

        private string _VisionAllowance;

        public string VisionAllowance
        {
            get { return _VisionAllowance; }
            set
            {
                _VisionAllowance = value;
                OnPropertyChanged();
            }
        }
        private string _TranspoAllowance;

        public string TranspoAllowance
        {
            get { return _TranspoAllowance; }
            set
            {
                _TranspoAllowance = value;
                OnPropertyChanged();
            }
        }

        private byte[] _EmployeePictureSource;

        public byte[] EmployeePictureSource
        {
            get { return _EmployeePictureSource; }
            set
            {
                _EmployeePictureSource = value;
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
                if (SelectedEmployee == null)
                {
                    return;
                }

                EmployeeSaveButtonText = "UPDATE";
                EmployeeNo = SelectedEmployee.EmployeeNo;
                Firstname = SelectedEmployee.Firstname;
                Lastname = SelectedEmployee.Lastname;
                Gender = SelectedEmployee.Gender;
                Address = SelectedEmployee.Address;
                Birthdate = SelectedEmployee.Birthdate;
                Position = SelectedEmployee.Position;
                HiringDate = SelectedEmployee.HiringDate;
                BasicPay = SelectedEmployee.BasicPay;
                SssNumber = SelectedEmployee.SssNumber;
                AccountNo = SelectedEmployee.AccountNo;
                Philhealth = SelectedEmployee.Philhealth;
                Pagibig = SelectedEmployee.Pagibig;
                Status = SelectedEmployee.Status;
                TinNo = SelectedEmployee.TinNo;
                MedicalAllowance = SelectedEmployee.MedicalAllowance;
                DentalAllowance = SelectedEmployee.DentalAllowance;
                TranspoAllowance = SelectedEmployee.TranspoAllowance;
                VisionAllowance = SelectedEmployee.VisionAllowance;
                Allowance = SelectedEmployee.Allowance;
                EmployeePicturePath = SelectedEmployee.EmployeePicturePath;
                Department = SelectedEmployee.Department;
                IsEmployeeNoEnabled = false;


                if (!File.Exists(SelectedEmployee.EmployeePicturePath))
                {

                    EmployeePictureSource = new Bitmap("logo.PNG").ToByteArray(ImageFormat.Png);
                }
                else
                {
                    EmployeePictureSource = new Bitmap(SelectedEmployee.EmployeePicturePath).ToByteArray(ImageFormat.Png);
                }

            }
        }

        private int _Harf;

        public int Harf
        {
            get { return _Harf; }
            set
            {
                _Harf = value;
                OnPropertyChanged();
            }
        }


        private bool _IsEmployeeNoEnabled;

        public bool IsEmployeeNoEnabled
        {
            get { return _IsEmployeeNoEnabled; }
            set
            {
                _IsEmployeeNoEnabled = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Models.DashboardMenu> _DashboardMenu;

        public ObservableCollection<Models.DashboardMenu> DashboardMenu
        {
            get { return _DashboardMenu; }
            set
            {
                _DashboardMenu = value;
                OnPropertyChanged();
            }
        }

        private Models.DashboardMenu _SelectedMenu;

        public Models.DashboardMenu SelectedMenu
        {
            get { return _SelectedMenu; }
            set
            {
                _SelectedMenu = value;
                OnPropertyChanged();
                if (SelectedMenu == null)
                {
                    return;
                }

                ClearEmployeeData();
                
            }
        }

        private double _BasicPay;

        public double BasicPay
        {
            get { return _BasicPay; }
            set
            {
                _BasicPay = value;
                OnPropertyChanged();
            }
        }

        private string _SssNumber;

        public string SssNumber
        {
            get { return _SssNumber; }
            set
            {
                _SssNumber = value;
                OnPropertyChanged();
            }
        }

        private string _AccountNo;

        public string AccountNo
        {
            get { return _AccountNo; }
            set
            {
                _AccountNo = value;
                OnPropertyChanged();
            }
        }


        private string _Pagibig;

        public string Pagibig
        {
            get { return _Pagibig; }
            set
            {
                _Pagibig = value;
                OnPropertyChanged();
            }
        }


        private string _TinNo;

        public string TinNo
        {
            get { return _TinNo; }
            set
            {
                _TinNo = value;
                OnPropertyChanged();
            }
        }
        private string _Philhealth;

        public string Philhealth
        {
            get { return _Philhealth; }
            set
            {
                _Philhealth = value;
                OnPropertyChanged();
            }

        }
        private string _Username;

        public string Username
        {
            get { return _Username; }
            set
            {
                _Username = value;
                OnPropertyChanged();
            }
        }




        #endregion


        #endregion

        private ObservableCollection<string> _Departments;

        public ObservableCollection<string> Departments
        {
            get { return _Departments; }
            set { _Departments = value;
                OnPropertyChanged();
            }
        }
        private string _Department;

        public string Department
        {
            get { return _Department; }
            set { _Department = value;
                OnPropertyChanged();
            }
        }



        public DelegateCommand LogoutCommand { get; set; }

        public DispatcherTimer _timer;
        public AsyncCommand<SelectionChangedEventArgs> DashboardSelectionCommandAsync { get; set; }
        public MainViewModel()
        {
            _timer = new DispatcherTimer(DispatcherPriority.Render);
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (sender, args) => {
                ClockTimer = $"©IJK Apex Phil Corporation @2022, Date/Time:{DateTime.UtcNow.ToLongDateString()}";
            };
            _timer.Start();

            new LeaveExtensions();
            WindowsTitle = "IJK APEX [HRIS]";

            if (UserAccount.UserType == "Employee" || UserAccount.UserType == "Manager" || UserAccount.UserType == "AccountsPayable") 
            {
                CurrentView = new Views.Profiles.UserProfileUI();
                //CurrentView = new EmployeeUI();
            }
            else
            {
                CurrentView = new HomeUI();
            }

            //CurrentView = UserAccount.UserType != "Employee"  ? new HomeUI() : new EmployeeUI();
            Tabtitle = "Employee Management ";
            VersionUI = Version;

            #region Employee Section
            OnSaveEmployeeCommand = new AsyncCommand(OnSaveEmployeeEvent);
            OnDeleteEmployeeCommand = new AsyncCommand(OnDeleteEmployeeEvent);
            BrowsePictureCommand = new DelegateCommand(BrowsePictureEvent);
            LogoutCommand = new DelegateCommand(LogoutEvent);
            OpenProfileCommand = new DelegateCommand(OpenProfileEvent);

            GenderCollection = new ObservableCollection<string>() {
            "Male",
            "Female"
            };
            StatusCollection = new ObservableCollection<string>()
            {
             "Monthly",
             "Daily",
             "Managerial",
             "Resign",
             "NoPayroll"
             //"For Last Pay"
            };
            Departments = new ObservableCollection<string>() {
                "Admin",
                "Accounting",
                "Sales and Marketing",
                "Technical and After Sales",
                "Warehouse and Logistics"
            };

            EmployeeSaveButtonText = "SAVE";

            using (var context = new PayrollDbContext())
            {
                if (UserAccount.UserType == "Employee" || UserAccount.UserType == "Manager")
                {
                    Employees = new ObservableCollection<Models.Employee>(context.Employees.Where(x => x.EmployeeNo == UserAccount.UserId));
                }
                else
                {
                    Employees = new ObservableCollection<Models.Employee>(context.Employees);
                }
            }

            IsEmployeeNoEnabled = true;

            if (UserAccount != null)
            {
                IsFieldEnabled = UserAccount.UserType == "Employee" ? false : true;

                Username = UserAccount.Username;

                var dashboards = new DashboardHelper().DashboardMenus(UserAccount.UserType);
                DashboardMenu = new ObservableCollection<DashboardMenu>(dashboards);
            }


            #endregion

            PrintEmployeeRecordCommand = new DelegateCommand<string>(PrintEmployeeRecordEvent);
            DashboardSelectionCommandAsync = new AsyncCommand<SelectionChangedEventArgs>(DashboardSelectionEvent);
            DashboardVisibility = Visibility.Hidden;
        }

        private Visibility _DashboardVisibility;

        public Visibility DashboardVisibility
        {
            get { return _DashboardVisibility; }
            set { _DashboardVisibility = value;
                OnPropertyChanged();
            }
        }



        private async Task DashboardSelectionEvent(SelectionChangedEventArgs obj)
        {
            var item = obj.AddedItems[0] as DashboardMenu;
            await Task.Run(() => { 
            DashboardVisibility = Visibility.Visible;
            switch (item.Title)
            {
                case "Home":
                        Application.Current.Dispatcher.Invoke((Action)delegate {
                            CurrentView = new HomeUI();
                            Tabtitle = "Dashboard";
                        });
                       
                    break;
                case "Employees":
                        Application.Current.Dispatcher.Invoke((Action)delegate {
                            CurrentView = new EmployeeUI();
                            Tabtitle = "Employee Management";
                        });
                    break;
                case "QR Code":
                        Application.Current.Dispatcher.Invoke((Action)delegate {
                            CurrentView = new QrCodeUI();
                            Tabtitle = "QR Code Management";
                        });
                    break;
                case "Attendance":
                        Application.Current.Dispatcher.Invoke((Action)delegate {
                            CurrentView = new AttendanceUI();
                            Tabtitle = "Attendance List";
                        });
                    break;
                case "Payroll":
                        Application.Current.Dispatcher.Invoke((Action)delegate {
                            CurrentView = new PayrollUI();
                            Tabtitle = "Payroll Management";
                        });
                    break;
                case "Cut-Offs":
                        Application.Current.Dispatcher.Invoke((Action)delegate {
                            CurrentView = new CutOffUI();
                            Tabtitle = "Cut-Offs";
                        });
                    break;
                case "Payslip":
                        Application.Current.Dispatcher.Invoke((Action)delegate {
                            CurrentView = new PayslipUI();
                            Tabtitle = "Payslip";
                        });
                    break;
                case "Cash Advance":
                        Application.Current.Dispatcher.Invoke((Action)delegate {
                            CurrentView = new CashAdvanceUI();
                            Tabtitle = "Cash Advance";
                        });
                    break;
                case "Leave":
                        Application.Current.Dispatcher.Invoke((Action)delegate {
                            CurrentView = new LeaveUI();
                            Tabtitle = "Leaves";
                        });
                    break;
                case "13th Month Pay":
                        Application.Current.Dispatcher.Invoke((Action)delegate {
                            CurrentView = new ThirteenMonthUI();
                            Tabtitle = "13th Month Pay";
                        });
                    break;
                case "User Accounts":
                        Application.Current.Dispatcher.Invoke((Action)delegate {
                            CurrentView = new UserAccountUI();
                            Tabtitle = "User Accounts";
                        });
                    break;
                case "Overtime":
                        Application.Current.Dispatcher.Invoke((Action)delegate {
                            CurrentView = new OverTimeUI();
                            Tabtitle = "Overtime";
                        });
                    break;
                case "Settings":
                        Application.Current.Dispatcher.Invoke((Action)delegate {
                            CurrentView = new Views.Settings.SettingsUI();
                            Tabtitle = "Settings";
                        });
                    break;
                    case "Loans":
                        Application.Current.Dispatcher.Invoke((Action)delegate {
                            CurrentView = new Views.LoansUI();
                            Tabtitle = "Loans";
                        });
                        break;
                }
            WindowsTitle = $"{WindowsName}{Tabtitle}]";
            });
            DashboardVisibility = Visibility.Hidden;
        }


        private void OpenProfileEvent()
        {
            CurrentView = new Views.Profiles.UserProfileUI();
            SelectedMenu = null;
        }

        private void PrintEmployeeRecordEvent(string employeeNo)
        {
            using (var context = new PayrollDbContext())
            {
                var employee = context.Employees.Where(x => x.EmployeeNo.Equals(employeeNo)).FirstOrDefault();

                var path = @"\\SERVER\Users\Public\Payroll Templates\Employee";
                var savingPath = $"{path}\\{employee.Lastname} {employee.Firstname} - Record.xlsx";
                using (SLDocument sLDocument = new SLDocument($"{path}\\employee.xlsx", "Sheet1"))
                {
                    sLDocument.SetCellValue("C7", employee.EmployeeNo);
                    sLDocument.SetCellValue("C9", employee.Lastname);
                    sLDocument.SetCellValue("C10", employee.Firstname);
                    sLDocument.SetCellValue("C11", employee.Address);
                    sLDocument.SetCellValue("C12", employee.Gender);
                    sLDocument.SetCellValue("C13", employee.DisplayBirthDate);
                    sLDocument.SetCellValue("C14", employee.Position);
                    sLDocument.SetCellValue("C15", employee.DisplayHiringDate);
                    sLDocument.SetCellValue("C16", employee.Status);
                    sLDocument.SetCellValue("C17", employee.BasicPay);
                    sLDocument.SetCellValue("F9", employee.SssNumber);
                    sLDocument.SetCellValue("F10", employee.Philhealth);
                    sLDocument.SetCellValue("F11", employee.Pagibig);
                    sLDocument.SetCellValue("F12", employee.TinNo);
                    sLDocument.SetCellValue("F13", employee.AccountNo);
                    sLDocument.SetCellValue("F14", employee.MedicalAllowance);
                    sLDocument.SetCellValue("F15", employee.DentalAllowance);
                    sLDocument.SetCellValue("F16", employee.VisionAllowance);
                    sLDocument.SetCellValue("F17", employee.TranspoAllowance);

                    sLDocument.SaveAs(savingPath);
                }

                System.Diagnostics.Process.Start(savingPath);
            }
        }

        private void LogoutEvent()
        {

            var mainWindow = (MainWindow)Application.Current.Windows
            .OfType<Window>().FirstOrDefault(x => x.IsActive);
            LoginWindow loginWindow = new LoginWindow();

            loginWindow.Show();
            if (loginWindow != null) mainWindow.Close();

        }

        private void BrowsePictureEvent()
        {
            var dialogResult = new OpenFileDialog()
            {
                Filter = "Image Files(*.png,*.jpg)|*.png;*.jpg;"
            };

            if (dialogResult.ShowDialog() != null)
            {
                var result = dialogResult.FileNames.FirstOrDefault();

                if (result == null) return;

                EmployeePicturePath = result;

                EmployeePictureSource = new Bitmap(EmployeePicturePath).ToByteArray(ImageFormat.Png);
            }
        }

        #region Employee Section
        private void SearchEmployee(string searchText)
        {
            if (UserAccount.UserType != "Employee")
            {
                using (var context = new PayrollDbContext())
                {

                    var result = context.Employees.Where(x =>
                    x.EmployeeNo.Contains(searchText)
                    || x.Firstname.Contains(searchText)
                    || x.Lastname.Contains(searchText)
                    || x.Address.Contains(searchText)
                    || x.Position.Contains(searchText)
                    );
                    Employees = new ObservableCollection<Models.Employee>(result);
                }
            }

        }
        private async Task OnDeleteEmployeeEvent()
        {
            using (var context = new PayrollDbContext())
            {
                if (SelectedEmployee != null)
                {
                    var id = context.Employees.Where(x => x.EmployeeNo == SelectedEmployee.EmployeeNo).FirstOrDefault();
                    context.Employees.Remove(id);
                    await context.SaveChangesAsync();
                    Employees = new ObservableCollection<Models.Employee>(context.Employees);
                    ClearEmployeeData();
                }


            }
        }

        private Models.Employee GetEmployee()
        {
            return new Models.Employee
            {
                EmployeeNo = EmployeeNo,
                Firstname = Firstname,
                Lastname = Lastname,
                Gender = Gender,
                Address = Address,
                Birthdate = Birthdate,
                Position = Position,
                HiringDate = HiringDate,
                BasicPay = BasicPay,
                SssNumber = SssNumber,
                AccountNo = AccountNo,
                Philhealth = Philhealth,
                Pagibig = Pagibig,
                TinNo = TinNo,
                Allowance = Allowance,
                TranspoAllowance = TranspoAllowance,
                DentalAllowance = DentalAllowance,
                VisionAllowance = VisionAllowance,
                MedicalAllowance = MedicalAllowance,
                Status = Status,
                EmployeePicturePath = EmployeePicturePath,
                Department = Department
            };

        }
        private void ClearEmployeeData()
        {


            EmployeeNo = null;
            Firstname = null;
            Lastname = null;
            Gender = null;
            Address = null;
            Birthdate = null;
            Position = null;
            HiringDate = null;
            BasicPay = 0;
            SelectedEmployee = null;
            SssNumber = null;
            AccountNo = null;
            Philhealth = null;
            Pagibig = null;
            Status = null;
            TinNo = null;
            TranspoAllowance = null;
            VisionAllowance = null;
            Allowance = null;
            DentalAllowance = null;
            MedicalAllowance = null;
            EmployeeSaveButtonText = "SAVE";
            IsEmployeeNoEnabled = true;
            EmployeePicturePath = EmployeePicturePath;
            EmployeePictureSource = null;
            Department = null;

        }

        private async Task OnSaveEmployeeEvent()
        {
            if (string.IsNullOrEmpty(EmployeeNo) || string.IsNullOrWhiteSpace(EmployeeNo))
            {
                MessageBox.Show("Unable to save records");
                return;
            }

            using (var context = new PayrollDbContext())
            {
                if (SelectedEmployee == null)
                {
                    context.Employees.Add(GetEmployee());
                    await context.SaveChangesAsync();
                    ClearEmployeeData();
                    MessageBox.Show("Save Successfully");
                    if (UserAccount.UserType == "Employee")
                    {
                        Employees = new ObservableCollection<Models.Employee>(context.Employees.Where(x => x.EmployeeNo == UserAccount.UserId));
                    }
                    else
                    {
                        Employees = new ObservableCollection<Models.Employee>(context.Employees);
                    }

                }
                else
                {
                    var updatedEmployee = GetEmployee();
                    var selectedEmployee = context.Employees.Where(x => x.EmployeeNo == updatedEmployee.EmployeeNo).FirstOrDefault();
                    selectedEmployee.Firstname = updatedEmployee.Firstname;
                    selectedEmployee.Lastname = updatedEmployee.Lastname;
                    selectedEmployee.Gender = updatedEmployee.Gender;
                    selectedEmployee.Address = updatedEmployee.Address;
                    selectedEmployee.Birthdate = updatedEmployee.Birthdate;
                    selectedEmployee.Position = updatedEmployee.Position;
                    selectedEmployee.HiringDate = updatedEmployee.HiringDate;
                    selectedEmployee.BasicPay = updatedEmployee.BasicPay;
                    selectedEmployee.SssNumber = updatedEmployee.SssNumber;
                    selectedEmployee.AccountNo = updatedEmployee.AccountNo;
                    selectedEmployee.Philhealth = updatedEmployee.Philhealth;
                    selectedEmployee.Pagibig = updatedEmployee.Pagibig;
                    selectedEmployee.TinNo = updatedEmployee.TinNo;
                    selectedEmployee.Allowance = updatedEmployee.Allowance;
                    selectedEmployee.MedicalAllowance = updatedEmployee.MedicalAllowance;
                    selectedEmployee.DentalAllowance = updatedEmployee.DentalAllowance;
                    selectedEmployee.TranspoAllowance = updatedEmployee.TranspoAllowance;
                    selectedEmployee.VisionAllowance = updatedEmployee.VisionAllowance;
                    selectedEmployee.Status = updatedEmployee.Status;
                    selectedEmployee.EmployeePicturePath = updatedEmployee.EmployeePicturePath;
                    selectedEmployee.Department = updatedEmployee.Department;

                    await context.SaveChangesAsync();
                    ClearEmployeeData();
                    if (UserAccount.UserType == "Employee")
                    {
                        Employees = new ObservableCollection<Models.Employee>(context.Employees.Where(x => x.EmployeeNo == UserAccount.UserId));
                    }
                    else
                    {
                        Employees = new ObservableCollection<Models.Employee>(context.Employees);
                    }
                    MessageBox.Show("Updated Successfully");
                    EmployeeSaveButtonText = "SAVE";
                }
            }
        }

        #endregion


    }
}
