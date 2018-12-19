using System;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;

namespace ResSched.DataModel
{
    public class Resource
    {
       
        public string Description;

        public string ImageLink;

        public string ImageLinkThumb;

        public string Name;

        [JsonProperty(PropertyName = "ID")]
        public Guid ResourceId;
        public bool IsActive;

        public bool IsDeleted;

        public DateTime LastModifiedDate;

        public string LastModifiedBy;

        public DateTime CreatedDate;

        public string CreatedBy;

    }
}
