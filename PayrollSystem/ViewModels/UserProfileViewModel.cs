using DevExpress.Mvvm;
using PayrollSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PayrollSystem.ViewModels
{
    class UserProfileViewModel : Abstracts.RaisePropertyChanged
    {
        #region Button Commands
        public DelegateCommand ButtonOneCommand { get; set; }
        public DelegateCommand ButtonTwoCommand { get; set; }
        public DelegateCommand ButtonThreeCommand { get; set; }
        public DelegateCommand ButtonFourCommand { get; set; }
        public DelegateCommand ButtonFiveCommand { get; set; }

        #endregion

        #region User Account Commands
        public DelegateCommand<object[]> UpdatePasswordCommand { get; set; }

        #endregion

        #region Tab Properties
        private int _SelectedIndex;

        public int SelectedIndex
        {
            get { return _SelectedIndex; }
            set
            {
                _SelectedIndex = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Button Properties
        private string _ButtonOneBackground;

        public string ButtonOneBackground
        {
            get { return _ButtonOneBackground; }
            set
            {
                _ButtonOneBackground = value;
                OnPropertyChanged();
            }
        }

        private string _ButtonTwoBackground;

        public string ButtonTwoBackground
        {
            get { return _ButtonTwoBackground; }
            set
            {
                _ButtonTwoBackground = value;
                OnPropertyChanged();
            }
        }

        private string _ButtonThreeBackground;

        public string ButtonThreeBackground
        {
            get { return _ButtonThreeBackground; }
            set
            {
                _ButtonThreeBackground = value;
                OnPropertyChanged();
            }
        }

        private string _ButtonFourBackground;

        public string ButtonFourBackground
        {
            get { return _ButtonFourBackground; }
            set
            {
                _ButtonFourBackground = value;
                OnPropertyChanged();
            }
        }
        private string _ButtonFiveBackground;

        public string ButtonFiveBackground
        {
            get { return _ButtonFiveBackground; }
            set
            {
                _ButtonFiveBackground = value;
                OnPropertyChanged();
            }
        }








        #endregion

        #region Employee Details
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

        private string _Fullname;

        public string Fullname
        {
            get { return _Fullname; }
            set
            {
                _Fullname = value;
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

        private string _Birthdate;

        public string Birthdate
        {
            get { return _Birthdate; }
            set { _Birthdate = value;
                OnPropertyChanged();
            }
        }

        private string _Address;

        public string Address
        {
            get { return _Address; }
            set { _Address = value;
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

        private string _HiringDate;

        public string HiringDate
        {
            get { return _HiringDate; }
            set
            {
                _HiringDate = value;
                OnPropertyChanged();
            }
        }

        private string _BasicPay;

        public string BasicPay
        {
            get { return _BasicPay; }
            set
            {
                _BasicPay = value;
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

        private string _SSS;

        public string SSS
        {
            get { return _SSS; }
            set
            {
                _SSS = value;
                OnPropertyChanged();
            }
        }

        private string _TIN;

        public string TIN
        {
            get { return _TIN; }
            set
            {
                _TIN = value;
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

        private string _EmployeePicturePath;

        public string EmployeePicturePath
        {
            get { return _EmployeePicturePath; }
            set
            {
                _EmployeePicturePath = value;
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

        #endregion

        #region User Account Properties

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


        public UserProfileViewModel()
        {
            #region Button Commands
            ButtonOneCommand = new DelegateCommand(ButtonOneEvent);
            ButtonTwoCommand = new DelegateCommand(ButtonTwoEvent);
            ButtonThreeCommand = new DelegateCommand(ButtonThreeEvent);
            ButtonFourCommand = new DelegateCommand(ButtonFourEvent);
            ButtonFiveCommand = new DelegateCommand(ButtonFiveEvent);

            ButtonOneBackground = "Navy";
            ButtonTwoBackground = "Gray";
            ButtonThreeBackground = "Gray";
            ButtonFourBackground = "Gray";
            ButtonFiveBackground = "Gray";


            #endregion

            #region User Account Commands
            Username = UserAccount.Username;
            UpdatePasswordCommand = new DelegateCommand<object[]>(UpdatePasswordEvent);
            #endregion

            GetEmployee();
        }

        private void UpdatePasswordEvent(object[] obj)
        {
            var confirmPassword = ((PasswordBox)obj[0]).Password;
            var newPassword = ((PasswordBox)obj[1]).Password;


            using (var context = new Database.PayrollDbContext())
            {
                var userAccount = context.UserAccounts.Where(x => x.UserId == UserAccount.UserId && x.UserType == UserAccount.UserType).FirstOrDefault();

                var hashedPassword = CreateMD5(confirmPassword).ToUpper();
                var newHasedPasword = CreateMD5(newPassword).ToUpper();

                if (!userAccount.Password.ToUpper().Equals(hashedPassword))
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



        #region Button Events
        private void ButtonOneEvent()
        {
            SelectedIndex = 0;
            ButtonOneBackground = "Navy";
            ButtonTwoBackground = "Gray";
            ButtonThreeBackground = "Gray";
            ButtonFourBackground = "Gray";
            ButtonFiveBackground = "Gray";
        }

        private void ButtonTwoEvent()
        {
            SelectedIndex = 1;
            ButtonOneBackground = "Gray";
            ButtonTwoBackground = "Navy";
            ButtonThreeBackground = "Gray";
            ButtonFourBackground = "Gray";
            ButtonFiveBackground = "Gray";
        }
        private void ButtonThreeEvent()
        {
            SelectedIndex = 2;
            ButtonOneBackground = "Gray";
            ButtonTwoBackground = "Gray";
            ButtonThreeBackground = "Navy";
            ButtonFourBackground = "Gray";
            ButtonFiveBackground = "Gray";
        }

        private void ButtonFourEvent()
        {
            SelectedIndex = 3;
            ButtonOneBackground = "Gray";
            ButtonTwoBackground = "Gray";
            ButtonThreeBackground = "Gray";
            ButtonFourBackground = "Navy";
            ButtonFiveBackground = "Gray";
        }

        private void ButtonFiveEvent()
        {
            SelectedIndex = 4;
            ButtonOneBackground = "Gray";
            ButtonTwoBackground = "Gray";
            ButtonThreeBackground = "Gray";
            ButtonFourBackground = "Gray";
            ButtonFiveBackground = "Navy";
        }

        #endregion


        private void GetEmployee()
        {
            using (var context = new Database.PayrollDbContext())
            {
                var employeeDetails = context.Employees.Where(x => x.EmployeeNo == UserAccount.UserId).FirstOrDefault();
                EmployeeNo = employeeDetails.EmployeeNo;
                Fullname = employeeDetails.Fullname;
                Gender = employeeDetails.Gender;
                Birthdate = employeeDetails.DisplayBirthDate;
                Address = employeeDetails.Address;
                Position = employeeDetails.Position;
                Department = employeeDetails.Department;
                HiringDate = employeeDetails.DisplayHiringDate;
                BasicPay = $"{employeeDetails.BasicPay} Php";
                Status = employeeDetails?.Status;
                Pagibig = employeeDetails.Pagibig;
                Philhealth = employeeDetails.Philhealth;
                SSS = employeeDetails.SssNumber;
                TIN = employeeDetails.TinNo;
                TranspoAllowance = employeeDetails.TranspoAllowance;
                Allowance = employeeDetails.Allowance ;
                MedicalAllowance = employeeDetails.MedicalAllowance;
                DentalAllowance = employeeDetails.DentalAllowance;
                VisionAllowance = employeeDetails.VisionAllowance;
                EmployeePicturePath = employeeDetails.EmployeePicturePath;
                AccountNo = employeeDetails.AccountNo;
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


    }
}
