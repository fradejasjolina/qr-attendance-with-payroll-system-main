using DevExpress.Mvvm;
using PayrollSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PayrollSystem.ViewModels
{
    class CashAdvanceViewModel : Abstracts.RaisePropertyChanged
    {

        public DelegateCommand SaveCashAdvanceCommand { get; set; }
        public DelegateCommand DeleteCashAdvanceCommand { get; set; }
        public AsyncCommand FileCashAdvanceCommand { get; set; }
        public DelegateCommand <int> DeleteRecordCommand { get; set; }


        #region List Cash Advance Properties

        private ObservableCollection<Models.Employee> _EmployeeNames;

        public ObservableCollection<Models.Employee> EmployeeNames
        {
            get { return _EmployeeNames; }
            set
            {
                _EmployeeNames = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<dynamic> _CashAdvances;

        public ObservableCollection<dynamic> CashAdvances
        {
            get { return _CashAdvances; }
            set
            {
                _CashAdvances = value;
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

        private dynamic _SelectedCashAdvance;

        public dynamic SelectedCashAdvance
        {
            get { return _SelectedCashAdvance; }
            set
            {
                _SelectedCashAdvance = value;
                OnPropertyChanged();
                if (SelectedCashAdvance != null)
                {
                    var result = EmployeeNames.FirstOrDefault(x => x.EmployeeNo == SelectedCashAdvance.EmployeeNo);
                    SelectedEmployee = result;
                    Amount = SelectedCashAdvance.Amount;
                }

            }
        }

        private decimal _Amount;

        public decimal Amount
        {
            get { return _Amount; }
            set
            {
                _Amount = value;
                OnPropertyChanged();
            }
        }

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
        #endregion



        private ObservableCollection<Models.Employee> _EmployeeList;

        public ObservableCollection<Models.Employee> EmployeeList
        {
            get { return _EmployeeList; }
            set
            {
                _EmployeeList = value;
                OnPropertyChanged();
            }
        }

        private Employee _SelectedEmployeeFiling;

        public Employee SelectedEmployeeFiling
        {
            get { return _SelectedEmployeeFiling; }
            set
            {
                _SelectedEmployeeFiling = value;
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

        private decimal _FilingAmount;

        public decimal FilingAmount
        {
            get { return _FilingAmount; }
            set
            {
                _FilingAmount = value;
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
            }
        }

        private ObservableCollection<Models.FilingCashAdvance> _FilingCashAdvancesCollection;

        public ObservableCollection<Models.FilingCashAdvance> FilingCashAdvancesCollection
        {
            get { return _FilingCashAdvancesCollection; }
            set
            {
                _FilingCashAdvancesCollection = value;
                OnPropertyChanged();
            }
        }

        private FilingCashAdvance _FilingCashAdvancesSelected;

        public FilingCashAdvance FilingCashAdvancesSelected
        {
            get { return _FilingCashAdvancesSelected; }
            set
            {
                _FilingCashAdvancesSelected = value;
                OnPropertyChanged();
                if (FilingCashAdvancesSelected != null)
                {
                    if (UserAccount.UserType == "Employee")
                    {
                        MessageBox.Show("Unable to edit the record.");
                        FilingCashAdvancesSelected = null;
                        return;
                    }
                    if (FilingCashAdvancesSelected.Status =="Approved" && UserAccount.UserType !="Administrator" && UserAccount.UserType != "Accounting")
                    {
                        MessageBox.Show("Unable to edit the record.");
                        return;
                    }

                    var employee = EmployeeList.Where(x => x.EmployeeNo == FilingCashAdvancesSelected.EmployeeNo).FirstOrDefault();
                    SelectedEmployeeFiling = employee;
                    FilingAmount = FilingCashAdvancesSelected.Amount;
                    SelectedStatus = FilingCashAdvancesSelected.Status;
                }

            }
        }

        private Visibility _ListCashAdvanceVisibility;

        public Visibility ListCashAdvanceVisibility
        {
            get { return _ListCashAdvanceVisibility; }
            set { _ListCashAdvanceVisibility = value;
                OnPropertyChanged();
            }
        }

        private string _RemainingBalanceText;

        public string RemainingBalanceText
        {
            get { return _RemainingBalanceText; }
            set { _RemainingBalanceText = value;
                OnPropertyChanged();
            }
        }


        private List<Models.Employee> GetEmployees(string userType)
        {
            using (var context = new  Database.PayrollDbContext())
            {
                switch (userType)
                {
                    case "Administrator":
                    case "Accounting":
                    case "AccountsPayable":
                       return context.Employees.Where(x => x.Status != "Resign").ToList();
                    case "Manager":
                    case "Employee":
                        return context.Employees.Where(x => x.EmployeeNo == UserAccount.UserId).ToList();
                    
                        //var userDepartment = context.Employees.Where(x => x.EmployeeNo == UserAccount.UserId).FirstOrDefault();
                        //return context.Employees.Where(x=>x.Department == userDepartment.Department && x.Status != "Resign").ToList();

                }
            }
            return new List<Employee>();
        }


        public CashAdvanceViewModel()
        {
            #region List Cash Advance Constructor
            EmployeeNames = new ObservableCollection<Models.Employee>(GetEmployees(UserAccount.UserType));

            SaveCashAdvanceCommand = new DelegateCommand(SaveCashAdvanceEvent);
            DeleteCashAdvanceCommand = new DelegateCommand(DeleteCashAdvanceEvent);
            GenerateCashAdvances2();
            DeleteRecordCommand = new DelegateCommand<int>(DeleteRecordEvent);
            #endregion

            EmployeeList = new ObservableCollection<Employee>(GetEmployees(UserAccount.UserType));
            SetStatusCollection(UserAccount.UserType);
            FileCashAdvanceCommand = new AsyncCommand(FileCashAdvanceEvent);
            GenerateCashAdvances(UserAccount.UserType);
            ShowTabControl(UserAccount.UserType);
            GetCurrentBalance(UserAccount.UserId);
            CashAdvancesTracker = new ObservableCollection<dynamic>(GetCashAdvanceTracker());
        }

        private void GenerateCashAdvances2()
        {
            using (var payrollDbContext = new PayrollSystem.Database.PayrollDbContext())
            {
                CashAdvances = new ObservableCollection<dynamic>();

                switch (UserAccount.UserType)
                {
                    case "Administrator":
                    case "Accounting":
                    case "AccountsPayable":
                        foreach (var item in payrollDbContext.CashAdvances)
                        {
                            var dynamicValue = new
                            {
                                Id = item.Id,
                                EmployeeNo = item.EmployeeNo,
                                FullName = item.Fullname,
                                Amount = item.Amount,
                                RemainingBalance = item.RemainingBalance,
                                DateAdded = item.DateAdded.ToShortDateString()
                            };
                            CashAdvances.Add(dynamicValue);
                        }
                        break;

                    case "Employee":
                        foreach (var item in payrollDbContext.CashAdvances.Where(x => x.EmployeeNo == UserAccount.UserId))
                        {
                            var dynamicValue = new
                            {
                                Id = item.Id,
                                EmployeeNo = item.EmployeeNo,
                                FullName = item.Fullname,
                                Amount = item.Amount,
                                RemainingBalance = item.RemainingBalance,
                                DateAdded = item.DateAdded.ToShortDateString()
                            };
                            CashAdvances.Add(dynamicValue);
                        }
                        break;



                }
            }
        }

        #region List Cash Advance Function
        private void DeleteCashAdvanceEvent()
        {
            if (SelectedCashAdvance != null)
            {
                using (var payrollDbContext = new PayrollSystem.Database.PayrollDbContext())
                {
                    var selected = new CashAdvanceModel
                    {
                        EmployeeNo = SelectedCashAdvance.EmployeeNo
                    };


                    var y = payrollDbContext.CashAdvances.Where(x => x.EmployeeNo == selected.EmployeeNo).FirstOrDefault();
                    var z = payrollDbContext.CashAdvanceTracker.Where(x => x.EmployeeNo == selected.EmployeeNo).FirstOrDefault();
                    if (y != null)
                    {
                        payrollDbContext.CashAdvances.Remove(y);
                        if (z != null)
                        {
                            payrollDbContext.CashAdvanceTracker.Remove(z);
                        }

                        payrollDbContext.SaveChanges();
                    }

                    CashAdvances = new ObservableCollection<dynamic>();

                    foreach (var item in payrollDbContext.CashAdvances)
                    {
                        var dynamicValue = new
                        {
                            Id = item.Id,
                            EmployeeNo = item.EmployeeNo,
                            FullName = item.Fullname,
                            Amount = item.Amount,
                            RemainingBalance = item.RemainingBalance,
                            DateAdded = item.DateAdded.ToShortDateString()
                        };
                        CashAdvances.Add(dynamicValue);
                    }

                }
            }
        }

        private void SaveCashAdvanceEvent()

        {

            using (var payrollDbContext = new PayrollSystem.Database.PayrollDbContext())
            {
                if (UserAccount.UserType != "Employee")
                {
                    var value = GetCashAdvanceModel();
                    if (value.Id == 0)
                    {
                        payrollDbContext.CashAdvances.Add(value);
                        payrollDbContext.SaveChanges();
                        MessageBox.Show("Added Successfully");
                    }
                    else
                    {
                        var dbValue = payrollDbContext.CashAdvances.FirstOrDefault(x => x.Id == value.Id);
                        dbValue.EmployeeNo = value.EmployeeNo;
                        dbValue.Fullname = value.Fullname;
                        dbValue.Amount = value.Amount;
                        dbValue.RemainingBalance = value.Amount;

                        payrollDbContext.SaveChanges();
                        MessageBox.Show("Modifed Successfully");
                    }

                    CashAdvances = new ObservableCollection<dynamic>();
                    foreach (var item in payrollDbContext.CashAdvances)
                    {
                        var dynamicValue = new
                        {
                            Id = item.Id,
                            EmployeeNo = item.EmployeeNo,
                            FullName = item.Fullname,
                            Amount = item.Amount,
                            RemainingBalance = item.RemainingBalance,
                            DateAdded = item.DateAdded.ToShortDateString()
                        };
                        CashAdvances.Add(dynamicValue);
                    }

                }
            }

        }
        private void DeleteRecordEvent(int id)
        {
            //if (SelectedCashAdvance == null)
            //{
            //    MessageBox.Show("Unable to delete record!");
            //    return;
            //}

            var messageBoxResult = MessageBox.Show("Do you want to delete this record?", "Delete Records", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                using (var context = new Database.PayrollDbContext())
                {
                    var removedData = context.FilingCashAdvances.Where(x => x.Id == id).FirstOrDefault();
                    context.FilingCashAdvances.Remove(removedData);
                    context.SaveChanges();

                    MessageBox.Show("Deleted Successfully");
                    SelectedCashAdvance = null;

                    FilingCashAdvancesCollection = new ObservableCollection<FilingCashAdvance>(context.FilingCashAdvances);
                    ResetValues();
                    SetStatusCollection(UserAccount.UserType);
                    GenerateCashAdvances(UserAccount.UserType);
                    GenerateCashAdvances2();    
                }


            }
        }
        private Models.CashAdvanceModel GetCashAdvanceModel()
        {

            return new Models.CashAdvanceModel
            {
                Id = SelectedCashAdvance == null ? 0 : SelectedCashAdvance.Id,
                Fullname = SelectedEmployee.Fullname,
                EmployeeNo = SelectedEmployee.EmployeeNo,
                Amount = Amount,
                RemainingBalance = Amount,
                DateAdded = DateTime.Now
            };
        }

        #endregion

        private async Task FileCashAdvanceEvent()
        {
            var validated = true;
            if(SelectedEmployeeFiling == null || FilingAmount == 0)
            {
                validated = false;
            }

            if (!validated)
            {
                MessageBox.Show("Please fill up first");
                return;
            }

            var savingRecord = GetFilingCashAdvance();

            var messageBoxRecord =
                $"Employee No:{savingRecord.EmployeeNo}\nEmployee Name:{savingRecord.EmployeeName}" +
                $"\nAmount:{savingRecord.Amount}\nDate Filed:{savingRecord.DateFiled}\nStatus:{savingRecord.Status}";

            var messageBoxResult = MessageBox.Show(messageBoxRecord,"Verify Records",MessageBoxButton.OKCancel,MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.OK)
            {
                using (var context = new Database.PayrollDbContext())
                {

                    if (savingRecord.Id == 0)
                    {
                        context.FilingCashAdvances.Add(savingRecord);
                        await context.SaveChangesAsync();
                        MessageBox.Show("Save Successfully!");
                    }
                    else
                    {
                        var selectedRecord = context.FilingCashAdvances.Where(x => x.Id == savingRecord.Id).FirstOrDefault();
                        if (selectedRecord != null)
                        {
                            selectedRecord.Amount = savingRecord.Amount;
                            selectedRecord.Status = savingRecord.Status;
                            if (savingRecord.Status == "Approved")
                            {
                                var cashAdvance = context.CashAdvances.Where(x => x.EmployeeNo == selectedRecord.EmployeeNo).FirstOrDefault();
                                if (cashAdvance != null)
                                {
                                    cashAdvance.Amount += selectedRecord.Amount;
                                    cashAdvance.RemainingBalance += selectedRecord.Amount;
                                }
                                else
                                {
                                    var newCashAdvance = new CashAdvanceModel()
                                    {
                                        EmployeeNo = savingRecord.EmployeeNo,
                                        Fullname = savingRecord.EmployeeName,
                                        Amount = savingRecord.Amount,
                                        RemainingBalance = savingRecord.Amount,
                                        DateAdded = DateTime.Now,
                                    };

                                    context.CashAdvances.Add(newCashAdvance);
                                }

                            }

                        }
                        context.SaveChanges();

                        MessageBox.Show("Filing Successfully!");


                    }

                    FilingCashAdvancesCollection = new ObservableCollection<FilingCashAdvance>(context.FilingCashAdvances);
                    ResetValues();
                    SetStatusCollection(UserAccount.UserType);
                    GenerateCashAdvances(UserAccount.UserType);
                    GenerateCashAdvances2();




                }
            }
        }

        private FilingCashAdvance GetFilingCashAdvance()
        {
            var cashAdvance = new FilingCashAdvance()
            {
                EmployeeNo = SelectedEmployeeFiling.EmployeeNo,
                EmployeeName = SelectedEmployeeFiling.Fullname,
                Amount = FilingAmount,
                DateFiled = DateTime.Now,
                Status = SelectedStatus
            };


            if (FilingCashAdvancesSelected != null)
            {
                cashAdvance.Id = FilingCashAdvancesSelected.Id;
            }

            return cashAdvance;
        }

        private void ResetValues()
        {
            SelectedEmployeeFiling = null;
            FilingAmount = 0;
            SelectedStatus = "Awaiting Approval";
        }

        private void GenerateCashAdvances(string userType)
        {
            using (var context = new Database.PayrollDbContext())
            {
                FilingCashAdvancesCollection = new ObservableCollection<FilingCashAdvance>();
                switch (userType)
                {
                    case "Administrator":
                    case "Accounting":
                    case "AccountsPayable":

                        FilingCashAdvancesCollection = new ObservableCollection<FilingCashAdvance>(context.FilingCashAdvances);
                        break;
                    case "Manager":
                    case "Employee":
                        FilingCashAdvancesCollection = new ObservableCollection<FilingCashAdvance>(context.FilingCashAdvances.Where(x=>x.EmployeeNo == UserAccount.UserId));
                        break;
                }
            }
        }





        private void GetValuesPerManager(string departmentName)
        {
            using (var context = new Database.PayrollDbContext())
            {
                var users = context.Employees.Where(x => x.Department == departmentName).ToList();
                foreach (var item in users)
                {
                    var cashAdvanceFiling = context.FilingCashAdvances.Where(x => x.EmployeeNo == item.EmployeeNo).FirstOrDefault();
                    if (cashAdvanceFiling == null) continue;
                    FilingCashAdvancesCollection.Add(cashAdvanceFiling);
                }
            }
        }

        private void SetStatusCollection(string userType)
        {
            switch (userType)
            {
                case "Administrator":
                    StatusCollection = new ObservableCollection<string>() {
                        "Awaiting Approval",
                        "Approved",
                        "Not Approved"
                    };
                    SelectedStatus = "Awaiting Approval";
                    break;
                case "Manager":
                case "Employee":
                case "Accounting":
                case "AccountsPayable":
                    StatusCollection = new ObservableCollection<string>() {
                        "Awaiting Approval"
                    };
                    SelectedStatus = "Awaiting Approval";
                    break;
            }

        }


        private void ShowTabControl(string userType)
        {
            ListCashAdvanceVisibility = Visibility.Hidden;
            switch (userType)
            {
                case "Administrator":
                case "AccountsPayable":
                case "Accounting":
                    ListCashAdvanceVisibility = Visibility.Visible;
                    break;
            }
        }

        private void GetCurrentBalance(string employeeNo)
        {
            using (var context = new Database.PayrollDbContext())
            {
                var result = context.CashAdvances.Where(x => x.EmployeeNo == employeeNo).FirstOrDefault();

                var remainingValue = result == null ? 0 : result.RemainingBalance;

                RemainingBalanceText = $"Remaining Balance:{remainingValue}";

            }
        }


        private List<dynamic> GetSearchResult()
        {
            var list = new List<dynamic>();

            using (var payrollDbContext = new PayrollSystem.Database.PayrollDbContext())
            {

                switch (UserAccount.UserType)
                {
                    case "Administrator":
                    case "Accounting":
                    case "AccountsPayable":
                        foreach (var item in payrollDbContext.CashAdvances)
                        {
                            var dynamicValue = new
                            {
                                Id = item.Id,
                                EmployeeNo = item.EmployeeNo,
                                FullName = item.Fullname,
                                Amount = item.Amount,
                                RemainingBalance = item.RemainingBalance,
                                DateAdded = item.DateAdded.ToShortDateString()
                            };
                            list.Add(dynamicValue);
                        }
                        break;

                    case "Employee":
                        foreach (var item in payrollDbContext.CashAdvances.Where(x => x.EmployeeNo == UserAccount.UserId))
                        {
                            var dynamicValue = new
                            {
                                Id = item.Id,
                                EmployeeNo = item.EmployeeNo,
                                FullName = item.Fullname,
                                Amount = item.Amount,
                                RemainingBalance = item.RemainingBalance,
                                DateAdded = item.DateAdded.ToShortDateString()
                            };
                            list.Add(dynamicValue);
                        }
                        break;
                }
            }
            return list;
        }


        private string _TextSearch;

        public string TextSearch
        {
            get { return _TextSearch; }
            set { _TextSearch = value;
                OnPropertyChanged();
                SearchList(TextSearch);

            }
        }
        private void SearchList(string searchText)
        {
            var list = GetSearchResult().Where(x=>x.EmployeeNo.Contains(searchText) || x.FullName.ToLower().Contains(searchText)).ToList();
            CashAdvances = new ObservableCollection<dynamic>(list);

        }


        private ObservableCollection<dynamic> _CashAdvancesTracker;

        public ObservableCollection<dynamic> CashAdvancesTracker
        {
            get { return _CashAdvancesTracker; }
            set { _CashAdvancesTracker = value;
                OnPropertyChanged();
            }
        }

        private List<dynamic> GetCashAdvanceTracker()
        {
            var list = new List<dynamic>();
            using (var context = new Database.PayrollDbContext())
            {
                foreach (var item in context.CashAdvanceTracker)
                {
                    var payrollDate = GetCashAdvanceTracker(item.PayrollId);
                    var details = GetEmployeeDetails(item.EmployeeNo);

                    var value = new {
                        Id = item.Id,
                        EmployeeNo = item.EmployeeNo,
                        FullName = details.Fullname,
                        PayrollDate = $"{payrollDate.DisplayStartDate} - {payrollDate.DisplayEndDate}",
                        Amount = item.Amount

                    };
                    list.Add(value);
                }
            }

            return list;
        }
        private Models.CutOff GetCashAdvanceTracker(int payrollId)
        {
            using (var context = new Database.PayrollDbContext())
            {
                var payrollDate = context.CutOffs.Where(x => x.Id == payrollId).FirstOrDefault();
                return payrollDate;

            }
        }

        private Models.Employee GetEmployeeDetails(string employeeNo)
        {
            using (var context = new Database.PayrollDbContext())
            {
                var details = context.Employees.Where(x => x.EmployeeNo == employeeNo).FirstOrDefault();
                return details;

            }
        }


        private string _TrackerSearch;

        public string TrackerSearch
        {
            get { return _TrackerSearch; }
            set { _TrackerSearch = value;
                OnPropertyChanged();
                SearchTracker();
            }
        }

        private void SearchTracker()
        {
            var result = GetCashAdvanceTracker();
            var list = result.Where(x =>x.EmployeeNo.Contains(TrackerSearch) || x.FullName.ToLower().Contains(TrackerSearch)).ToList();
            CashAdvancesTracker = new ObservableCollection<dynamic>(list);
        }


    }


  
}

