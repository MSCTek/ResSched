using GalaSoft.MvvmLight;
using System;

namespace ResSched.ObjModel
{
    public partial class User : ObservableObject
    {
        private string _createdBy;
        private DateTime _createdDate;
        private string _email;
        private string _installationId;
        private bool _isActive;
        private bool _isDeleted;
        private DateTime _lastLoginDate;
        private string _lastModifiedBy;
        private DateTime _lastModifiedDate;
        private string _name;
        private Guid _userId;
        public User()
        {
        }

        public string Email
        {
            get { return _email; }
            set { Set(nameof(Email), ref _email, value); }
        }

        public string InstallationId
        {
            get { return _installationId; }
            set { Set(nameof(InstallationId), ref _installationId, value); }
        }

        public DateTime LastLoginDate
        {
            get { return _lastLoginDate; }
            set { Set(nameof(LastLoginDate), ref _lastLoginDate, value); }
        }

        public string Name
        {
            get { return _name; }
            set { Set(nameof(Name), ref _name, value); }
        }

        public Guid UserId
        {
            get { return _userId; }
            set { Set(nameof(UserId), ref _userId, value); }
        }
    }
}