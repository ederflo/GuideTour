using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuideTourData;
using GuideTourData.Models;
using GuideTourLogic.Logics;
using GuideTourWeb.Models.TourViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GuideTourWeb.Controllers
{
    public class TourController : Controller
    {
        public IActionResult Index()
        {
            TourLogic tourLogic = new TourLogic();
            TeamLogic teamLogic = new TeamLogic();
            List<Tour> tours = tourLogic.Get();
            IndexTourViewModel viewModel = new IndexTourViewModel();
            if (tours != null)
            {
                viewModel.NotStarted = tours.FindAll(x => x.StartedTour == null && x.EndedTour == null);
                viewModel.Started = tours.FindAll(x => x.StartedTour != null && x.EndedTour == null);
                viewModel.Teams = teamLogic.Get();
            }

            viewModel.Started = viewModel.Started.OrderBy(x => x.StartedTour).ToList();
            return View(viewModel);
        }

        [HttpPatch]
        public Tour StartTour(string id = "")
        {
            TourLogic tourLogic = new TourLogic();
            return tourLogic.StartTour(id);
        }

        [HttpPatch]
        public Tour CompleteTour(string id = "")
        {
            TourLogic tourLogic = new TourLogic();
            return tourLogic.CompleteTour(id);
        }

        [HttpPost]
        public Tour NewTour (IndexTourViewModel viewModel)
        {
            TourLogic tourLogic = new TourLogic();
            Tour tour = new Tour()
            {
                Id = null,
                EndedTour = null,
                GuideName = viewModel.GuideName,
                GuideTeam = viewModel.GuideTeam,
                StartedTour = null,
                VisitorName = viewModel.VisitorName
            };
            return tourLogic.Add(tour);
        }
    }
}