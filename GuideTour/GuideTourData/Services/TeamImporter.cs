using GuideTourData.Models;
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

        public static List<ImportTeam> LoadJson(string filepath)
        {
            List<ImportTeam> items = new List<ImportTeam>();
            using (StreamReader r = new StreamReader(filepath))
            {
                var json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<ImportTeam>>(json);
            }

            return items;
        }
    }
}
