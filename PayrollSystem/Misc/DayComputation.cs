using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Misc
{
    class DayComputation
    {
        public string _startDate { get; set; }
        public string _endDate { get; set; }

        public DayComputation(string startDate, string endDate)
        {
            _startDate = startDate;
            _endDate = endDate;


        }
        public decimal GetAbsentCount(decimal workingDays)
        {
            var bussinessDays = GetNumberOfCutOffs();
            var result = workingDays <= 0 ? 0 : Math.Abs(bussinessDays - workingDays);

            return result;
        }



        public int GetNumberOfCutOffs()
        {

            double result = GetBusinessDays(DateTime.Parse(_startDate), DateTime.Parse(_endDate));

            return Convert.ToInt32(result);
        }

        private double GetBusinessDays(DateTime startD, DateTime endD)
        {
            double calcBusinessDays = 1 + ((endD - startD).TotalDays * 5 - (startD.DayOfWeek - endD.DayOfWeek) * 2) / 7;

            if (endD.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
            if (startD.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;

            return calcBusinessDays;
        }

        public IEnumerable<DateTime> BussinessDays()
        {
            var start = DateTime.Parse(_startDate);
            var end = DateTime.Parse(_endDate);

            for (var date = start; date <= end; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    yield return date;
                }
            }
        }





    }
}
