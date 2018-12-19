using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

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
        private Guid _reservedByUserId;
        private string _reservedForUser;
        private DateTime _reservedOnDateTime;
        private Guid _resourceId;
        private Guid _resourceScheduleId;
        public ResourceSchedule()
        {
        }

        public bool IsDeleted
        {
            get { return _isDeleted; }
            set { Set(nameof(IsDeleted), ref _isDeleted, value); }
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
