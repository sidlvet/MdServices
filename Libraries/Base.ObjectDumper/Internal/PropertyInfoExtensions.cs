using System;
using System.Reflection;

namespace Mesco.Base
{
    internal static class PropertyInfoExtensions
    {
        internal static object TryGetValue(this PropertyInfo property, object element)
        {
            object value;
            try
            {
                value = property.GetValue(element);
            }
            catch (Exception ex)
            {
                value = $"{{{ex.Message}}}";
            }

            return value;
        }
    }
}
