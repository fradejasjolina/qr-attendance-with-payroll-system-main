using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PayrollSystem.ViewModels
{
    class IndividualAccountVIewModel : Abstracts.RaisePropertyChanged
    {

        public DelegateCommand<object[]> UpdatePasswordCommand { get; set; }
        private string _Username;

        public string Username
        {
            get { return _Username; }
            set { _Username = value;
                OnPropertyChanged();
            }
        }



        public IndividualAccountVIewModel()
        {
            var employeeDetails = GetEmployee(UserAccount.UserId);
            if (employeeDetails != null)
            {
                Details = new ObservableCollection<Models.Employee>(employeeDetails);
            }

            Username = UserAccount.Username;
            UpdatePasswordCommand = new DelegateCommand<object[]>(UpdatePasswordEvent);
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

        private void UpdatePasswordEvent(object[] obj)
        {
            var confirmPassword = ((PasswordBox)obj[0]).Password;
            var newPassword = ((PasswordBox)obj[1]).Password;


            using (var context = new Database.PayrollDbContext())
            {
                var userAccount = context.UserAccounts.Where(x => x.UserId == UserAccount.UserId).FirstOrDefault();

                var hashedPassword = CreateMD5(confirmPassword).ToUpper();
                var newHasedPasword = CreateMD5(newPassword).ToUpper();

                if (!userAccount.Password.Equals(hashedPassword))
                {
                    System.Windows.MessageBox.Show("Please provide the correct previous password");
                    return;
                }
                else
                {
                    userAccount.Username = Username;
                    userAccount.Password = newHasedPasword;
                    context.SaveChanges();
                    System.Windows.MessageBox.Show("Password Changed Successfully");

                }
            }
        }

        private List<Models.Employee> GetEmployee(string employeeNo)
        {
            using (var context = new Database.PayrollDbContext())
            {
                return context.Employees.Where(x => x.EmployeeNo == employeeNo).ToList();

            }
        }
        private Models.UserAccount GetUser(string employeeNo)
        {
            using (var context = new Database.PayrollDbContext())
            {
                return context.UserAccounts.Where(x => x.UserId == employeeNo).FirstOrDefault();
            }


        }



        #region Properties
        private ObservableCollection<Models.Employee> _Details;



        public ObservableCollection<Models.Employee> Details
        {
            get { return _Details; }
            set
            {
                _Details = value;
                OnPropertyChanged();
            }
        }



        #endregion

    }
}
