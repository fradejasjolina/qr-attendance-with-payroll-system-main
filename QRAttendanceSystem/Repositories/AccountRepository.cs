using QRAttendanceSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace QRAttendanceSystem.Repositories
{
    class AccountRepository : Interfaces.IAccountRepository
    {
        public void DeleteProfile(Guid guid)
        {
            using (var context = new AppDbContext())
            {
                var profile = context.Profiles.Where(x => x.ProfileId == guid).FirstOrDefault();
                context.Profiles.Remove(profile);
                context.SaveChanges();
            }
        }

        public void DeleteUserAccount(Guid guid)
        {
            using (var context = new AppDbContext())
            {
                var userAccount = context.UserAccounts.Where(x => x.UserId == guid).FirstOrDefault();
                context.UserAccounts.Remove(userAccount);
                context.SaveChanges();
            }
        }

        public void InsertProfile(Profile profile)
        {
            using (var context =new AppDbContext())
            {
                context.Profiles.Add(profile);
                context.SaveChanges();
            }
        }

        public void InsertUserAccount(UserAccount user)
        {
            throw new NotImplementedException();
        }

        public void UpdateProfile(Profile profile)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserAccount(UserAccount user)
        {
            throw new NotImplementedException();
        }



    }
}
