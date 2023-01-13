using System;
using System.Collections.Generic;
using System.Text;

namespace MADBHR_Common.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNullOrEmpty(this object obj)
        {
            return (obj == null);
        }
        public static bool IsNotNullOrEmpty(this object obj)
        {
            return !IsNullOrEmpty(obj);
        }
    }
}
