using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace PayrollSystem.Abstracts
{
    class RaisePropertyChanged : INotifyPropertyChanged
    {
        public static string Version = "v1.00(dev)";
        public static Models.UserAccount UserAccount { get; set; }
        public static string TemplatesPath = @"\\SERVER\Users\Public\Payroll Templates\";
        public static string WindowsName = "IJK APEX [HRIS - ";
        public static Models.Setting Setting { get; set; }

        public RaisePropertyChanged()
        {
            using (var context = new Database.PayrollDbContext())
            {
                //var settings = context.
                Setting = context.Settings.FirstOrDefault();
            }



        }





        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
