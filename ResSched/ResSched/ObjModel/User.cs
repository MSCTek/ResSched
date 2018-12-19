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

        public string CreatedBy
        {
            get { return _createdBy; }
            set { Set(nameof(CreatedBy), ref _createdBy, value); }
        }

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { Set(nameof(CreatedDate), ref _createdDate, value); }
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

        public bool IsActive
        {
            get { return _isActive; }
            set { Set(nameof(IsActive), ref _isActive, value); }
        }

        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { Set(nameof(IsDeleted), ref _isDeleted, value); }
        }

        public DateTime LastLoginDate
        {
            get { return _lastLoginDate; }
            set { Set(nameof(LastLoginDate), ref _lastLoginDate, value); }
        }

        public string LastModifiedBy
        {
            get { return _lastModifiedBy; }
            set { Set(nameof(LastModifiedBy), ref _lastModifiedBy, value); }
        }

        public DateTime LastModifiedDate
        {
            get { return _lastModifiedDate; }
            set { Set(nameof(LastModifiedDate), ref _lastModifiedDate, value); }
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