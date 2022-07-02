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
        /// <param name="thisObj"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T Attributes<T>(this Enum thisObj, int index = 0) where T : Attribute
        {
            var type = thisObj.GetType();
            var memberInfo = type.GetMember(thisObj.ToString());
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
    }
}
