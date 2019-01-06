using SQLite;
using System;

namespace ResSched.DataModel
{
    [Table("User")]
    public class User
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Email { get; set; }

        public string InstallationId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime LastLoginDate { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public string Name { get; set; }

        [PrimaryKey]
        public Guid UserId { get; set; }

        public string UserName { get; set; }
    }
}