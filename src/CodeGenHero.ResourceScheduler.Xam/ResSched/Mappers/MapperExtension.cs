using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ResSched.Mappers
{
    public static class MapperExtension
    {
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