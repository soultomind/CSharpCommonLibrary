using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Reflection
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class AssemblyBuildDateAttribute : System.Attribute
    {
        public DateTime DateTime { get; }

        public AssemblyBuildDateAttribute()
        {
            DateTime = DateTime.Now;
        }

        /// <summary>
        /// <paramref name="value"/> 값은 yyyyMmdd 포맷형태로 설정
        /// </summary>
        /// <param name="value"></param>
        public AssemblyBuildDateAttribute(string value)
            : this(value, "yyyyMMdd")
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        public AssemblyBuildDateAttribute(string value, string format)
        {
            DateTime = DateTime.ParseExact(
                value,
                format,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None
            );
        }
    }
}
