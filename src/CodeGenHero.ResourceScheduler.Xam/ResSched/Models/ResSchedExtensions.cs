using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ResSched.Models
{
    public static class ResSchedExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return new ObservableCollection<T>(source);
        }
    }
}