using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ResSched.Helpers
{
    public static class ExtensionMethods
    {
        //extension method for enums
        public static string ToDescription(this Enum en)
        {
            return en.ToString().Replace('_', ' ');
        }
    }
}
