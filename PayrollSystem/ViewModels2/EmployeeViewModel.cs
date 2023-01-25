using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.ViewModels2
{
    class EmployeeViewModel:Abstracts.RaisePropertyChanged
    {
        #region Button Commands
        public DelegateCommand ButtonOneCommand { get; set; }
        public DelegateCommand ButtonTwoCommand { get; set; }
        public DelegateCommand ButtonThreeCommand { get; set; }
        public DelegateCommand ButtonFourCommand { get; set; }
        public DelegateCommand ButtonFiveCommand { get; set; }

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

        public EmployeeViewModel()
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



    }
}
