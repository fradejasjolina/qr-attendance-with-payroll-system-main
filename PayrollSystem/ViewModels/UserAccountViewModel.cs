using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PayrollSystem.ViewModels
{
    class UserAccountViewModel : Abstracts.RaisePropertyChanged
    {
        public DelegateCommand<object[]> CreateUserAccount { get; set; }
        public DelegateCommand<int> DeleteRecordCommand { get; set; }
        public DelegateCommand<int> ActivateUserCommand { get; set; }


        #region Properties

        private ObservableCollection<Models.UserAccount> _UserAccounts;

        public ObservableCollection<Models.UserAccount> UserAccounts
        {
            get { return  _UserAccounts; }
            set {  _UserAccounts = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Models.UserAccount> _InactiveUserAccounts;

        public ObservableCollection<Models.UserAccount> InactiveUserAccounts
        {
            get { return _InactiveUserAccounts; }
            set
            {
                _InactiveUserAccounts = value;
                OnPropertyChanged();
            }
        }



        private ObservableCollection<string> _UserTypes;

        public ObservableCollection<string> UserTypes
        {
            get { return _UserTypes; }
            set { _UserTypes = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Models.Employee> _Employees;

        public ObservableCollection<Models.Employee> Employees
        {
            get { return _Employees; }
            set { _Employees = value;
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

        private string _Username;

        public string Username
        {
            get { return _Username; }
            set { _Username = value;
                OnPropertyChanged();
            }
        }

        private string _SelectedUserType;

        public string SelectedUserType
        {
            get { return _SelectedUserType; }
            set { _SelectedUserType = value;
                OnPropertyChanged();
            }
        }



        #endregion


        public UserAccountViewModel()
        {
            Employees = new ObservableCollection<Models.Employee>(GetEmployees());
            UserAccounts = new ObservableCollection<Models.UserAccount>(GetUserAccounts(true));
            InactiveUserAccounts = new ObservableCollection<Models.UserAccount>(GetUserAccounts(false));

            UserTypes = new ObservableCollection<string> { 
                "Administrator",
                "HR",
                "Accounting",
                "Employee",
                "Manager",
                "AccountsPayable"
            };
            CreateUserAccount = new DelegateCommand<object[]>(CreateUserAccountEvent);
            DeleteRecordCommand = new DelegateCommand<int>(DeleteRecordEvent);
            ActivateUserCommand = new DelegateCommand<int>(ActivateUserEvent);
        }
        private void DeleteRecordEvent(int id)
        {
            using (var context = new Database.PayrollDbContext())
            {
                var result = MessageBox.Show("Do you want to Inactivate this record?","Inactivating of User Accounts",MessageBoxButton.OKCancel,MessageBoxImage.Warning);
                if(result == MessageBoxResult.OK)
                {
                    var userAccounts = context.UserAccounts.Where(x => x.Id == id).FirstOrDefault();
                    userAccounts.IsEnabled = false;
                    
                    //context.UserAccounts.Remove(userAccounts);
                    context.SaveChanges();
                    MessageBox.Show("Inactivate Sucessfully!");
                    UserAccounts = new ObservableCollection<Models.UserAccount>(GetUserAccounts(true));
                    InactiveUserAccounts = new ObservableCollection<Models.UserAccount>(GetUserAccounts(false));
                }
            }
        }

        private void ActivateUserEvent(int id)
        {
            using (var context = new Database.PayrollDbContext())
            {
                var result = MessageBox.Show("Do you want to active this user?", "Activation of User Accounts", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.OK)
                {
                    var userAccounts = context.UserAccounts.Where(x => x.Id == id).FirstOrDefault();
                    userAccounts.IsEnabled = true;

                    //context.UserAccounts.Remove(userAccounts);
                    context.SaveChanges();
                    MessageBox.Show("Activation Sucessfully!");
                    UserAccounts = new ObservableCollection<Models.UserAccount>(GetUserAccounts(true));
                    InactiveUserAccounts = new ObservableCollection<Models.UserAccount>(GetUserAccounts(false));

                }
            }
        }





        private void CreateUserAccountEvent(object[] obj)
        {
            if (SelectedEmployee == null) return;


            var password = ((PasswordBox)obj[0]).Password;

            using (var context = new Database.PayrollDbContext())
            {
                var userAccount = new Models.UserAccount {
                    UserId = SelectedEmployee.EmployeeNo,
                   Username  = Username,
                    Password = CreateMD5(password),
                    UserType = SelectedUserType
                };

                context.UserAccounts.Add(userAccount);
                context.SaveChanges();
                System.Windows.MessageBox.Show("Save Successfully!");
                UserAccounts = new ObservableCollection<Models.UserAccount>(GetUserAccounts(true));
                SelectedEmployee = null;
                Username = null;
                password = null;
                SelectedUserType = null;
            }
        }

        #region Methods
        private List<Models.UserAccount> GetUserAccounts(bool isEnabled)
        {
            using (var context = new Database.PayrollDbContext())
            {
                //return context.UserAccounts.ToList();
                var list = new List<Models.UserAccount>();
                foreach (var item in context.UserAccounts.Where(x=>x.IsEnabled == isEnabled))
                {
                    Employees.ToList().ForEach(x => Console.WriteLine(x.EmployeeNo));
                    var employeeName = Employees.Where(x=>x.EmployeeNo == item.UserId).FirstOrDefault();

                    var models = new Models.UserAccount
                    {
                        Id = item.Id,
                        UserId = item.UserId,
                        Username = item.Username,
                        Password = item.Password,
                        UserType = item.UserType,
                        Fullname = employeeName != null ? employeeName.Fullname : ""

                    };
                    list.Add(models);
                }

                return list;
            }
        }
        private List<Models.Employee> GetEmployees()
        {
            using (var context = new Database.PayrollDbContext())
            {
                return context.Employees.ToList();
            }


        }




        public string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public string Decrypt(string input)
        {
            try
            {
                var encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecodeByte = Convert.FromBase64String(input);
                int charCount = utf8Decode.GetCharCount(todecodeByte, 0, todecodeByte.Length);
                char[] decodedChar = new char[charCount];
                utf8Decode.GetChars(todecodeByte, 0, todecodeByte.Length, decodedChar, 0);
                string result = new String(decodedChar);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Decode" + ex.Message);
            }
        }
        #endregion
    }
}
