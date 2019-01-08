using dataRS = CodeGenHero.ResourceScheduler.Xam.ModelData.RS;
using dtoRS = CodeGenHero.ResourceScheduler.DTO.RS;
using objRS = CodeGenHero.ResourceScheduler.Xam.ModelObj.RS;

namespace CodeGenHero.ResourceScheduler.Xam
{
    public static partial class ModelMapper
    {
        #region ModelObj to ModelDto

        public static dtoRS.Resource ToDto(this objRS.Resource source)
        {
            return new dtoRS.Resource()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                Description = source.Description,
                Id = source.Id,
                ImageLink = source.ImageLink,
                ImageLinkThumb = source.ImageLinkThumb,
                IsActive = source.IsActive,
                IsDeleted = source.IsDeleted,
                Name = source.Name,
                UpdatedBy = source.UpdatedBy,
                UpdatedDate = source.UpdatedDate,
            };
        }

        public static dtoRS.ResourceSchedule ToDto(this objRS.ResourceSchedule source)
        {
            return new dtoRS.ResourceSchedule()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                Id = source.Id,
                IsDeleted = source.IsDeleted,
                ReservationEndDateTime = source.ReservationEndDateTime,
                ReservationNotes = source.ReservationNotes,
                ReservationStartDateTime = source.ReservationStartDateTime,
                ReservedByUserId = source.ReservedByUserId,
                ReservedForUser = source.ReservedForUser,
                ReservedOnDateTime = source.ReservedOnDateTime,
                ResourceId = source.ResourceId,
                UpdatedBy = source.UpdatedBy,
                UpdatedDate = source.UpdatedDate,
            };
        }

        public static dtoRS.User ToDto(this objRS.User source)
        {
            return new dtoRS.User()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                Email = source.Email,
                Id = source.Id,
                InstallationId = source.InstallationId,
                IsActive = source.IsActive,
                IsDeleted = source.IsDeleted,
                LastLoginDate = source.LastLoginDate,
                Name = source.Name,
                UpdatedBy = source.UpdatedBy,
                UpdatedDate = source.UpdatedDate,
                UserName = source.UserName,
            };
        }

        #endregion ModelObj to ModelDto

        #region ModelData to ModelDto

        public static dtoRS.Resource ToDto(this dataRS.Resource source)
        {
            return new dtoRS.Resource()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                Description = source.Description,
                Id = source.Id,
                ImageLink = source.ImageLink,
                ImageLinkThumb = source.ImageLinkThumb,
                IsActive = source.IsActive,
                IsDeleted = source.IsDeleted,
                Name = source.Name,
                UpdatedBy = source.UpdatedBy,
                UpdatedDate = source.UpdatedDate,
            };
        }

        public static dtoRS.ResourceSchedule ToDto(this dataRS.ResourceSchedule source)
        {
            return new dtoRS.ResourceSchedule()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                Id = source.Id,
                IsDeleted = source.IsDeleted,
                ReservationEndDateTime = source.ReservationEndDateTime,
                ReservationNotes = source.ReservationNotes,
                ReservationStartDateTime = source.ReservationStartDateTime,
                ReservedByUserId = source.ReservedByUserId,
                ReservedForUser = source.ReservedForUser,
                ReservedOnDateTime = source.ReservedOnDateTime,
                ResourceId = source.ResourceId,
                UpdatedBy = source.UpdatedBy,
                UpdatedDate = source.UpdatedDate,
            };
        }

        public static dtoRS.User ToDto(this dataRS.User source)
        {
            return new dtoRS.User()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                Email = source.Email,
                Id = source.Id,
                InstallationId = source.InstallationId,
                IsActive = source.IsActive,
                IsDeleted = source.IsDeleted,
                LastLoginDate = source.LastLoginDate,
                Name = source.Name,
                UpdatedBy = source.UpdatedBy,
                UpdatedDate = source.UpdatedDate,
                UserName = source.UserName,
            };
        }

        #endregion ModelData to ModelDto

        #region ModelDto to ModelObj

        public static objRS.Resource ToModelObj(this dtoRS.Resource source)
        {
            return new objRS.Resource()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                Description = source.Description,
                Id = source.Id,
                ImageLink = source.ImageLink,
                ImageLinkThumb = source.ImageLinkThumb,
                IsActive = source.IsActive,
                IsDeleted = source.IsDeleted,
                Name = source.Name,
                UpdatedBy = source.UpdatedBy,
                UpdatedDate = source.UpdatedDate,
            };
        }

        public static objRS.ResourceSchedule ToModelObj(this dtoRS.ResourceSchedule source)
        {
            return new objRS.ResourceSchedule()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                Id = source.Id,
                IsDeleted = source.IsDeleted,
                ReservationEndDateTime = source.ReservationEndDateTime,
                ReservationNotes = source.ReservationNotes,
                ReservationStartDateTime = source.ReservationStartDateTime,
                ReservedByUserId = source.ReservedByUserId,
                ReservedForUser = source.ReservedForUser,
                ReservedOnDateTime = source.ReservedOnDateTime,
                ResourceId = source.ResourceId,
                UpdatedBy = source.UpdatedBy,
                UpdatedDate = source.UpdatedDate,
                ReservationDate = source.ReservationStartDateTime.Date
            };
        }

        public static objRS.User ToModelObj(this dtoRS.User source)
        {
            return new objRS.User()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                Email = source.Email,
                Id = source.Id,
                InstallationId = source.InstallationId,
                IsActive = source.IsActive,
                IsDeleted = source.IsDeleted,
                LastLoginDate = source.LastLoginDate,
                Name = source.Name,
                UpdatedBy = source.UpdatedBy,
                UpdatedDate = source.UpdatedDate,
                UserName = source.UserName,
            };
        }

        #endregion ModelDto to ModelObj

        #region ModelDto to ModelData

        public static dataRS.Resource ToModelData(this dtoRS.Resource source)
        {
            return new dataRS.Resource()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                Description = source.Description,
                Id = source.Id,
                ImageLink = source.ImageLink,
                ImageLinkThumb = source.ImageLinkThumb,
                IsActive = source.IsActive,
                IsDeleted = source.IsDeleted,
                Name = source.Name,
                UpdatedBy = source.UpdatedBy,
                UpdatedDate = source.UpdatedDate,
            };
        }

        public static dataRS.ResourceSchedule ToModelData(this dtoRS.ResourceSchedule source)
        {
            return new dataRS.ResourceSchedule()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                Id = source.Id,
                IsDeleted = source.IsDeleted,
                ReservationEndDateTime = source.ReservationEndDateTime,
                ReservationNotes = source.ReservationNotes,
                ReservationStartDateTime = source.ReservationStartDateTime,
                ReservedByUserId = source.ReservedByUserId,
                ReservedForUser = source.ReservedForUser,
                ReservedOnDateTime = source.ReservedOnDateTime,
                ResourceId = source.ResourceId,
                UpdatedBy = source.UpdatedBy,
                UpdatedDate = source.UpdatedDate,
                ReservationDate = source.ReservationStartDateTime.Date
            };
        }

        public static dataRS.User ToModelData(this dtoRS.User source)
        {
            return new dataRS.User()
            {
                CreatedBy = source.CreatedBy,
                CreatedDate = source.CreatedDate,
                Email = source.Email,
                Id = source.Id,
                InstallationId = source.InstallationId,
                IsActive = source.IsActive,
                IsDeleted = source.IsDeleted,
                LastLoginDate = source.LastLoginDate,
                Name = source.Name,
                UpdatedBy = source.UpdatedBy,
                UpdatedDate = source.UpdatedDate,
                UserName = source.UserName,
            };
        }

        #endregion ModelDto to ModelData
    }
}