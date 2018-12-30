namespace ResSched.Mappers
{
    public static class Mapper
    {
        public static DataModel.ResourceSchedule ToModelData(this ObjModel.ResourceSchedule source)
        {
            return new DataModel.ResourceSchedule()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                IsDeleted = source.IsDeleted,
                LastModifiedBy = source.LastModifiedBy,
                LastModifiedDate = source.LastModifiedDate,
                ReservationDate = source.ReservationDate,
                ReservationStartDateTime = source.ReservationStartDateTime,
                ReservationEndDateTime = source.ReservationEndDateTime,
                ReservationNotes = source.ReservationNotes,
                ReservedByUserId = source.ReservedByUserId,
                ReservedByUserEmail = source.ReservedByUserEmail,
                ReservedForUser = source.ReservedForUser,
                ReservedOnDateTime = source.ReservedOnDateTime,
                ResourceId = source.ResourceId,
                ResourceScheduleId = source.ResourceScheduleId
            };
        }

        public static DataModel.Resource ToModelData(this ObjModel.Resource source)
        {
            return new DataModel.Resource()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                IsDeleted = source.IsDeleted,
                LastModifiedBy = source.LastModifiedBy,
                LastModifiedDate = source.LastModifiedDate,
                ResourceId = source.ResourceId,
                Description = source.Description,
                ImageLink = source.ImageLink,
                ImageLinkThumb = source.ImageLinkThumb,
                IsActive = source.IsActive,
                Name = source.Name
            };
        }

        public static DataModel.User ToModelData(this ObjModel.User source)
        {
            return new DataModel.User()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                IsDeleted = source.IsDeleted,
                LastModifiedBy = source.LastModifiedBy,
                LastModifiedDate = source.LastModifiedDate,
                Email = source.Email,
                InstallationId = source.InstallationId,
                IsActive = source.IsActive,
                LastLoginDate = source.LastLoginDate,
                Name = source.Name,
                UserId = source.UserId,
                UserName = source.UserName
            };
        }

        public static ObjModel.ResourceSchedule ToModelObj(this DataModel.ResourceSchedule source)
        {
            return new ObjModel.ResourceSchedule()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                IsDeleted = source.IsDeleted,
                LastModifiedBy = source.LastModifiedBy,
                LastModifiedDate = source.LastModifiedDate,
                ReservationDate = source.ReservationDate,
                ReservationStartDateTime = source.ReservationStartDateTime,
                ReservationEndDateTime = source.ReservationEndDateTime,
                ReservationNotes = source.ReservationNotes,
                ReservedByUserId = source.ReservedByUserId,
                ReservedByUserEmail = source.ReservedByUserEmail,
                ReservedForUser = source.ReservedForUser,
                ReservedOnDateTime = source.ReservedOnDateTime,
                ResourceId = source.ResourceId,
                ResourceScheduleId = source.ResourceScheduleId
            };
        }

        public static ObjModel.Resource ToModelObj(this DataModel.Resource source)
        {
            return new ObjModel.Resource()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                IsDeleted = source.IsDeleted,
                LastModifiedBy = source.LastModifiedBy,
                LastModifiedDate = source.LastModifiedDate,
                ResourceId = source.ResourceId,
                Description = source.Description,
                ImageLink = source.ImageLink,
                ImageLinkThumb = source.ImageLinkThumb,
                IsActive = source.IsActive,
                Name = source.Name
            };
        }

        public static ObjModel.User ToModelObj(this DataModel.User source)
        {
            return new ObjModel.User()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                IsDeleted = source.IsDeleted,
                LastModifiedBy = source.LastModifiedBy,
                LastModifiedDate = source.LastModifiedDate,
                Email = source.Email,
                InstallationId = source.InstallationId,
                IsActive = source.IsActive,
                LastLoginDate = source.LastLoginDate,
                Name = source.Name,
                UserId = source.UserId,
                UserName = source.UserName
            };
        }
    }
}