using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PayrollSystem.ViewModels
{
    class OverTimeViewModel:Abstracts.RaisePropertyChanged
    {

        #region Commands
        public DelegateCommand FileOverTimeCommand{ get; set; }
        public DelegateCommand SearchOvertimeCommand { get; set; }
        public DelegateCommand<int> DeleteRecordCommand { get; set; }
        #endregion

        #region Properties
        private string _Fullname;

        public string Fullname
        {
            get { return _Fullname; }
            set { _Fullname = value;
                OnPropertyChanged();
            }
        }

        private DateTime _SelectedDate;

        public DateTime SelectedDate
        {
            get { return _SelectedDate; }
            set { _SelectedDate = value;
                OnPropertyChanged();
            }
        }


        private DateTime _OtFrom;

        public DateTime OtFrom
        {
            get { return _OtFrom; }
            set { _OtFrom = value;
                OnPropertyChanged();
            }
        }

        private DateTime _OtTo;

        public DateTime OtTo
        {
            get { return _OtTo; }
            set { _OtTo = value;
                OnPropertyChanged();
            }
        }

        private string _Reason;

        public string Reason
        {
            get { return _Reason; }
            set { _Reason = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Models.Employee> _EmployeeListing;

        public ObservableCollection<Models.Employee> EmployeeListing
        {
            get { return _EmployeeListing; }
            set { _EmployeeListing = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _StatusCollection;

        public ObservableCollection<string> StatusCollection
        {
            get { return _StatusCollection; }
            set { _StatusCollection = value; }
        }

        private string _SelectedStatus;

        public string SelectedStatus
        {
            get { return _SelectedStatus; }
            set { _SelectedStatus = value;
                OnPropertyChanged();
            }
        }

        private Models.Employee _SelectedEmployee;

        public Models.Employee SelectedEmployee
        {
            get { return _SelectedEmployee; }
            set { _SelectedEmployee = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Models.Overtime> _OvertimeCollection;

        public ObservableCollection<Models.Overtime> OvertimeCollection
        {
            get { return _OvertimeCollection; }
            set { _OvertimeCollection = value;
                OnPropertyChanged();
            }
        }


        private Models.Overtime _SelectedOvertime;

        public Models.Overtime SelectedOvertime
        {
            get { return _SelectedOvertime; }
            set { _SelectedOvertime = value;
                PassValue();
            }
        }




        #endregion

        #region Search Properties
        private DateTime _SearchDateStart;

        public DateTime SearchDateStart
        {
            get { return _SearchDateStart; }
            set { _SearchDateStart = value;
                OnPropertyChanged();
            }
        }

        private DateTime _SearchDateEnd;

        public DateTime SearchDateEnd
        {
            get { return _SearchDateEnd; }
            set { _SearchDateEnd = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Constructor
        public OverTimeViewModel()
        {

            DeleteRecordCommand = new DelegateCommand<int>(DeleteRecordEvent);

            FileOverTimeCommand = new DelegateCommand(FileOverTimeEvent);
            SearchOvertimeCommand = new DelegateCommand(SearchOvertimeEvent);
            SelectedDate = DateTime.Now;
            OtFrom = DateTime.Now;
            OtTo = DateTime.Now;
            SearchDateStart = DateTime.Now;
            SearchDateEnd = DateTime.Now;

            var employees = GetEmployee(UserAccount.UserType);
            EmployeeListing = new ObservableCollection<Models.Employee>(employees);
            SetStatusCollection(UserAccount.UserType);
            OvertimeCollection = new ObservableCollection<Models.Overtime>(GetOvertimes(UserAccount.UserType));

        }

        private void SearchOvertimeEvent()
        {
            var list = GetOvertimes(UserAccount.UserType).Where(x => x.DateOverTime >= SearchDateStart && x.DateOverTime <= SearchDateEnd).ToList();
            OvertimeCollection = new ObservableCollection<Models.Overtime>(list);


        }
        #endregion

        #region Methods
        private void PassValue()
        {
            if (SelectedOvertime == null) return;
            if (UserAccount.UserType == "Employee")
            {
                MessageBox.Show("Unable to modify the data!");
                return;
            }

            SelectedDate = SelectedOvertime.DateOverTime;
            SelectedStatus = SelectedOvertime.Status;
            OtFrom = SelectedOvertime.OvertimeFrom;
            OtTo = SelectedOvertime.OvertimeTo;
            Reason = SelectedOvertime.Reasons;


            using (var context = new Database.PayrollDbContext())
            {
                var user = EmployeeListing.Where(x => x.EmployeeNo == SelectedOvertime.EmployeeNo).FirstOrDefault();
                if (user != null)
                {
                    SelectedEmployee = user;
                }


            }
        }
        private Models.Overtime IsOvertimeExists(string employeeNo)
        {
            using (var context = new Database.PayrollDbContext())
            {
                var isExists = context.Overtimes.Where(x => x.EmployeeNo == employeeNo).FirstOrDefault();
                return isExists;
            }
        }
        private List<Models.Overtime> GetOvertimes(string userType)
        {
            using (var context = new Database.PayrollDbContext())
            {
                switch (userType)
                {
                    case "Administrator":
                    case "Accounting":
                    case "HR":
                        return context.Overtimes.ToList();
                    case "Manager":
                        var userDepartment = context.Employees.Where(x=>x.EmployeeNo == UserAccount.UserId).FirstOrDefault().Department;
                        var list = new List<Models.Overtime>();
                        foreach (var item in context.Employees.Where(x=>x.Department == userDepartment))
                        {
                            var isExists = IsOvertimeExists(item.EmployeeNo);
                            if (isExists != null)
                            {
                                list.Add(isExists);
                            }
                        }
                        return list;
                    case "Employee":
                        return context.Overtimes.Where(x=>x.EmployeeNo == UserAccount.UserId).ToList();
                }
            }
            return new List<Models.Overtime>();
        }
        private void SetStatusCollection(string userType)
        {
            switch (userType)
            {
                case "Administrator":
                case "Manager":
                case "HR":
                    StatusCollection = new ObservableCollection<string>() {
                        "Awaiting Approval",
                        "Approved",
                        "Not Approved"
                    };
                    SelectedStatus = "Awaiting Approval";
                    break;
                case "Employee":
                case "Accounting":
                    StatusCollection = new ObservableCollection<string>() {
                        "Awaiting Approval"
                    };
                    SelectedStatus = "Awaiting Approval";
                    break;

            }

        }
        private void DeleteRecordEvent(int id)
        {
            //if (SelectedOvertime == null)
            //{
            //    MessageBox.Show("Unable to delete record!");
            //    return;
            //}

            var messageBoxResult = MessageBox.Show("Do you want to delete this record?", "Delete Records", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                using (var context = new Database.PayrollDbContext())
                {
                    var removedData = context.Overtimes.Where(x => x.Id == id).FirstOrDefault();
                    context.Overtimes.Remove(removedData);
                    context.SaveChanges();

                    MessageBox.Show("Deleted Successfully");
                    SelectedOvertime = null;

                    ResetFields();
                    OvertimeCollection = new ObservableCollection<Models.Overtime>(GetOvertimes(UserAccount.UserType));
                }
            }
        }

        private void FileOverTimeEvent()
        {
            if (!Validation())
            {
                MessageBox.Show("Please input record first");
                return;
            }


            var overtime = GetOvertime();
            var msg = $"Please check if the following record is correct:\n\nDATE:{overtime.DateOverTime}\nTIME IN:{overtime.OvertimeTo}\nTIME OUT:{overtime.OvertimeFrom}" +
                $"\nREASONS:{overtime.Reasons}\nSTATUS:{overtime.Status}";

            var messageBoxResult = MessageBox.Show(msg,"Verify Records",MessageBoxButton.OKCancel,MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                using (var context = new PayrollSystem.Database.PayrollDbContext())
                {
                    if (overtime.Id == 0)
                    {
                        context.Overtimes.Add(overtime);
                        context.SaveChanges();
                        MessageBox.Show("Save Successfully!");
                    }
                    else
                    {
                        var existing = context.Overtimes.Where(x => x.Id == overtime.Id).FirstOrDefault();
                        if (existing != null)
                        {
                            existing.OvertimeFrom = OtFrom;
                            existing.OvertimeTo = OtTo;
                            existing.DateOverTime = SelectedDate;
                            existing.Reasons = Reason;
                            existing.Status = SelectedStatus;
                            existing.EmployeeNo = SelectedEmployee.EmployeeNo;
                            existing.Fullname = SelectedEmployee.Fullname;
                            context.SaveChanges();
                            MessageBox.Show("Update Successfully!");
                        }
                    }

                    ResetFields();
                    OvertimeCollection = new ObservableCollection<Models.Overtime>(GetOvertimes(UserAccount.UserType));
                }

            }
        }
        private bool Validation()
        {
            if(SelectedEmployee == null || SelectedDate == null ||string.IsNullOrEmpty(Reason) || string.IsNullOrWhiteSpace(Reason))
            {
                return false;
            }
            return true;
        }



        private void ResetFields()
        {
            OtFrom = DateTime.Now;
            OtTo = DateTime.Now;
            SelectedDate = DateTime.Now;
            SelectedStatus = "Awaiting Approval";
            Reason = null;
            SelectedEmployee = null;
            
        }
        private Models.Overtime GetOvertime()
        {
            var result = new Models.Overtime { 
                OvertimeFrom = OtFrom,
                OvertimeTo = OtTo,
                DateOverTime = SelectedDate,
                Reasons = Reason,
                Status = SelectedStatus,
                EmployeeNo = SelectedEmployee.EmployeeNo,
                Fullname = SelectedEmployee.Fullname
            };
            if (SelectedOvertime != null)
            {
                result.Id = SelectedOvertime.Id;
            }
            return result;
        }
        private List<Models.Employee> GetEmployee(string userType)
        {
            using (var context = new Database.PayrollDbContext())
            {
                switch (userType)
                {
                    case "Administrator":
                    case "Accounting":
                    case "HR":
                        return context.Employees.Where(x => x.Status != "Resign").ToList();
                    case "Manager":
                        var userDepartment = context.Employees.Where(x => x.EmployeeNo == UserAccount.UserId).FirstOrDefault().Department;
                        return context.Employees.Where(x => x.Status != "Resign" && x.Department == userDepartment).ToList();
                    case "Employee":
                        return context.Employees.Where(x => x.EmployeeNo == UserAccount.UserId).ToList();
                }
            }
            return null;


        }
        #endregion
    }
}
