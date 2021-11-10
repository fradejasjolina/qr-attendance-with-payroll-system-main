using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using QRAttendanceSystem.Models;
using System.Data.Entity;
using MySql.Data.EntityFramework;

namespace QRAttendanceSystem
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()  : base("name=AppDbContext")
        {

        }


        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<QrCode> QrCodes { get; set; }

    }
}
