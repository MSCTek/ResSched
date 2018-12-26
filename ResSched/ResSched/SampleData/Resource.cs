using ResSched.DataModel;
using System;

namespace ResSched.SampleData
{
    public class SampleResource
    {
        public static Guid MeetingRoom1_ID = new Guid("0ce812ed-615c-4fad-8661-6c699e911204");
        public static Guid MeetingRoom3_ID = new Guid("1c7c8906-7603-4100-88cf-3ff04b1c1bc9");

        public static Resource MeetingRoom1
        {
            get
            {
                return new Resource()
                {
                    Name = "#1 Red Fox Meeting Room",
                    Description = "10 people max, bow window",
                    ResourceId = MeetingRoom1_ID,
                    ImageLink = "https://github.com/robintschroeder/ResSched/blob/master/Assets/Red_Fox_500x500.png?raw=true",
                    ImageLinkThumb = "https://github.com/robintschroeder/ResSched/blob/master/Assets/Red_Fox_500x500.png?raw=true",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedBy = "gwashington",
                    CreatedDate = DateTime.UtcNow.AddDays(-1),
                    LastModifiedBy = "gwashington",
                    LastModifiedDate = DateTime.UtcNow.AddDays(-1),
                };
            }
        }

        public static Resource MeetingRoom2
        {
            get
            {
                return new Resource()
                {
                    Name = "#2 Gray Fox Meeting Room",
                    Description = "5 people max",
                    IsActive = true,
                    ResourceId = new Guid("88605046-d79e-4e45-a706-7e578b6f7997"),
                    ImageLink = "https://github.com/robintschroeder/ResSched/blob/master/Assets/Gray_Fox_500x500.png?raw=true",
                    ImageLinkThumb = "https://github.com/robintschroeder/ResSched/blob/master/Assets/Gray_Fox_500x500.png?raw=true",
                    IsDeleted = false,
                    CreatedBy = "gwashington",
                    CreatedDate = DateTime.UtcNow.AddDays(-1),
                    LastModifiedBy = "gwashington",
                    LastModifiedDate = DateTime.UtcNow.AddDays(-1),
                };
            }
        }

        public static Resource MeetingRoom3
        {
            get
            {
                return new Resource()
                {
                    Name = "#3 Artic Fox Meeting Room",
                    Description = "5 people max",
                    IsActive = true,
                    ResourceId = MeetingRoom3_ID,
                    ImageLink = "https://github.com/robintschroeder/ResSched/blob/master/Assets/Arctic_Fox_500x500.png?raw=true",
                    ImageLinkThumb = "https://github.com/robintschroeder/ResSched/blob/master/Assets/Arctic_Fox_500x500.png?raw=true",
                    IsDeleted = false,
                    CreatedBy = "gwashington",
                    CreatedDate = DateTime.UtcNow.AddDays(-1),
                    LastModifiedBy = "gwashington",
                    LastModifiedDate = DateTime.UtcNow.AddDays(-1),
                };
            }
        }

        public static Resource XCarve
        {
            get
            {
                return new Resource()
                {
                    Name = "X-Carve",
                    Description = "3D Carving Machine",
                    IsActive = true,
                    ResourceId = new Guid("0212fbfe-0a7b-458d-88fd-1d9843e8b3c8"),
                    ImageLink = "https://github.com/robintschroeder/ResSched/blob/master/Assets/XCarve.png?raw=true",
                    ImageLinkThumb = "https://github.com/robintschroeder/ResSched/blob/master/Assets/XCarve.png?raw=true",
                    IsDeleted = false,
                    CreatedBy = "gwashington",
                    CreatedDate = DateTime.UtcNow.AddDays(-1),
                    LastModifiedBy = "gwashington",
                    LastModifiedDate = DateTime.UtcNow.AddDays(-1),
                };
            }
        }
    }
}