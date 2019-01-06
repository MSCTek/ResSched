using SQLite;
using System;

namespace ResSched.DataModel
{
    [Table("Resource")]
    public class Resource
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Description { get; set; }

        public string ImageLink { get; set; }

        public string ImageLinkThumb { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public string Name { get; set; }

        [PrimaryKey]
        public Guid ResourceId { get; set; }
    }
}