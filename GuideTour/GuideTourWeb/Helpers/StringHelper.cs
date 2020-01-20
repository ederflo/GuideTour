using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Helpers
{
    public class StringHelper
    {
        public static string Slice(string s, int maxLength, bool withDot = false)
        {
            string endling = withDot ? "." : "";
            string result = s;
            int lengthOfName = result.Length;
            if (lengthOfName > maxLength)
                result = result.Substring(0, maxLength - endling.Length) + endling;
            return result;
        }
    }
}
