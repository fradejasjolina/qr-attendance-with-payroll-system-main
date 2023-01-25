using MySql.Data.EntityFramework;
using System.Data.Entity;
using PayrollSystem.Models;

namespace PayrollSystem.Database
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    class PayrollDbContext:DbContext
    {

        public PayrollDbContext() : base("name=AppDbContext")
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<QrCode> QrCodes { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<CutOff> CutOffs { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<PayrollAmount> PayrollAmounts { get; set; }
        public DbSet<CashAdvanceModel> CashAdvances { get; set; }
        public DbSet<CashAdvanceTracker> CashAdvanceTracker { get; set; }
        public DbSet<LeaveModel> Leaves { get; set; }
        public DbSet<EmployeeLeave> EmployeeLeaves { get; set; }
        public DbSet<FilingCashAdvance> FilingCashAdvances { get; set; }
        public DbSet<Overtime> Overtimes { get; set; }
        public DbSet<Setting> Settings { get; set; }

        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanTracker> LoanTrackers { get; set; }

    }
}
