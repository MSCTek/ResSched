using GalaSoft.MvvmLight;

namespace CodeGenHero.ResourceScheduler.Xam.ModelObj.RS
{
    public partial class ResourceSchedule : ObservableObject
    {
        private System.DateTime _reservationDate;

        public System.DateTime ReservationDate
        {
            get { return _reservationDate; }
            set
            {
                Set<System.DateTime>(() => ReservationDate, ref _reservationDate, value);
            }
        }

        public string ReservationEndDateTimeDisplay { get { return ReservationEndDateTime.ToString("hh:mm tt"); } }
        public string ReservationStartDateTimeDisplay { get { return ReservationStartDateTime.ToString("d MMM yyyy  hh:mm tt"); } }
        public string ReservedOnDateTimeDisplay { get { return ReservedOnDateTime.ToString("d MMM yyyy  hh:mm tt"); } }
    }
}