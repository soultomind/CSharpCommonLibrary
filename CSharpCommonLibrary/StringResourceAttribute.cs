﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary
{
    /// <summary>
    /// 문자열 리소스 애트리뷰트
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class StringResourceAttribute : System.Attribute
    {
        /// <summary>
        /// 포맷
        /// <para>{0} ... {9} 형태의 인자를 필요로 하는 포맷 문자열이 있을경우 포맷으로 간주</para>
        /// </summary>
        public string Format { get; private set; } = String.Empty;

        /// <summary>
        /// 메시지
        /// </summary>
        public string Message { get; private set; } = String.Empty;

        private StringResourceAttribute() { }

        public static StringResourceAttribute ToFormat(string format)
        {
            return new StringResourceAttribute() { Format = format };
        }

        public static StringResourceAttribute ToMessage(string message)
        {
            return new StringResourceAttribute() { Message = message };
        }
    }
}
