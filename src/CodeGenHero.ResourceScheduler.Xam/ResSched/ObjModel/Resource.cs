using GalaSoft.MvvmLight;
using System;

namespace ResSched.ObjModel
{
    public partial class Resource : ObservableObject
    {
        private string _createdBy;
        private DateTime _createdDate;
        private string _description;
        private string _imageLink;
        private string _imageLinkThumb;
        private bool _isActive;
        private bool _isDeleted;
        private string _lastModifiedBy;
        private DateTime _lastModifiedDate;
        private string _name;
        private Guid _resourceId;

        public Resource()
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

        public string Description
        {
            get { return _description; }
            set { Set(nameof(Description), ref _description, value); }
        }

        public string ImageLink
        {
            get { return _imageLink; }
            set { Set(nameof(ImageLink), ref _imageLink, value); }
        }

        public string ImageLinkThumb
        {
            get { return _imageLinkThumb; }
            set { Set(nameof(ImageLinkThumb), ref _imageLinkThumb, value); }
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

        public Guid ResourceId
        {
            get { return _resourceId; }
            set { Set(nameof(ResourceId), ref _resourceId, value); }
        }
    }
}