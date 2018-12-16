using System;
using System.Collections.Generic;
using System.Text;

namespace ResSched.DataModel
{
    public class Resource
    {
        public string Description;
        public string ImageLink;
        public string ImageLinkThumb;
        public string Name;
        public Guid ResourceId;
        public bool IsActive;
        public bool IsDeleted;
        public DateTime LastModifiedDate;
        public string LastModifiedBy;
        public DateTime CreatedDate;
        public string CreatedBy;

    }
}
