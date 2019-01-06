namespace ResSched.Models
{
    public enum MenuItemType
    {
        Browse = 0,
        About = 1,
        Login = 2,
        MyReservations = 3,
        Events = 4
    }

    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}