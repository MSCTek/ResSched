using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace ResSched.Models.MeetupEvents
{
    public static class MeetupExtensions
    {
        public static ObservableCollection<Result> SortByTime(this IEnumerable<Result> source, ListSortDirection sortDirection)
        {
            var retVal = new ObservableCollection<Result>();

            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (sortDirection == ListSortDirection.Ascending)
            {
                retVal = new ObservableCollection<Result>(source.OrderBy(x => x.time));
            }
            else
            {
                retVal = new ObservableCollection<Result>(source.OrderByDescending(x => x.time));
            }

            return retVal;
        }
    }
}