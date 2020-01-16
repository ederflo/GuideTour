using System;
using System.Collections.Generic;
using System.Text;

namespace GuideTourData.Models
{
    public class ImportTeam
    {
        public string Name { get; set; }

        public List<Guide> Guides { get; set; } = new List<Guide>();
    }
}
