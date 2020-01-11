using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuideTourData.Models
{
    public class Tour
    {
        public string Id { get; set; }

        public string GuideName { get; set; }

        public string GuideTeam { get; set; }

        public DateTime? StartedTour { get; set; }

        public DateTime? EndedTour { get; set; }

        public string VisitorName { get; set; }
    }
}
