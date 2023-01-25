using DevExpress.Mvvm;
using MySql.Data.MySqlClient;
using PayrollSystem.Database;
using PayrollSystem.Misc;
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
    class LoansViewModel:Abstracts.RaisePropertyChanged
    {

        #region Commands
        public DelegateCommand SaveLoanCommand { get; set; }
        public DelegateCommand DeleteLoanCommand { get; set; }
        public DelegateCommand ClearLoanCommand { get; set; }

        public DelegateCommand<int> ViewLoanDetailsCommand { get; set; }


        #endregion

        #region Properties
        private ObservableCollection<LoanViewModel> _Loans;

        public ObservableCollection<LoanViewModel> Loans
        {
            get { return _Loans; }
            set { _Loans = value;
                OnPropertyChanged();
            }
        }


        private LoanViewModel _SelectedLoan;

        public LoanViewModel SelectedLoan
        {
            get { return _SelectedLoan; }
            set { _SelectedLoan = value;
                OnPropertyChanged();
                LoadSelectedItem(SelectedLoan);
            }
        }



        private ObservableCollection<DropdownEmployee> _Employees;

        public ObservableCollection<DropdownEmployee> Employees
        {
            get { return _Employees; }
            set { _Employees = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _LoanTypes;

        public ObservableCollection<string> LoanTypes
        {
            get { return _LoanTypes; }
            set { _LoanTypes = value;
                OnPropertyChanged();
            }
        }


        private Visibility _EditLoan;

        public Visibility EditLoan
        {
            get { return _EditLoan; }
            set { _EditLoan = value;
                OnPropertyChanged();
            }
        }


        #region Fields

        private string _SelectedLoanType;

        public string SelectedLoanType
        {
            get { return _SelectedLoanType; }
            set { _SelectedLoanType = value;
                OnPropertyChanged();
            }
        }

        private DropdownEmployee _SelectedEmployee;

        public DropdownEmployee SelectedEmployee
        {
            get { return _SelectedEmployee; }
            set { _SelectedEmployee = value;
                OnPropertyChanged();
            }
        }


        private int _Terms;

        public int Terms
        {
            get { return _Terms; }
            set { _Terms = value;
                OnPropertyChanged();
            }
        }

        private double? _LoanAmount;

        public double? LoanAmount
        {
            get { return _LoanAmount; }
            set { _LoanAmount = value;
                OnPropertyChanged();
            }
        }


        private DateTime? _StartDate;

        public DateTime? StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _EndDate;

        public DateTime? EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value;
                OnPropertyChanged();
            }
        }

        private double? _MonthlyAmortization;

        public double? MonthlyAmortization
        {
            get { return _MonthlyAmortization; }
            set { _MonthlyAmortization = value;
                OnPropertyChanged();
            }
        }


        #endregion



        private ObservableCollection<LoanDisplayViewModel> _LoanDetails;

        public ObservableCollection<LoanDisplayViewModel> LoanDetails
        {
            get { return _LoanDetails; }
            set { _LoanDetails = value;
                OnPropertyChanged();
            }
        }

        private bool _IsLoanDetailsOpen;

        public bool IsLoanDetailsOpen
        {
            get { return _IsLoanDetailsOpen; }
            set { _IsLoanDetailsOpen = value;
                OnPropertyChanged();
            }
        }


        private string _LoanDetailText;

        public string LoanDetailText
        {
            get { return _LoanDetailText; }
            set { _LoanDetailText = value;
                OnPropertyChanged();
            }
        }



        private Visibility _SavingVisible;

        public Visibility SavingVisible
        {
            get { return _SavingVisible; }
            set { _SavingVisible = value;
                OnPropertyChanged(nameof(SavingVisible));
            }
        }


        #endregion

        #region Constructor

        public LoansViewModel()
        {
            InitializeEvent();
        }


        private void InitializeEvent()
        {
            using (var context = new PayrollDbContext())
            {
                var list = new LoanHelper().GetLoans(UserAccount.UserType,UserAccount.UserId);

               
                Loans = new ObservableCollection<LoanViewModel>(list);
                var employees = context.Database.SqlQuery<DropdownEmployee>("CALL sp_DropdownEmployees()").ToList();
                Employees = new ObservableCollection<DropdownEmployee>(employees);

                LoanTypes = new ObservableCollection<string> {
                    "SSS Salary Loan",
                    "Pagibig Salary Loan",
                    "SSS Calamity Loan",
                    "Pagibig Calamity Loan"
                };
            }

            SaveLoanCommand = new DelegateCommand(SaveLoanEvent);
            DeleteLoanCommand = new DelegateCommand(DeleteSelectedLoanEvent);
            ClearLoanCommand = new DelegateCommand(ClearLoanEvent);
            ViewLoanDetailsCommand = new DelegateCommand<int>(ViewLoanDetailsEvent);
            EditLoan = Visibility.Hidden;
            IsLoanDetailsOpen = false;
            SavingVisible = UserAccount.UserType == "Administrator" ? Visibility.Visible : Visibility.Collapsed;

        }
        #endregion

        #region Events
        private void SaveLoanEvent()
        {

            try
            {
                using (var context = new PayrollDbContext())
                {
                    if (SelectedLoan == null)
                    {
                        context.Loans.Add(new Loan
                        {
                            EmployeeNo = SelectedEmployee.EmployeeNo,
                            Type = SelectedLoanType,
                            Terms = Terms,
                            LoanAmount = LoanAmount,
                            StartDate = StartDate,
                            EndDate = EndDate,
                            MonthlyAmortization = MonthlyAmortization,
                            LoanStatus = true
                        });
                    }
                    else
                    {
                        var loan = context.Loans.Where(x => x.Id == SelectedLoan.Id).FirstOrDefault();
                        if(loan != null)
                        {
                            loan.EmployeeNo = SelectedEmployee.EmployeeNo;
                            loan.Type = SelectedLoanType;
                            loan.Terms = Terms;
                            loan.LoanAmount = LoanAmount;
                            loan.StartDate = StartDate;
                            loan.EndDate = EndDate;
                            loan.MonthlyAmortization = MonthlyAmortization;
                        }
                    }

                    context.SaveChanges();
                    ClearData();

                    InitializeEvent();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message+"\n\n"+ex.StackTrace);
            }
        }

        private void LoadSelectedItem(LoanViewModel loanViewModel)
        {
            if (UserAccount.UserType != "Administrator") return;
            if (loanViewModel == null) return;


            SelectedLoanType = LoanTypes.Where(x => x == loanViewModel.Type).FirstOrDefault();
            LoanAmount = loanViewModel.LoanAmount;
            SelectedEmployee = Employees.Where(x => x.EmployeeNo == loanViewModel.EmployeeNo).FirstOrDefault();
            Terms = loanViewModel.Terms;
            StartDate = loanViewModel.StartDate;
            EndDate = loanViewModel.EndDate;
            MonthlyAmortization = loanViewModel.MonthlyAmortization;
            EditLoan = Visibility.Visible;
        }

        private void ClearData()
        {
            SelectedLoanType = null;
            LoanAmount = 0;
            SelectedEmployee = null;
            Terms = 0;
            StartDate = null ;
            EndDate = null;
            MonthlyAmortization = null;
            SelectedLoan = null;

        }

        private void DeleteSelectedLoanEvent()
        {
            if(SelectedLoan != null)
            {
                using (var context = new PayrollDbContext())
                {
                    var loan = context.Loans.Where(x => x.Id == SelectedLoan.Id).FirstOrDefault();
                    if (loan != null)
                    {
                        context.Loans.Remove(loan);
                        context.SaveChanges();

                        ClearData();
                        InitializeEvent();
                    }
                }
            }
        }

        private void ClearLoanEvent()
        {
            ClearData();
            EditLoan = Visibility.Hidden;
        }

        private void ViewLoanDetailsEvent(int id)
        {
            using (var context = new PayrollDbContext())
            {
                var x = new MySqlParameter
                {
                    ParameterName = "@loanId",
                    Value = id,
                    MySqlDbType = MySqlDbType.Int32
                };
                var result = context.Database.SqlQuery<LoanDisplayViewModel>("CALL sp_checkLoans(@loanId)", x).ToList();
                LoanDetails = new ObservableCollection<LoanDisplayViewModel>(result);
                IsLoanDetailsOpen = true;
                var loanType = context.Loans.Where(y=>y.Id == id).FirstOrDefault();

                LoanDetailText = $"Loan Details - {loanType.Type}";
                
            }
        }

        #endregion

    }

    class DropdownEmployee 
    {
        public string EmployeeNo { get; set; }
        public string Fullname { get; set; }

    }
}
