using GalaSoft.MvvmLight;
using ResSched.ObjModel;
using System;

namespace ResSched.Models
{
    public class HourlySchedule : ObservableObject
    {
        private DateTime _hour;
        private ResourceSchedule _resourceSchedule;

        public DateTime Hour
        {
            get { return _hour; }
            set { Set(nameof(Hour), ref _hour, value); }
        }

        public string HourDisplay { get { return this.Hour.ToShortTimeString(); } }

        public bool IsReserved { get { return ResourceSchedule == null ? false : true; } }

        public string ReservedMessage { get { return ResourceSchedule == null ? "Open" : ResourceSchedule.ReservedForUser; } }

        public ResourceSchedule ResourceSchedule
        {
            get { return _resourceSchedule; }
            set { Set(nameof(ResourceSchedule), ref _resourceSchedule, value); }
        }
    }
}