using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        /// 애트리뷰트 정보를 반환합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisEnum"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T Attributes<T>(this Enum thisEnum, int index = 0) where T : Attribute
        {
            var type = thisEnum.GetType();
            var memberInfo = type.GetMember(thisEnum.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

            if (attributes.Length > index && -1 < index)
            {
                return (T)attributes[index];
            }
            else
            {
                return null;
            }
        }

        public static bool HasFlag(this Enum thisEnum, Enum value)
        {
            if (thisEnum.GetType() != value.GetType())
            {
                throw new ArgumentException(nameof(value));
            }

            int thisEnumFlag = Convert.ToInt32(thisEnum);
            int valueEnumFlag = Convert.ToInt32(value);

            return (thisEnumFlag & valueEnumFlag) == valueEnumFlag;
        }
    }
}
