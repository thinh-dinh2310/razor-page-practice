﻿using System.Collections.Generic;

namespace Web.Common
{
    public class Helper
    {
        public static string StringFormat(string format, IDictionary<string, object> values)
        {
            if (values == null) return format;
            foreach (var p in values)
            {
                format = format.Replace("{" + p.Key + "}", p.Value?.ToString());
            }
            return format;
        }
    }
}
