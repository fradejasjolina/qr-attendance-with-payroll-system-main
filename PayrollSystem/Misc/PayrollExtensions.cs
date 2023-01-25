using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayrollSystem.Misc
{
    public static class PayrollExtensions
    {

        public static double TotalLates(this List<TimeSpan> timeSpans)
        {
            var totalOfLates = 0d;

            foreach (var late in timeSpans)
            {
                totalOfLates += Math.Abs(late.TotalMinutes);
            }
            return totalOfLates;
        }
        public static decimal TotalUndertimes(this List<decimal> undertimes)
        {
            var totalOfUnderTime = 0M;
            foreach (var undertime in undertimes)
            {
                totalOfUnderTime += Math.Abs(undertime);
            }
            return totalOfUnderTime;
        }



    }
}
