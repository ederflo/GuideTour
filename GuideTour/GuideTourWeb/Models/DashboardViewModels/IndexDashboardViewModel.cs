using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Models.DashboardViewModels
{
    public class IndexDashboardViewModel
    {
        public int CntFinishedTours { get; set; }

        public int CntOngoingTours { get; set; }

        public TimeSpan WholeToursDuration { get; set; }

        public TimeSpan AverageTourDuration { get; set; }

        public double AverageToursPerGuide { get; set; }

        public double AverageToursPerTeam { get; set; }

        public int CntCanceledTours { get; set; }

        public int CntGuides { get; set; }

        public DateTime? FirstTour { get; set; }

        public DateTime? LastTour  { get; set; }
    }
}
