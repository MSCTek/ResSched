// <auto-generated> - Template:DTO, Version:1.1, Id:58fa7ee2-89f7-41e6-85ed-8d4482653990
namespace CodeGenHero.ResourceScheduler.DTO.RS
{
	public partial class ResourceSchedule
	{
		public ResourceSchedule()
		{
			InitializePartial();
		}

		public System.Guid Id { get; set; } // Primary key
		public System.Guid ResourceId { get; set; }
		public string CreatedBy { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public bool IsDeleted { get; set; }
		public string UpdatedBy { get; set; }
		public System.DateTime UpdatedDate { get; set; }
		public System.DateTime ReservationStartDateTime { get; set; }
		public System.DateTime ReservationEndDateTime { get; set; }
		public string ReservationNotes { get; set; }
		public System.Guid? ReservedByUserId { get; set; }
		public string ReservedForUser { get; set; }
		public System.DateTime ReservedOnDateTime { get; set; }
		public virtual Resource Resource { get; set; } 
		public virtual User User { get; set; } 


		partial void InitializePartial();

	}
}
