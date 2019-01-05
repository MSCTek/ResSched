using ResSched.DataModel;
using System;

namespace ResSched.SampleData
{
    public class SampleUser
    {
        public static string SampleUserGuest_Email = "guest@guest.com";
        public static Guid SampleUserGuest_ID = Guid.Parse("ebf76675-1d6b-4af0-aac5-072ea68cb1dd");

        public static User SampleUserGeorge
        {
            get
            {
                return new User()
                {
                    Name = "George Washington",
                    UserName = "gWashington",
                    Email = "gWashington@gmail.com",
                    InstallationId = null,
                    LastLoginDate = DateTime.UtcNow,
                    UserId = Guid.Parse("4f9f7766-4050-424b-b72c-57180bda0d2c"),
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = "gwashington",
                    CreatedDate = DateTime.UtcNow.AddDays(-1),
                    LastModifiedBy = "gwashington",
                    LastModifiedDate = DateTime.UtcNow.AddDays(-1),
                };
            }
        }

        public static User SampleUserGuest
        {
            get
            {
                return new User()
                {
                    Name = "guest",
                    UserName = "guest",
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

        public static User SampleUserMicky
        {
            get
            {
                return new User()
                {
                    Name = "Micky Mouse",
                    UserName = "mikMouse",
                    Email = "mikMouse@gmail.com",
                    InstallationId = null,
                    LastLoginDate = DateTime.UtcNow,
                    UserId = Guid.Parse("f690ad1d-fcca-46ba-ab43-16b04497f896"),
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
                    UserName = "minMouse",
                    Email = "minMouse@gmail.com",
                    InstallationId = null,
                    LastLoginDate = DateTime.UtcNow,
                    UserId = Guid.Parse("7a7e485b-954a-4f85-85b2-7bbd0082720f"),
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = "gWashington",
                    CreatedDate = DateTime.UtcNow.AddDays(-1),
                    LastModifiedBy = "gWashinton",
                    LastModifiedDate = DateTime.UtcNow.AddDays(-1),
                };
            }
        }

        public static User SampleUserRobin
        {
            get
            {
                return new User()
                {
                    Name = "Robin Schroeder",
                    UserName = "rschroeder",
                    Email = "robin@msctek.com",
                    InstallationId = null,
                    LastLoginDate = DateTime.UtcNow,
                    UserId = Guid.Parse("7d687c23-fefb-458d-b3e9-814247a9a9d6"),
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