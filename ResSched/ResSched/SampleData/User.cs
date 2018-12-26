using GalaSoft.MvvmLight;
using ResSched.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResSched.SampleData
{
    public class SampleUser 
    {
        public static User SampleUserGeorge
        {
            get
            {
                return new User()
                {
                    Name = "George Washington",
                    Email = "gWashington@gmail.com",
                    InstallationId = null,
                    LastLoginDate = DateTime.UtcNow,
                    UserId = Guid.NewGuid(), //TODO: make this a static guid
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = "gwashington",
                    CreatedDate = DateTime.UtcNow.AddDays(-1),
                    LastModifiedBy = "gwashington",
                    LastModifiedDate = DateTime.UtcNow.AddDays(-1),
                };
            }
        }

        public static User SampleUserMicky
        {
            get
            {
                return new User()
                {
                    Name = "Micky Mouse",
                    Email = "mikMouse@gmail.com",
                    InstallationId = null,
                    LastLoginDate = DateTime.UtcNow,
                    UserId = Guid.NewGuid(), //TODO: make this a static guid
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = "gWashington",
                    CreatedDate = DateTime.UtcNow.AddDays(-1),
                    LastModifiedBy = "gWashinton",
                    LastModifiedDate = DateTime.UtcNow.AddDays(-1),
                };
            }
        }

        public static User SampleUserMinnie
        {
            get
            {
                return new User()
                {
                    Name = "Minnie Mouse",
                    Email = "minMouse@gmail.com",
                    InstallationId = null,
                    LastLoginDate = DateTime.UtcNow,
                    UserId = Guid.NewGuid(), //TODO: make this a static guid
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = "gWashington",
                    CreatedDate = DateTime.UtcNow.AddDays(-1),
                    LastModifiedBy = "gWashinton",
                    LastModifiedDate = DateTime.UtcNow.AddDays(-1),
                };
            }
        }
        public static Guid SampleUserGuest_ID = Guid.NewGuid(); //TODO: make this a static guid
        public static string SampleUserGuest_Email = "guest@guest.com";
        public static User SampleUserGuest
        {
            get
            {
                return new User()
                {
                    Name = "guest",
                    Email = SampleUserGuest_Email,
                    InstallationId = null,
                    LastLoginDate = DateTime.UtcNow,
                    UserId = SampleUserGuest_ID,
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = "gWashington",
                    CreatedDate = DateTime.UtcNow.AddDays(-1),
                    LastModifiedBy = "gWashinton",
                    LastModifiedDate = DateTime.UtcNow.AddDays(-1),
                };
            }
        }

    }
}
