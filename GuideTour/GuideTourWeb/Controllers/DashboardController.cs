using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuideTourData.Models;
using GuideTourData.Services;
using GuideTourLogic.Logics;
using GuideTourTestData.DataProvider;
using GuideTourWeb.Models.DashboardViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GuideTourWeb.Controllers
{
    public class DashboardController : Controller
    {
        public readonly IDocumentDbRepository _ddb;

        public DashboardController(IDocumentDbRepository ddb)
        {
            _ddb = ddb;
        }

        [Route("Dashboard/Overview")]
        public async Task<IActionResult> Index()
        {
            GuideLogic guideLogic = new GuideLogic(_ddb);
            TourLogic tourLogic = new TourLogic(_ddb);
            List<Guide> guides = await guideLogic.Get();
            List<Tour> tours = await tourLogic.Get();
            TimeSpan entireTourDuration = new TimeSpan();

            IndexDashboardViewModel viewModel = new IndexDashboardViewModel();
            List<Tour> finishedTours = new List<Tour>();
            List<Tour> ongoingTours = new List<Tour>();
            List<Tour> canceldTours = new List<Tour>();
            List<Tour> fromIfGuideApp = new List<Tour>();

            if (tours == null || tours.Count <= 0)
            {
                await tourLogic.Add(TourGenerator.Generate(guides));
            }

            foreach (Tour t in tours)
            {
                if (t.Canceled)
                    canceldTours.Add(t);
                if (t.StartedTour != null && t.EndedTour == null)
                    ongoingTours.Add(t);
                if (t.StartedTour != null && t.EndedTour != null)
                    finishedTours.Add(t);
                if (!string.IsNullOrEmpty(t.IfGuideAppId))
                    fromIfGuideApp.Add(t);
                if (t.EndedTour != null)
                {
                    TimeSpan tsOfTour = new TimeSpan(t.EndedTour.Value.Ticks - t.StartedTour.Value.Ticks);
                    entireTourDuration = entireTourDuration.Add(tsOfTour);
                }
            }

            viewModel.CntFinishedTours = finishedTours.Count;
            viewModel.CntOngoingTours = ongoingTours.Count;
            viewModel.AverageToursPerGuide = Math.Round((double)tours.Count / guides.Count, 2);
            viewModel.AverageTourDuration = entireTourDuration.Divide(tours.Count);

            return View(viewModel);
        }

        [Route("Dashboard/Overview/Tours")]
        public IActionResult OverviewTours()
        {
            return View();
        }

        [Route("Dashboard/Overview/Teams")]
        public IActionResult OverviewTeams()
        {
            return View();
        }

        [Route("Dashboard/Overview/Guides")]
        public IActionResult OverviewGuides()
        {
            return View();
        }

        [Route("Dashboard/Data/Tours")]
        public IActionResult DataTours()
        {
            return View();
        }

        [Route("Dashboard/Data/Teams")]
        public IActionResult DataTeams()
        {
            return View();
        }

        [Route("Dashboard/Data/Guides")]
        public IActionResult DataGuides()
        {
            return View();
        }
    }
}