using DevExpress.Mvvm;
using PayrollSystem.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PayrollSystem.ViewModels
{
    class LoginViewModel : Abstracts.RaisePropertyChanged
    {
        private string _Version;

        public string Version
        {
            get { return _Version; }
            set { _Version = value;
                OnPropertyChanged();
            }
        }

        private string _Username;

        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }



        public AsyncCommand<PasswordBox> LoginCommandAsync  { get; set; }

        public LoginViewModel()
        {
            Version = "v0.1.00(dev)";
            LoginCommandAsync = new AsyncCommand<PasswordBox>(LoginCommandEvent);
        }

        private async Task LoginCommandEvent(PasswordBox passwordBox)
        {
            var username = Username;
            var password = passwordBox.Password;


            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Username is empty!");
                return;
            }
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password is empty!");
                return;
            }

            bool hasAccess = false;

            await Task.Run(() =>
            {
                if (username  == "admin" && password == "1234")
                {
                    hasAccess = true;
                }
                else
                {
                    MessageBox.Show("Invalid credentials!");
                }

                //using (var context = new Database.AppDbContext())
                //{
                //    var userAccount = context.UserAccounts.FirstOrDefault(x => x.Username.Equals(username) && x.Password.Equals(password));

                //    if (userAccount != null)
                //    {
                //        hasAccess = true;
                //        Variables.userAccount = userAccount;
                //    }
                //    else
                //    {
                //        MessageBox.Show("Invalid credentials!");
                //    }
                //}
            });

            if (hasAccess) OpenWindow();

        }

        private void OpenWindow()
        {
            var loginWindow = (LoginWindow)Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();
            if (loginWindow != null) loginWindow.Close();
        }
    }
}
