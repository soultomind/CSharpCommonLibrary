using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Reflection
{
    public static class AssemblyBuildInfo
    {
        public static bool TryGetBuildDate(Assembly assembly, out DateTime dateTime)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var attribute = assembly.GetCustomAttribute<AssemblyBuildDateAttribute>();
            if (attribute != null)
            {
                dateTime = attribute.DateTime;
                return true;
            }

            dateTime = default(DateTime);
            return false;
        }
    }
}
