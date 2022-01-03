using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonLibrary.Web
{
    public class HttpParameter
    {
        public string Name { get; set; }
        public List<string> Values { get; private set; }

        public HttpParameter(string name, string[] values)
        {
            Name = name;
            Values = new List<string>(values);
        }

        public HttpParameter(string name)
        {
            Name = name;
            Values = new List<string>();
        }

        public void AddValue(string value)
        {
            Values.Add(value);
        }
    }
}
