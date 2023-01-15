using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Extensions
{
    public static class AttributeExtension
    {
        /// <summary>
        /// <paramref name="sourceType"/> 
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="propertyName"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Attribute GetAttribute(Type sourceType, string propertyName, int index = 0)
        {
            if (sourceType == null)
            {
                throw new ArgumentNullException(nameof(sourceType));
            }

            if (!sourceType.IsClass)
            {
                throw new ArgumentException("!" + nameof(sourceType) + ".IsClass");
            }

            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (String.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException(nameof(propertyName) + " String.IsNullOrEmpty");
            }

            object[] attr = sourceType.GetProperty(propertyName).GetCustomAttributes(false);

            Attribute attribute = null;
            if (attr != null && attr.Length > 0)
            {
                if (attr.Length > index && -1 < index)
                {
                    return (Attribute)attr[index];
                }
            }
            return attribute;
        }

        public static Attribute GetAttribute(Type sourceType, Type findAttributeType, string propertyName)
        {
            if (sourceType == null)
            {
                throw new ArgumentNullException(nameof(sourceType));
            }

            if (!sourceType.IsClass)
            {
                throw new ArgumentException("!" + nameof(sourceType) + ".IsClass");
            }

            if (findAttributeType == null)
            {
                throw new ArgumentNullException(nameof(findAttributeType));
            }

            if (!findAttributeType.IsSubclassOf(typeof(Attribute)))
            {
                throw new ArgumentNullException("!" + nameof(findAttributeType) + "IsSubclassOf(typeof(Attribute))");
            }

            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (String.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException(nameof(propertyName) + " String.IsNullOrEmpty");
            }

            object[] attributes = sourceType.GetProperty(propertyName).GetCustomAttributes(false);

            Attribute attribute = null;
            if (attributes != null && attributes.Length > 0)
            {
                foreach (Attribute itemAttribute in attributes)
                {
                    if (Object.ReferenceEquals(itemAttribute.GetType(), findAttributeType))
                    {
                        attribute = itemAttribute;
                        break;
                    }
                }
            }

            return attribute;
        }
    }
}
