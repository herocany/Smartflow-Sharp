using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Common
{
    [Serializable]
    public class Paging
    {
        public int Page
        {
            get;
            set;
        }

        public int Limit
        {
            get;
            set;
        }

     

        public string Arg
        {
            get;
            set;
        }

        public Dictionary<String, String> Get()
        {
            Dictionary<String, String> dic = new Dictionary<String, String>();
            if (!String.IsNullOrEmpty(this.Arg))
            {
                IDictionary<String, JToken> keyValuePairs =
                    Newtonsoft.Json.JsonConvert.DeserializeObject(this.Arg, typeof(JObject)) as JObject;

                if (keyValuePairs != null)
                {
                    foreach (String key in keyValuePairs.Keys)
                    {
                        string s = keyValuePairs[key].ToString().Trim();
                        if (!String.IsNullOrEmpty(s))
                        {
                            dic.Add(key, s);
                        }
                    }
                }
            }
            return dic;
        }

    }
}
