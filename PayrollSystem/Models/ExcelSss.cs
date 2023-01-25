using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PayrollSystem.Models
{
    class SssColumn
    {
        public string RangeOfCompensation { get; set; }
        public string EmployeesCompensation { get; set; }
        public decimal MandatoryFund { get; set; }
        public decimal Total { get; set; }
        public decimal RSSER { get; set; }
        public decimal RSEEE { get; set; }
        public decimal RSETOTAL { get; set; }
        public decimal ECER { get; set; }
        public decimal ECEE { get; set; }
        public decimal ECTotal { get; set; }
        public decimal MPFER { get; set; }
        public decimal MPEE { get; set; }
        public decimal MPTotal { get; set; }
        public decimal TotalER { get; set; }
        public decimal TotalEE { get; set; }
        public decimal TotalTotal { get; set; }

        public decimal StartRange {
            get {
                var splittedRange = RangeOfCompensation.Split('-').FirstOrDefault().Replace(",", "");
                if (splittedRange.Equals("Below"))
                {
                    return 0;
                }
                if (decimal.TryParse(splittedRange, out decimal result))
                {
                    return result;
                }
                return 0;
            }
        }


        public decimal EndRange
        {
            get
            {
                
                var splittedRange = RangeOfCompensation.Split('-').LastOrDefault().Trim().Replace(",","");
                var isFloat = decimal.TryParse(splittedRange, out decimal result);
                if (splittedRange.Equals("over"))
                {
                    var firstValue = decimal.Parse(RangeOfCompensation.Split('-').First().Trim().Replace(",", ""));
                    return firstValue + 1;
                }
                if (isFloat)
                {
                    return result;
                }
                return 0;
            }
        }


    }
    class ExcelSss
    {
        private List<Models.SssColumn> GetExcelValues()
        {
            var list = new List<Models.SssColumn>();
            #region Retrieve Excel values
            using (SLDocument sLDocument =new SLDocument("ExcelSss.xlsx","Sheet1"))
            {
                var document = sLDocument.SelectWorksheet("Sheet1");
                var startRow = 5;
                var endRow = 49;
                for (int i = startRow; i <= endRow; i++)
                {
                    var mscroc = sLDocument.GetCellValueAsString($"A{i}");
                    var mscec = sLDocument.GetCellValueAsString($"B{i}");
                    var mscmpf = sLDocument.GetCellValueAsDecimal($"C{i}");
                    var msctotal = sLDocument.GetCellValueAsDecimal($"D{i}");
                    var rsser = sLDocument.GetCellValueAsDecimal($"E{i}");
                    var rssee = sLDocument.GetCellValueAsDecimal($"F{i}");
                    var rstotal = sLDocument.GetCellValueAsDecimal($"G{i}");
                    var ecer = sLDocument.GetCellValueAsDecimal($"H{i}");
                    var ecee = sLDocument.GetCellValueAsDecimal($"I{i}");
                    var ectotal = sLDocument.GetCellValueAsDecimal($"J{i}");
                    var mpfer = sLDocument.GetCellValueAsDecimal($"K{i}");
                    var mpfee = sLDocument.GetCellValueAsDecimal($"L{i}");
                    var mpftotal = sLDocument.GetCellValueAsDecimal($"M{i}");
                    var totaler = sLDocument.GetCellValueAsDecimal($"N{i}");
                    var totalee = sLDocument.GetCellValueAsDecimal($"O{i}");
                    var totaltotal = sLDocument.GetCellValueAsDecimal($"P{i}");

                    var sssColumn = new SssColumn {
                        RangeOfCompensation = mscroc,
                        EmployeesCompensation = mscec,
                        MandatoryFund = mscmpf,
                        Total = msctotal,
                        RSSER = rsser,
                        RSEEE = rssee,
                        RSETOTAL = rstotal,
                        ECER = ecer,
                        ECEE = ecee,
                        ECTotal = ectotal,
                        MPEE = mpfee,
                        MPFER = mpfer,
                        MPTotal = mpftotal,
                        TotalEE = totalee,
                        TotalER = totaler,
                        TotalTotal = totaltotal
                    };

                    list.Add(sssColumn);

                }

            }
            #endregion

            return list;    
        }
        public Models.SssColumn GetSssColumn(decimal totalIncome)
        {
            var excel = GetExcelValues();
            foreach (var x in excel)
            {
                if(totalIncome >= x.StartRange  && totalIncome <= x.EndRange)
                {
                    return x;
                }
            }
            return null;
        }

    }
}
 