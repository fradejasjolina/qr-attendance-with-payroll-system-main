using QRAttendanceSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRAttendanceSystem.ViewModels
{
    class PopupViewModel:Abstracts.RaisePropertyChanged
    {
        public static QRResult QRResult;

        public PopupViewModel()
        {

           // SaveLogs();
        }
        private void SaveLogs()
        {
            var result = PopupViewModel.QRResult;

            using (var context = new AppDbContext())
            {
                var newLog = new UserLog
                {
                    TimeIn = DateTime.Now,
                    TimeOut = null,
                    UserId = Guid.Parse(result.QrValue)
                };

                if (context.UserLogs.Count() == 0)
                {
                    context.UserLogs.Add(newLog);
                }
                else
                {
                    var log = context.UserLogs.Where(x => x.UserId.ToString() == result.QrValue)
                                    .OrderByDescending(x => x.LogId).FirstOrDefault();
                    if (log == null)
                    {
                        context.UserLogs.Add(newLog);
                    }
                    else
                    {
                        if (log.TimeOut == null)
                        {
                            log.TimeOut = DateTime.Now;
                        }
                        else
                        {
                            context.UserLogs.Add(newLog);
                        }
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
