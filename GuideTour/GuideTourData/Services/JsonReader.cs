using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GuideTourData.Services
{
    public class JsonReader<T>
    {

        public static List<T> ReadJson(string filepath)
        {
            List<T> items = new List<T>();
            using (StreamReader r = new StreamReader(filepath))
            {
                var json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<T>>(json);
            }

            return items;
        }
    }
}
