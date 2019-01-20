using CodeGenHero.ResourceScheduler.DTO.RS;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ResSched.Mappers
{
    public static class MapperExtension
    {
        public static ResourceSchedule ToDto(this CodeGenHero.ResourceScheduler.Xam.ModelData.RS.PendingResourceSchedule source)
        {
            return new ResourceSchedule()
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

        public static ObservableCollection<T> ToObservableCollection<T>(this IList<T> list)
        {
            var retVal = new ObservableCollection<T>();
            foreach (var item in list)
            {
                retVal.Add(item);
            }
            return retVal;
        }
    }
}