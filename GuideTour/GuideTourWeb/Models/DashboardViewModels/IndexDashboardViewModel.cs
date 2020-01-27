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

        public int CntCanceledTours { get; set; }

        public int CntIfGuideAppTours { get; set; }

        public int AverageToursPerGuide { get; set; }

        public List<TourViewModel> FinishedTours { get; set; }

        public TimeSpan AverageTourDuration { get; set; }
    }
}
