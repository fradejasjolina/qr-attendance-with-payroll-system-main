using System;
using System.Collections.Generic;
using System.Text;
using QRAttendanceSystem.Models;

namespace QRAttendanceSystem.Interfaces
{
    interface IAccountRepository
    {
        void InsertProfile(Profile profile);
        void UpdateProfile(Profile profile);
        void DeleteProfile(Guid guid);
        void InsertUserAccount(UserAccount user);
        void UpdateUserAccount(UserAccount user);
        void DeleteUserAccount(Guid guid);

    }
}
