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

        public double AverageToursPerGuide { get; set; }

        public TimeSpan AverageTourDuration { get; set; }

        public List<TourViewModel> FinishedTours { get; set; }
    }
}
