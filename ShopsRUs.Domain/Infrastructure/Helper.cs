using System;
using System.Collections.Generic;
using System.Text;

namespace ShopsRUs.Domain.Infrastructure
{
    public static class Helper
    {
        public static T ParseEnum<T>(this string value)
        {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }
    }
}
