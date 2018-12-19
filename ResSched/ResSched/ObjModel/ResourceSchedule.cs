using GalaSoft.MvvmLight;
using System;

namespace ResSched.ObjModel
{
    public partial class ResourceSchedule : ObservableObject
    {
        private string _createdBy;
        private DateTime _createdDate;
        private bool _isDeleted;
        private string _lastModifiedBy;
        private DateTime _lastModifiedDate;
        private DateTime _reservationDateTime;
        private string _reservationNotes;
        private string _reservedByUserEmail;
        private Guid _reservedByUserId;
        private string _reservedForUser;
        private DateTime _reservedOnDateTime;
        private Resource _resource;
        private Guid _resourceId;
        private Guid _resourceScheduleId;

        public ResourceSchedule()
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

        public DateTime ReservationDateTime
        {
            get { return _reservationDateTime; }
            set { Set(nameof(ReservationDateTime), ref _reservationDateTime, value); }
        }

        public string ReservationNotes
        {
            get { return _reservationNotes; }
            set { Set(nameof(ReservationNotes), ref _reservationNotes, value); }
        }

        public string ReservedByUserEmail
        {
            get { return _reservedByUserEmail; }
            set { Set(nameof(ReservedByUserEmail), ref _reservedByUserEmail, value); }
        }

        public Guid ReservedByUserId
        {
            get { return _reservedByUserId; }
            set { Set(nameof(ReservedByUserId), ref _reservedByUserId, value); }
        }

        public string ReservedForUser
        {
            get { return _reservedForUser; }
            set { Set(nameof(ReservedForUser), ref _reservedForUser, value); }
        }

        public DateTime ReservedOnDateTime
        {
            get { return _reservedOnDateTime; }
            set { Set(nameof(ReservedOnDateTime), ref _reservedOnDateTime, value); }
        }

        public Resource Resource
        {
            get { return _resource; }
            set { Set(nameof(Resource), ref _resource, value); }
        }

        public Guid ResourceId
        {
            get { return _resourceId; }
            set { Set(nameof(ResourceId), ref _resourceId, value); }
        }

        public Guid ResourceScheduleId
        {
            get { return _resourceScheduleId; }
            set { Set(nameof(ResourceScheduleId), ref _resourceScheduleId, value); }
        }
    }
}