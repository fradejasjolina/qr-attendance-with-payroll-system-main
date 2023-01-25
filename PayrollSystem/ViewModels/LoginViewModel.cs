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
        private string _Versions;

        public string Versions
        {
            get { return _Versions; }
            set
            {
                _Versions = value;
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

        private Visibility _ErrorMessageVisibility;

        public Visibility ErrorMessageVisibility
        {
            get { return _ErrorMessageVisibility; }
            set
            {
                _ErrorMessageVisibility = value;
                OnPropertyChanged();
            }
        }

        private string _ErrorMessageText;

        public string ErrorMessageText
        {
            get { return _ErrorMessageText; }
            set
            {
                _ErrorMessageText = value;
                OnPropertyChanged();
            }
        }


        private Visibility _ProgressVisibility;
        public Visibility ProgressVisibility
        {
            get { return _ProgressVisibility; }
            set
            {
                _ProgressVisibility = value;
                OnPropertyChanged();
            }
        }


        public AsyncCommand<PasswordBox> LoginCommandAsync { get; set; }

        public LoginViewModel()
        {
            Version = "v0.1.00(dev)";
            LoginCommandAsync = new AsyncCommand<PasswordBox>(LoginCommandEvent);
            ProgressVisibility = Visibility.Hidden;
        }
        public static string CreateMD5(string input)
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


        private async Task LoginCommandEvent(PasswordBox passwordBox)
        {
            ProgressVisibility = Visibility.Visible;
            ErrorMessageVisibility = Visibility.Hidden;
            var username = Username;
            var password = passwordBox.Password;
          

            if (string.IsNullOrWhiteSpace(username))
            {
                ProgressVisibility = Visibility.Hidden;
                MessageBox.Show("Username is empty!");
                
                return;
            }
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrEmpty(password))
            {
                ProgressVisibility = Visibility.Hidden;
                MessageBox.Show("Password is empty!");
                return;
            }

            bool hasAccess = false;

            await Task.Run(() =>
            {
                using (var context = new Database.PayrollDbContext())
                {
                    try
                    {
                        var sqlQuery = $"Select * from useraccounts where Username ='{Username}' and Password='{CreateMD5(password)}' and IsEnabled=1;";

                        var userAccount = context.Database.SqlQuery<Models.UserAccount>(sqlQuery).FirstOrDefault();


                        if (userAccount != null)
                        {
                            hasAccess = true;
                            UserAccount = userAccount;
                        }
                        else
                        {
                            ErrorMessageVisibility = Visibility.Visible;
                            ProgressVisibility = Visibility.Hidden;
                            ErrorMessageText = "This user does not exists";
                        }
                    }
                    catch (Exception)
                    {
                        ErrorMessageVisibility = Visibility.Visible;
                        ProgressVisibility = Visibility.Hidden;
                        ErrorMessageText = "Refuse to connect";
                    }
                }
            });

            if (hasAccess) OpenWindow();

        }

        private void OpenWindow()
        {
            var windows = Application.Current.Windows.OfType<Window>().ToList();

            var loginWindow = (LoginWindow)windows.Where(x => x.IsActive).FirstOrDefault();


            if (loginWindow != null)        
            {
                MainWindow mainWindow = new MainWindow();
                loginWindow.Hide();
                mainWindow.Show();
                loginWindow.Close();
            }
        }
    }
}
