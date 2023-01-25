using DevExpress.Mvvm;
using PayrollSystem.Misc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PayrollSystem.ViewModels
{
     class LeaveViewModel : Abstracts.RaisePropertyChanged
    {
        public DelegateCommand <int> DeleteRecordCommand { get; set; }

        #region Properties
        private string _SearchText;

        public string SearchText
        {
            get { return _SearchText; }
            set { _SearchText = value;
                OnPropertyChanged();
               SearchList(SearchText);
            }
        }


        private ObservableCollection<Models.LeaveModel> _LeavesCollection;

        public ObservableCollection<Models.LeaveModel> LeavesCollection
        {
            get { return _LeavesCollection; }
            set
            {
                _LeavesCollection = value;
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

        private Models.Employee _EmployeeLeaveSelected;

        public Models.Employee EmployeeLeaveSelected
        {
            get { return _EmployeeLeaveSelected; }
            set { _EmployeeLeaveSelected = value;
                OnPropertyChanged();

                if(EmployeeLeaveSelected != null)
                {
                    using (var context = new Database.PayrollDbContext())
                    {
                        var result = context.EmployeeLeaves.DistinctBy(x => x.EmployeeNo)
                            .Where(y => y.EmployeeNo == EmployeeLeaveSelected.EmployeeNo);

                        EmployeeLeaves = result == null ? new ObservableCollection<Models.EmployeeLeave>()
                            : new ObservableCollection<Models.EmployeeLeave>(result.ToList());

                    }
                }

            }
        }

        private ObservableCollection<string> _LeaveTypes;

        public ObservableCollection<string> LeaveTypes
        {
            get { return _LeaveTypes; }
            set
            {
                _LeaveTypes = value;
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

        private ObservableCollection<string> _Departments;

        public ObservableCollection<string> Departments
        {
            get { return _Departments; }
            set
            {
                _Departments = value;
                OnPropertyChanged();
            }
        }


        private string _Department;

        public string Department
        {
            get { return _Department; }
            set
            {
                _Department = value;
                OnPropertyChanged();
            }
        }


        private DateTime? _DateFiled;

        public DateTime? DateFiled
        {
            get { return _DateFiled; }
            set
            {
                _DateFiled = value;
                OnPropertyChanged();
            }
        }

        private string _SelectedLeave;

        public string SelectedLeave
        {
            get { return _SelectedLeave; }
            set
            {
                _SelectedLeave = value;
                OnPropertyChanged();
            }
        }

        private string _Remarks;

        public string Remarks
        {
            get { return _Remarks; }
            set
            {
                _Remarks = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Models.EmployeeLeave> _EmployeeLeaves;

        public ObservableCollection<Models.EmployeeLeave> EmployeeLeaves
        {
            get { return _EmployeeLeaves; }
            set
            {
                _EmployeeLeaves = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _LeaveStatusCollection;

        public ObservableCollection<string> LeaveStatusCollection
        {
            get { return _LeaveStatusCollection; }
            set
            {
                _LeaveStatusCollection = value;
                OnPropertyChanged();
            }
        }
        private string _SelectedLeaveStatus;

        public string SelectedLeaveStatus
        {
            get { return _SelectedLeaveStatus; }
            set { _SelectedLeaveStatus = value;
                OnPropertyChanged();
            }
        }

        private string _ButtonText;

        public string ButtonText
        {
            get { return _ButtonText; }
            set
            {
                _ButtonText = value;
                OnPropertyChanged();
            }
        }

        private Models.LeaveModel _SelectedEmployeeLeave;

        public Models.LeaveModel SelectedEmployeeLeave
        {
            get { return _SelectedEmployeeLeave; }
            set { _SelectedEmployeeLeave = value;
                OnPropertyChanged();
                if(SelectedEmployeeLeave != null)
                {
                    ResetFields();
                    
                    if (SelectedEmployeeLeave.Status == "Approved" )
                    {
                        MessageBox.Show("This value can't be modify");
                        ButtonText = "Save Leave";
                        return;
                    }
                    if (UserAccount.UserType == "Employee")
                    {
                        MessageBox.Show("You don't have the right to access record");
                        ButtonText = "Save Leave";
                        return;
                    }


                    if (UserAccount.UserType == "Manager" || UserAccount.UserType == "Administrator")
                    {
                        LeaveStatusCollection = new ObservableCollection<string>() {
                        "Awaiting Approval",
                        "Approved",
                        "Not Approved"
                        };
                    }
                    else
                    {
                        LeaveStatusCollection = new ObservableCollection<string>() { "Awaiting Approval" };
                    }

                    var employee = Employees.Where(x => x.EmployeeNo == SelectedEmployeeLeave.EmployeeNo).FirstOrDefault();
                    SelectedEmployee = employee;
                    Department = SelectedEmployeeLeave.Department;
                    DateFiled = SelectedEmployeeLeave.DateFiled;
                    SelectedLeave = SelectedEmployeeLeave.LeaveType;
                    Remarks = SelectedEmployeeLeave.Remarks;
                    SelectedLeaveStatus = SelectedEmployeeLeave.Status;

                    ButtonText = "Update Leave";
                }
            }
        }

        #endregion


        #region Commands
        public DelegateCommand SaveLeaveCommand { get; set; }
        #endregion

        #region Constructor
        public LeaveViewModel()
        {
            LeavesCollection = new ObservableCollection<Models.LeaveModel>(GetLeaveModels());
            EmployeeLeaves = new ObservableCollection<Models.EmployeeLeave>(GetEmployeeLeaves());
            Employees = new ObservableCollection<Models.Employee>(GetEmployees());
            DeleteRecordCommand = new DelegateCommand<int>(DeleteRecordEvent);

            LeaveTypes = new ObservableCollection<string>(){
                "Leave with pay",
                "Leave without pay",  
                "Birthday Leave",
                "Half day Leave with pay",
                "Half day Leave without pay",
                "Sick Leave with Pay",
                "Sick Leave without Pay"
                //"Personal Leave with Pay",
                //"Personal Leave without Pay"
            };
            Departments = new ObservableCollection<string>() {
                "Admin",
                "Accounting",
                "Sales and Marketing",
                "Technical and After Sales",
                "Warehouse and Logistics"
            };
            if (UserAccount.UserType == "Administrator" || UserAccount.UserType == "Accounting")
            {
                LeaveStatusCollection = new ObservableCollection<string>() {
                "Awaiting Approval",
                "Approved",
                "Not Approved"
                };
            }
            else
            {
                LeaveStatusCollection = new ObservableCollection<string>() {
                "Awaiting Approval"
                };
            }
            SelectedLeaveStatus = "Awaiting Approval";
            ButtonText = "Save Leave";


            SaveLeaveCommand = new DelegateCommand(SaveLeaveEvent);
        }
        #endregion

        #region Methods

        private void SearchList(string searchText)
        {
           
            var list = GetLeaveModels()
                                        .Where(x =>
                                                    x.EmployeeNo.Contains(searchText) ||
                                                    x.Fullname.ToLower().Contains(searchText) ||
                                                    x.DisplayDateFiled.Contains(searchText)
                                              ).ToList();
            LeavesCollection = new ObservableCollection<Models.LeaveModel>(list);
        }

        private void SaveLeaveEvent()
        {
            if (!IsValidated())
            {
                MessageBox.Show("Please fill up the record first!");
                return;
            }
            var leave = GetLeave();

            var msg = $"Employee No:{leave.EmployeeNo}\nEmployee Name:{leave.Fullname}\n" +
                $"Department:{leave.Department}\nDate Filed:{leave.DateFiled}\n" +
                $"Leave Type:{leave.LeaveType}\nReasons:{leave.Remarks}\nStatus:{leave.Status}";

            var msgBoxResult = MessageBox.Show(msg, "Verify Records", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (msgBoxResult == MessageBoxResult.OK)
            {
                using (var context = new Database.PayrollDbContext())
                {
                    if (ButtonText == "Save Leave")
                    {
                        context.Leaves.Add(leave);
                        context.SaveChanges();
                        MessageBox.Show("Save Successfully");

                        #region Code for validation of dates
                        //var hasTheSameDate = context.Leaves.Where(x => x.EmployeeNo == leave.EmployeeNo && x.DateFiled == leave.DateFiled).FirstOrDefault();
                        //if (hasTheSameDate == null)
                        //{
                        //    context.Leaves.Add(leave);
                        //    context.SaveChanges();
                        //    MessageBox.Show("Save Successfully");
                        //}
                        //else
                        //{
                        //    MessageBox.Show("The current date is already field");
                        //    return;
                        //}
                        #endregion
                    }
                    else
                    {
                        if (SelectedEmployeeLeave != null)
                        {
                            var employeeId = context.Leaves.Where(y => y.Id == SelectedEmployeeLeave.Id).FirstOrDefault();
                            if (employeeId != null)
                            {
                                var leaves = GetLeave();

                                employeeId.EmployeeNo = leave.EmployeeNo;
                                employeeId.Fullname = leave.Fullname;
                                employeeId.Department = leave.Department;
                                employeeId.DateFiled = leave.DateFiled;
                                employeeId.LeaveType = leave.LeaveType;
                                employeeId.Remarks = leave.Remarks;
                                employeeId.Status = leave.Status;

                                if (leave.Status == "Approved")
                                {
                                    var employeeLeave = context.EmployeeLeaves.Where(x => x.EmployeeNo == leave.EmployeeNo).FirstOrDefault();
                                    if (employeeLeave == null) return;

                                    switch (leave.LeaveType)
                                    {
                                        case "Leave with pay":
                                            employeeLeave.CurrentLeave -= 1;
                                            context.SaveChanges();
                                            break;
                                        case "Half day Leave with pay":
                                            employeeLeave.CurrentLeave -= 0.5;
                                            context.SaveChanges();
                                            break;
                                        case "Leave without pay":
                                            employeeLeave.CurrentLeave -= 0;
                                            context.SaveChanges();
                                            break;
                                        case "Birthday Leave":
                                            employeeLeave.CurrentLeave -= 0;
                                            context.SaveChanges();
                                            break;
                                        case "Half day Leave without pay":
                                            employeeLeave.CurrentLeave -= 0;
                                            context.SaveChanges();
                                            break;
                                        case "Sick Leave with Pay":
                                            employeeLeave.CurrentLeave -= 1;
                                            context.SaveChanges();
                                            break;
                                        case "Sick Leave without Pay":
                                            employeeLeave.CurrentLeave -= 0;
                                            context.SaveChanges();
                                            break;
                                        //case "Personal Leave with Pay":
                                        //    employeeLeave.CurrentLeave -= 1;
                                        //    context.SaveChanges();
                                        //    break;
                                        //case "Personal Leave without Pay":
                                        //    employeeLeave.CurrentLeave -= 0;
                                        //    context.SaveChanges();
                                        //    break;

                                    }

                                    MessageBox.Show("Updated Successfully!");

                                }

                            }


                            Console.WriteLine();

                        }
                    }
                    LeavesCollection = new ObservableCollection<Models.LeaveModel>();
                    LeavesCollection = new ObservableCollection<Models.LeaveModel>(GetLeaveModels());
                    EmployeeLeaves = new ObservableCollection<Models.EmployeeLeave>();
                    EmployeeLeaves = new ObservableCollection<Models.EmployeeLeave>(GetEmployeeLeaves());
                    ResetFields();
                    ButtonText = "Save Leave";

                    switch (UserAccount.UserType)
                    {

                        case "Administrator":
                        case "Accounting":
                            LeaveStatusCollection = new ObservableCollection<string>() { "Awaiting Approval", "Approved", "Not Approved" };
                            break;
                        case "Manager":
                        case "Employee":
                            LeaveStatusCollection = new ObservableCollection<string>() { "Awaiting Approval" };
                            break;

                    }



                }
            }
        }
        private void DeleteRecordEvent(int id)
        {
            //if (SelectedLeave == null)
            //{
            //    MessageBox.Show("Unable to delete record!");
            //    return;
            //}

            var messageBoxResult = MessageBox.Show("Do you want to delete this record?", "Delete Records", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                using (var context = new Database.PayrollDbContext())
                {
                    var removedData = context.Leaves.Where(x => x.Id == id).FirstOrDefault();
                    context.Leaves.Remove(removedData);
                    context.SaveChanges();

                    MessageBox.Show("Deleted Successfully");
                    SelectedLeave = null;
                }


                LeavesCollection = new ObservableCollection<Models.LeaveModel>();
                LeavesCollection = new ObservableCollection<Models.LeaveModel>(GetLeaveModels());
                EmployeeLeaves = new ObservableCollection<Models.EmployeeLeave>();
                EmployeeLeaves = new ObservableCollection<Models.EmployeeLeave>(GetEmployeeLeaves());
                ResetFields();
                ButtonText = "Save Leave";

                switch (UserAccount.UserType)
                {

                    case "Administrator":
                    case "Accounting":
                        LeaveStatusCollection = new ObservableCollection<string>() { "Awaiting Approval", "Approved", "Not Approved" };
                        break;
                    case "Manager":
                    case "Employee":
                        LeaveStatusCollection = new ObservableCollection<string>() { "Awaiting Approval" };
                        break;

                }

            }
        }

        private List<Models.Employee> GetEmployees()
        {
            List<Models.Employee> employees = new List<Models.Employee>();

            using (var context = new Database.PayrollDbContext())
            {
                if(UserAccount.UserType == "Administrator" || UserAccount.UserType == "Accounting")
                {
                    employees = context.Employees.Where(x => x.Status != "Resign").ToList();
                }
                else if (UserAccount.UserType == "Manager")
                {
                    var department = context.Employees.Where(x => x.EmployeeNo == UserAccount.UserId).FirstOrDefault().Department;

                    employees = context.Employees.Where(x => x.Department == department).ToList();

                }
                else
                {
                    //employee account
                    employees = context.Employees.Where(x => x.EmployeeNo == UserAccount.UserId).ToList();
                }
                
            }
            return employees;
        }
        private List<Models.LeaveModel> GetLeaveModels()
        {
            var leaves = new List<Models.LeaveModel>();
            using (var context = new Database.PayrollDbContext())
            {
                var userAccount = context.Employees.Where(x => x.EmployeeNo == UserAccount.UserId).FirstOrDefault().Department;

                if (UserAccount.UserType == "Administrator" || UserAccount.UserType == "Accounting")
                {
                    leaves = context.Leaves.ToList();
                }
                else if (UserAccount.UserType == "Manager")
                {
                    leaves = context.Leaves.Where(x => x.Department == userAccount).ToList();
                }
                else
                {
                    leaves = context.Leaves.Where(x => x.EmployeeNo == UserAccount.UserId).ToList();
                }

            }
            return leaves;
        }
        private List<Models.EmployeeLeave> GetEmployeeLeaves()
        {
            var list = new List<Models.EmployeeLeave>();
            using (var context = new Database.PayrollDbContext())
            {
                if(UserAccount.UserType =="Administrator"  || UserAccount.UserType =="Accounting" )
                {
                    list = context.EmployeeLeaves.DistinctBy(x => x.EmployeeNo).ToList();
                }
                else
                {
                    list = context.EmployeeLeaves.DistinctBy(x => x.EmployeeNo).Where(x => x.EmployeeNo == UserAccount.UserId).ToList();
                }

                var tempList = new List<Models.EmployeeLeave>();
                foreach (var item in list)
                {
                    var isEmployeeResign = context.Employees.Where(x => x.EmployeeNo == item.EmployeeNo).FirstOrDefault();
                    if (isEmployeeResign.Status == "Resign" || isEmployeeResign.Status == "NoPayroll")
                    {
                        continue;
                    }
                    tempList.Add(item);
                }
                list = tempList;

                /*
                foreach (var item in context.Employees)
                {
                    if (item.Status == "Resign") continue;


                    var leaveCount = 0;no
                    var years = DateTime.Now.Year - ((DateTime)item.HiringDate).Year;
                    if (years == 0)
                    {
                        leaveCount = 0;
                    }

                    if (years == 1)
                    {
                        leaveCount = 5;
                    }

                    else if (years == 2)
                    {
                        leaveCount = 5;
                    }
                    else if (years >= 3 && years <= 4Z1
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

                    ////var hasLeaveCount = context.EmployeeLeaves.Where(x => x.EmployeeNo == item.EmployeeNo).FirstOrDefault();

                    var model = new Models.EmployeeLeave
                    {
                        DateHired = (DateTime)item.HiringDate,
                        EmployeeNo = item.EmployeeNo,
                        Fullname = item.Fullname,
                        LeaveCount = leaveCount,
                        YearsOfService = years,
                        CurrentLeave = leaveCount
                    };

                    context.EmployeeLeaves.Add(model);
                    list.Add(model);
                }
                context.SaveChanges(); */

                return list;
            }

        }  
        private bool IsValidated()
        {
            if(SelectedEmployee == null || string.IsNullOrEmpty(Department) || string.IsNullOrWhiteSpace(Department) ||string.IsNullOrEmpty(Remarks) || string.IsNullOrWhiteSpace(Remarks) || DateFiled == null)
            {
                return false;
            }

            return true;
        }
        private Models.LeaveModel GetLeave()
        {
            return new Models.LeaveModel
            {
                EmployeeNo = SelectedEmployee.EmployeeNo,
                Fullname = SelectedEmployee.Fullname,
                Department = Department,
                DateFiled = (DateTime)DateFiled,
                LeaveType = SelectedLeave,
                Remarks = Remarks,
                Status = SelectedLeaveStatus

            };
        }
        private void ResetFields()
        {
            SelectedEmployee = null;
            Department = null;
            DateFiled = null;
            SelectedLeave = null;
            Remarks = string.Empty;
            SelectedLeaveStatus = "Awaiting Approval";
            ButtonText = "Save Leave";

        }
        private bool IsLeaveExsits(string employeeNo)
        {
            using (var context = new Database.PayrollDbContext())
            {
                var result = context.EmployeeLeaves.Where(x => x.EmployeeNo == employeeNo).FirstOrDefault();
                return result != null;
            }
        }
        #endregion


    }
}
