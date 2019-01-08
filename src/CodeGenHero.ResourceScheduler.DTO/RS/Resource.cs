// <auto-generated> - Template:DTO, Version:1.1, Id:58fa7ee2-89f7-41e6-85ed-8d4482653990
namespace CodeGenHero.ResourceScheduler.DTO.RS
{
	public partial class Resource
	{
		public Resource()
		{
			InitializePartial();
		}

		public System.Guid Id { get; set; } // Primary key
		public string CreatedBy { get; set; }
		public System.DateTime CreatedDate { get; set; }
		public string Description { get; set; }
		public string ImageLink { get; set; }
		public string ImageLinkThumb { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string UpdatedBy { get; set; }
		public System.DateTime UpdatedDate { get; set; }
		public string Name { get; set; }
		public virtual System.Collections.Generic.ICollection<ResourceSchedule> ResourceSchedules { get; set; } // Many to many mapping


		partial void InitializePartial();

	}
}