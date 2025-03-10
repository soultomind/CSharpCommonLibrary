using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Net35.Extension
{
    public static class EnumExtension
    {
        public static bool HasFlag(this Enum @this, Enum value)
        {
            if (@this.GetType() != value.GetType())
            {
                throw new ArgumentException(nameof(value));
            }

            int thisEnumFlag = Convert.ToInt32(@this);
            int valueEnumFlag = Convert.ToInt32(value);

            return (thisEnumFlag & valueEnumFlag) == valueEnumFlag;
        }
    }
}
