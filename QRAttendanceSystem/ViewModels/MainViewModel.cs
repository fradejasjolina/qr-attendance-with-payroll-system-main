using QRAttendanceSystem.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows;
using ZXing;
using System.IO;
using System.Linq;
using DevExpress.Mvvm;
using System.Threading.Tasks;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using QRAttendanceSystem.Extensions;
using System.Drawing.Imaging;
using System.Windows.Threading;


namespace QRAttendanceSystem.ViewModels
{
    class MainViewModel:Abstracts.RaisePropertyChanged
    {

        private string ParentPath = $@"{Directory.GetCurrentDirectory()}\QRCodes\";
        private VideoCaptureDevice VideoCaptureDevice;
        private FilterInfoCollection FilterInfoCollection;

        DispatcherTimer DispatcherTimer = new DispatcherTimer();
        public bool IsScanned;


        #region Properties
        private byte[] _ImageSource;

        public byte[] ImageSource
        {
            get { return _ImageSource; }
            set { _ImageSource = value;
                OnPropertyChanged();
            }
        }
        private string _Color;

        public string Color
        {
            get { return _Color; }
            set { _Color = value;
                OnPropertyChanged();
            }
        }

        private List<string> _CameraDevices;

        public List<string> CameraDevices
        {
            get { return _CameraDevices; }
            set { _CameraDevices = value;
                OnPropertyChanged();
            }
            
        }

        private int _SelectedCameraIndex;

        public int SelectedCameraIndex
        {
            get { return _SelectedCameraIndex; }
            set { _SelectedCameraIndex = value;
                OnPropertyChanged();
            }
        }



        #endregion


        public AsyncCommand ScanQrCodeCommandAsync { get; set; }
        public  AsyncCommand OpenCameraCommandAsync { get; set; }
        public DelegateCommand OnCloseWindow { get; set; }
        public MainViewModel()
        {
            Color = "#";
            ImageSource = new Bitmap($"{ParentPath}4ab3241c-055a-42f0-86bc-22570e69babb.png").ToByteArray(ImageFormat.Png);
            //OpenCameraCommandAsync = new AsyncCommand(OpenCameraEvent);
            //ScanQrCodeCommandAsync = new AsyncCommand(ScanQrCodeEvent);
            //ShowCameraList();

            //DispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            //DispatcherTimer.Tick += DispatcherTimer_Tick;
            //DispatcherTimer.Start();



        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if(VideoCaptureDevice.IsRunning && IsScanned)
            {
                VideoCaptureDevice.Stop();
                OpenWindow();
            }
        }

        private void OpenWindow()
        {
            var loginWindow = (MainWindow)Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);

            PopupWindow mainWindow = new PopupWindow();

            mainWindow.Show();
            
            if (loginWindow != null) loginWindow.Close();
        }


        private void ShowCameraList()
        {
            CameraDevices = new List<string>();
            FilterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            
            foreach (FilterInfo item in FilterInfoCollection)
            {
                CameraDevices.Add(item.Name);
            }

            SelectedCameraIndex = 1;
            VideoCaptureDevice = new VideoCaptureDevice();
        }



        private async Task OpenCameraEvent()
        {
            await Task.Run(() =>
            {
                RestartCamera();
            });
        }
        private void RestartCamera()
        {
            VideoCaptureDevice = new VideoCaptureDevice(FilterInfoCollection[SelectedCameraIndex].MonikerString);
            VideoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            VideoCaptureDevice.Start();
        }


        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            ImageSource = bitmap.ToByteArray(ImageFormat.Png);
            var result = DecodeQRCode(new Bitmap(new MemoryStream(ImageSource)));
            if (result == null) return;
            PopupViewModel.QRResult = result;
            IsScanned = true;
        }

      

        private async Task ScanQrCodeEvent()
        {
            await OpenCameraEvent();
    
        }

        
        private QRResult DecodeQRCode(Bitmap bitmap)
        {
            var reader = new BarcodeReader() {
                AutoRotate = true
            };
            
            var result = reader.Decode(bitmap);
            if (result == null) return null;
            
            return new QRResult() {
                QrType = result.BarcodeFormat.ToString(),
                QrValue = result.Text
            };
        }

        private void EncodeQRCode(string fileName,string qrValue)
        {
            try
            {
                var writer = new BarcodeWriter() { Format = BarcodeFormat.QR_CODE };
                var result = writer.Write(qrValue);
                var barcodeBitmap = new Bitmap(result);
                barcodeBitmap.Save(fileName);
                barcodeBitmap.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message+"\n\n"+ex.StackTrace);
            }
        }



    }
}
