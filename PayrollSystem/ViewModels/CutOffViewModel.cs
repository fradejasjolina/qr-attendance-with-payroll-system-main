using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm;
using PayrollSystem.Database;
using PayrollSystem.Models;


namespace PayrollSystem.ViewModels
{
    class CutOffViewModel:Abstracts.RaisePropertyChanged
    {

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


        private ObservableCollection<CutOff> _CutOffCollection;

        public ObservableCollection<CutOff> CutOffCollection
        {
            get { return _CutOffCollection; }
            set { _CutOffCollection = value;
                OnPropertyChanged();
            }
        }

        private CutOff _SelectedCutOffItem;

        public CutOff SelectedCutOffItem
        {
            get { return _SelectedCutOffItem; }
            set { _SelectedCutOffItem = value;
                OnPropertyChanged();
                if (SelectedCutOffItem == null) return;
                StartDate = SelectedCutOffItem.StartDate;
                EndDate = SelectedCutOffItem.EndDate;
            }
        }



        public AsyncCommand AddNewCutOffCommandAsync { get; set; }
        public DelegateCommand ClearCutOffData { get; set; }

        public CutOffViewModel()
        {
            AddNewCutOffCommandAsync = new AsyncCommand(AddNewCutOffEvent);
            ClearCutOffData = new DelegateCommand(ClearCutOffEvent);
            GenerateCutOffs();
        }

        private CutOff GetCutOff()
        {
            return new CutOff {
                StartDate = (DateTime)StartDate,
                EndDate = (DateTime)EndDate
            };
        }

        private void GenerateCutOffs()
        {
            using (var context = new PayrollDbContext())
            {
                CutOffCollection = new ObservableCollection<CutOff>(context.CutOffs.ToList().OrderByDescending(x=>x.Id));
            }
        }
        private void ClearData()
        {
            SelectedCutOffItem = null;
            StartDate = null;
            EndDate = null;

        }

        private void ClearCutOffEvent()
        {
            SelectedCutOffItem = null;
            StartDate = null;
            EndDate = null;
        }


        private async Task AddNewCutOffEvent()
        {
            if (StartDate == null && EndDate == null)
            {
                MessageBox.Show("Please select dates");
                return;
            }


            using (var context = new PayrollDbContext())
            {
                if (SelectedCutOffItem == null)
                {
                    var cutOff = GetCutOff();
                    context.CutOffs.Add(cutOff);
                    await context.SaveChangesAsync();
                    MessageBox.Show("Save Successfully");
                    GenerateCutOffs();
                    ClearData();
                }
                else
                {
                    var x = context.CutOffs.Where(y => y.Id == SelectedCutOffItem.Id).FirstOrDefault();
                    if (x != null)
                    {
                        x.StartDate = (DateTime)StartDate;
                        x.EndDate = (DateTime)EndDate;

                        await context.SaveChangesAsync();
                        MessageBox.Show("Update Successfully");
                        GenerateCutOffs();
                        ClearData();
                    }
                }
            }
        }
    }
}
