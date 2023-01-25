using DocumentFormat.OpenXml.Spreadsheet;
using PayrollSystem.Database;
using PayrollSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PayrollSystem.Misc
{
    internal class LoanHelper
    {
        public List<LoanViewModel> GetLoans(string userType, string userId)
        {
            var list = new List<LoanViewModel>();
            using (var context = new PayrollDbContext())
            {
                var result = context.Database.SqlQuery<LoanViewModel>("CALL sp_ListOfLoans()").ToList();

                if (userType == "Administrator")
                {
                    return result;
                }
                else
                {
                    var filteredList = result.Where(x => x.EmployeeNo == userId).ToList();
                    return filteredList;
                }
            }
        }

        public Visibility DisableControls(string userType)
        {
            return userType == "Administrator" ? Visibility.Visible: Visibility.Collapsed;
        }

    }
}
