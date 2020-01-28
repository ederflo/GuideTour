using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuideTourWeb.Models.DashboardViewModels
{
    public class TourOverviewViewModel
    {
        public int CntCanceledTours { get; set; }

        public int CntIfGuideAppTours { get; set; }
    }
}
