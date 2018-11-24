using System;
using System.Collections.Generic;
using System.Text;

namespace ResSched.Models
{
    public enum MenuItemType
    {
        Browse,
        About,
        Login,
        MyReservations
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
