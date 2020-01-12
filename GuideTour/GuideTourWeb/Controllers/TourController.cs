using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuideTourData;
using GuideTourData.Models;
using GuideTourLogic.Logics;
using GuideTourWeb.Hubs;
using GuideTourWeb.Models.TourViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GuideTourWeb.Controllers
{
    public class TourController : Controller
    {
        private readonly IHubContext<TourHub> _hubcontext;

        public TourController(IHubContext<TourHub> hubcontext)
        {
            _hubcontext = hubcontext;
        }

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
            Tour tour = null;
            if ((tour = tourLogic.StartTour(id)) != null) 
            {
                _hubcontext.Clients.All.SendAsync("TourStarted", tour);
            }
            return tour;
        }

        [HttpPatch]
        public Tour CompleteTour(string id = "")
        {
            TourLogic tourLogic = new TourLogic();
            Tour tour = null;
            if ((tour = tourLogic.CompleteTour(id)) != null)
            {
                _hubcontext.Clients.All.SendAsync("TourCompleted", tour);
            }
            return tour;
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

            if (tourLogic.Add(tour) != null)
            {
                _hubcontext.Clients.All.SendAsync("NewRequestedTour", tour);
            }

            return tour;
        }
    }
}