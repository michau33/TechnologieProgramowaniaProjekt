using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TpProjektModel.Reflection.Extensions
{
    internal static class Extensions
    {
        internal static IEnumerable<Attribute> GetAttributes<T>(this T info, bool inherit) where T : ICustomAttributeProvider
        {
            return info.GetCustomAttributes(inherit).Cast<Attribute>();
        }
    }
}
