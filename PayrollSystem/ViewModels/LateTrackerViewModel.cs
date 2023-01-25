using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.ViewModels
{
    class Late
    {
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public string Fullname { get; set; }
        public DateTime LogDate { get; set; }
        public string TimeIn { get; set; }
        public string TimeOut { get; set; }
        public float LateTotal { get; set; }

        public string LogDateDisplay { get=> LogDate.ToShortDateString(); }
    }

    class LateTrackerViewModel : Abstracts.RaisePropertyChanged
    {
        private ObservableCollection<Late> _LateCollection;

        public ObservableCollection<Late> LateCollection
        {
            get { return _LateCollection; }
            set { _LateCollection = value;
                OnPropertyChanged();
            }
        }

        public LateTrackerViewModel()
        {
            var latethisMonth = AdjustData().Where(x=>x.LogDate.Month == DateTime.Now.Month && x.LateTotal != 0).ToList();
            LateCollection = new ObservableCollection<Late>(latethisMonth);
        }


        private List<Late> AdjustData()
        {
            var list = new List<Late>();


            using (var context = new Database.PayrollDbContext())
            {
                foreach (var item in context.Attendance)
                {
                    var logDate = DateTime.Parse(item.LOGDATE);
                    var totalLate = item.TOTAL_LATE != "" ? float.Parse(item.TOTAL_LATE) : 0f;


                    Late late = new Late()
                    {
                        Id = item.ID,
                        Fullname = item.Fullname,
                        EmployeeNo = item.STUDENTID,
                        LogDate = logDate,
                        TimeIn = item.TIMEIN,
                        TimeOut = item.TIMEOUT,
                        LateTotal = totalLate
                    };

                    list.Add(late);
                }
            }
            return list;
        }
    }
}
