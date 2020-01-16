using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GuideTourData.Services
{
    public class TeamImporter
    {
        private readonly IDocumentDbRepository _ddb;

        public TeamImporter(IDocumentDbRepository ddb)
        {
            _ddb = ddb;
        }

        public static void LoadJson(string filepath)
        {
            using (StreamReader r = new StreamReader(filepath))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<object>>(json);
                foreach (var item in items)
                {
                    // Console.WriteLine("{0} {1}", item.temp, item.vcc);
                }
            }
        }
    }
}
