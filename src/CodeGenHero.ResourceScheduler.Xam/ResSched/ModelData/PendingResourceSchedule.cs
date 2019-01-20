using SQLite;

namespace CodeGenHero.ResourceScheduler.Xam.ModelData.RS
{
    [Table("PendingResourceSchedule")]
    public partial class PendingResourceSchedule
    {
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }

        [PrimaryKey]
        public System.Guid Id { get; set; }

        public bool IsDeleted { get; set; }
        public System.DateTime ReservationDate { get; set; }
        public System.DateTime ReservationEndDateTime { get; set; }
        public string ReservationNotes { get; set; }
        public System.DateTime ReservationStartDateTime { get; set; }
        public System.Guid? ReservedByUserId { get; set; }
        public string ReservedForUser { get; set; }
        public System.DateTime ReservedOnDateTime { get; set; }
        public System.Guid ResourceId { get; set; }
        public string UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    }
}