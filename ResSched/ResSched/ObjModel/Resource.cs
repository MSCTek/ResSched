using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResSched.ObjModel
{
    public partial class Resource : ObservableObject
    {
        private string _description;
        private string _imageLink;
        private string _imageLinkThumb;
        private string _name;
        private Guid _resourceId;
        private bool _isActive;
        public DateTime _lastModifiedDate;
        public string _lastModifiedBy;
        public DateTime _createdDate;
        public string _createdBy;
        public bool _isDeleted;



        public Resource()
        {

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

        public DateTime LastModifiedDate
        {
            get { return _lastModifiedDate; }
            set { Set(nameof(LastModifiedDate), ref _lastModifiedDate, value); }
        }

        public string LastModifiedBy
        {
            get { return _lastModifiedBy; }
            set { Set(nameof(LastModifiedBy), ref _lastModifiedBy, value); }
        }

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { Set(nameof(CreatedDate), ref _createdDate, value); }
        }

        public string CreatedBy
        {
            get { return _createdBy; }
            set { Set(nameof(CreatedBy), ref _createdBy, value); }
        }
    }
}
