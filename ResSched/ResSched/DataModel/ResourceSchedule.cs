using SQLite;
using System;

namespace ResSched.DataModel
{
    [Table("ResourceSchedule")]
    public class ResourceSchedule
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public DateTime ReservationStartDateTime { get; set; }
        public DateTime ReservationEndDateTime { get; set; }

        public string ReservationNotes { get; set; }

        public string ReservedByUserEmail { get; set; }

        public Guid ReservedByUserId { get; set; }

        public string ReservedForUser { get; set; }

        public DateTime ReservedOnDateTime { get; set; }

        public Guid ResourceId { get; set; }

        [PrimaryKey]
        public Guid ResourceScheduleId { get; set; }
    }
}