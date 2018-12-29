using ResSched.DataModel;
using System;

namespace ResSched.SampleData
{
    public static class SampleResourceSchedule
    {
        public static ResourceSchedule SampleSchedule1
        {
            get
            {
                return new ResourceSchedule()
                {
                    ResourceScheduleId = Guid.NewGuid(),
                    ResourceId = SampleResource.MeetingRoom1_ID,
                    ReservationDate = DateTime.UtcNow.Date.AddDays(4).Date,
                    ReservationStartDateTime = DateTime.UtcNow.Date.AddDays(4).AddHours(10),
                    ReservationEndDateTime = DateTime.UtcNow.Date.AddDays(4).AddHours(12),
                    ReservedOnDateTime = DateTime.UtcNow.AddDays(-1),
                    ReservedByUserId = SampleUser.SampleUserGuest_ID,
                    ReservedByUserEmail = SampleUser.SampleUserGuest_Email,
                    ReservedForUser = "Micky Mouse",
                    ReservationNotes = "Sample Notes for Sample Reservation #1",
                    IsDeleted = false,
                    CreatedBy = "gwashington",
                    CreatedDate = DateTime.UtcNow.AddDays(-1),
                    LastModifiedBy = "gwashington",
                    LastModifiedDate = DateTime.UtcNow.AddDays(-1),
                };
            }
        }

        public static ResourceSchedule SampleSchedule2
        {
            get
            {
                return new ResourceSchedule()
                {
                    ResourceScheduleId = Guid.NewGuid(),
                    ResourceId = SampleResource.MeetingRoom3_ID,
                    ReservationDate = DateTime.UtcNow.Date.AddDays(4).Date,
                    ReservationStartDateTime = DateTime.UtcNow.Date.AddDays(4).AddHours(9),
                    ReservationEndDateTime = DateTime.UtcNow.Date.AddDays(4).AddHours(12),
                    ReservedOnDateTime = DateTime.UtcNow.AddDays(-1),
                    ReservedByUserId = SampleUser.SampleUserGuest_ID,
                    ReservedByUserEmail = SampleUser.SampleUserGuest_Email,
                    ReservedForUser = "Micky Mouse",
                    ReservationNotes = "Sample Notes for Sample Reservation #2",
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