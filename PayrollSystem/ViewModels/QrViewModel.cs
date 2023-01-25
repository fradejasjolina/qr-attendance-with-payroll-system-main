using DevExpress.Mvvm;
using PayrollSystem.Database;
using PayrollSystem.Misc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ZXing;

namespace PayrollSystem.ViewModels
{
    class QrViewModel:Abstracts.RaisePropertyChanged
    {
        private ObservableCollection<Models.Employee> _QrEmployees;

        public ObservableCollection<Models.Employee> QrEmployees
        {
            get { return _QrEmployees; }
            set { _QrEmployees = value;
                OnPropertyChanged();
            }
        }

        private byte[] _ImageSource;

        public byte[] ImageSource
        {
            get { return _ImageSource; }
            set { _ImageSource = value;
                OnPropertyChanged();
            }
        }

        private string _SearchQrText;

        public string SearchQrText
        {
            get { return _SearchQrText; }
            set { _SearchQrText = value;
                OnPropertyChanged();
                SearchEmployee(SearchQrText);
            }
        }



        private Models.Employee _SelectedQrEmployee;

        public Models.Employee SelectedQrEmployee
        {
            get { return _SelectedQrEmployee; }
            set { _SelectedQrEmployee = value;
                OnPropertyChanged();

                if (SelectedQrEmployee == null)
                {
                    return;
                }
                using (var context = new PayrollDbContext())
                {
                    var isQrExists = context.QrCodes.Where(x => x.EmployeeNo == SelectedQrEmployee.EmployeeNo).FirstOrDefault();
                    if (isQrExists == null)
                    {
                        ImageSource = new Bitmap("logo.png").ToByteArray(ImageFormat.Png);
                    }
                    else
                    {
                        if (File.Exists(isQrExists.QrCodePath))
                        {
                            ImageSource = new Bitmap(isQrExists.QrCodePath).ToByteArray(ImageFormat.Png);
                        }
                    }
                }
            }
        }

        public AsyncCommand OnGenerateQrCodeCommandAsync { get; set; }
        public AsyncCommand OnSaveQrCommandAsync { get; set; }

        public QrViewModel()
        {
            using (var context = new PayrollDbContext())
            { 
                var y = context.Employees.Where(x => x.Status != "Resign");
                QrEmployees = new ObservableCollection<Models.Employee>(y);
            }
            OnGenerateQrCodeCommandAsync = new AsyncCommand(OnGenerateQrCodeEvent);
            OnSaveQrCommandAsync = new AsyncCommand(OnSaveQrCodeEvent);

            ImageSource = new Bitmap("logo.png").ToByteArray(ImageFormat.Png);
        }
        private void SearchEmployee(string searchText)
        {
            using (var context = new PayrollDbContext())
            {
                var result = context.Employees.Where(x=>x.Status != "Resign").Where(x =>
                x.EmployeeNo.Contains(searchText)
                || x.Firstname.Contains(searchText)
                || x.Lastname.Contains(searchText)
                || x.Position.Contains(searchText)
                );

                QrEmployees = new ObservableCollection<Models.Employee>(result);
            }
        }

        private async Task OnGenerateQrCodeEvent()
        {
            if (SelectedQrEmployee == null)
            {
                return;
            }

            EncodeQRCode(SelectedQrEmployee.EmployeeNo);
            MessageBox.Show("Generated Successfully");

            await Task.CompletedTask;
        }

        private void EncodeQRCode(string qrValue)
        {
            try
            {
                var writer = new BarcodeWriter()
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Width = 300,
                        Height = 300

                    }
                };
                var result = writer.Write(qrValue);
                var barcodeBitmap = new Bitmap(result);
                barcodeBitmap.SetResolution(300, 300);

                ImageSource = barcodeBitmap.ToByteArray(ImageFormat.Png);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n\n" + ex.StackTrace);
            }
        }

        private async Task OnSaveQrCodeEvent()
        {
            
            var path = @"\\SERVER\Users\Public\Payroll Templates\employeeqrcode\" + SelectedQrEmployee.Fullname + ".png";
            var bitmap = new Bitmap(new MemoryStream(ImageSource));
            bitmap.Save(path);

            using (var context = new PayrollDbContext())    
            {
                var result = context.QrCodes.Where(x => x.EmployeeNo == SelectedQrEmployee.EmployeeNo).FirstOrDefault();

                if (result != null)
                {
                    MessageBox.Show("QR Code is existing");
                    return;
                }
                
                context.QrCodes.Add(new Models.QrCode
                {
                    QrCodePath = path,
                    EmployeeNo = SelectedQrEmployee.EmployeeNo,
                    QrCodeValue = Misc.Encryptor.MD5Hash(SelectedQrEmployee.EmployeeNo)
                });

                await context.SaveChangesAsync();
                MessageBox.Show("Save Successfully");
            }
        }
    }
}
