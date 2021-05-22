using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Common
{
    public class Utils
    {
        public static string BindQuot(string ids)
        {
            string[] RArry = ids.Split(',');
            string[] NRArray = new string[RArry.Length];
            for (int i = 0; i < RArry.Length; i++)
            {
                NRArray[i] = string.Format("'{0}'", RArry[i]);
            }
            return string.Join(",", NRArray);
        }

        public static IEnumerable<string> StringToList(string ids)
        {
            IList<string> results = new List<string>();
            if (!String.IsNullOrEmpty(ids))
            {
                results = ids.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList<string>();
            }
            return results;
        }

        public static dynamic ToDynamic(Dictionary<string, object> dict)
        {
            dynamic result = new System.Dynamic.ExpandoObject();

            foreach (var entry in dict)
            {
                (result as ICollection<KeyValuePair<string, object>>).Add(new KeyValuePair<string, object>(entry.Key, entry.Value));
            }

            return result;
        }

        public static string ToBase64String(string s)
        {
            byte[] byteArray = System.Text.UTF8Encoding.UTF8.GetBytes(s);

            return Convert.ToBase64String(byteArray);
        }
    }
}
