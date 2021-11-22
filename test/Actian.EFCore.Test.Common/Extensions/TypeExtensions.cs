using System;
using System.Linq;

namespace Actian.EFCore.TestUtilities
{
    public static class TypeExtensions
    {
        public static string PrettyName(this Type t, bool fullName = false) => t.IsGenericType
            ? $"{t.GenericTypeName(fullName)}<{string.Join(", ", t.GetGenericArguments().Select(a => a.PrettyName(fullName)))}>"
            : t.Name;

        private static string GenericTypeName(this Type t, bool fullName)
        {
            var genericTypeName = fullName ? t.GetGenericTypeDefinition().FullName : t.GetGenericTypeDefinition().Name;
            return genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
        }
    }
}
