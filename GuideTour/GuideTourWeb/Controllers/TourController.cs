using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using GuideTourData;
using GuideTourData.DataAccess;
using GuideTourData.Models;
using GuideTourData.Services;
using GuideTourLogic.Logics;
using GuideTourWeb.Helpers.ObjectHelpers;
using GuideTourWeb.Hubs;
using GuideTourWeb.Models.TourViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GuideTourWeb.Controllers
{
    public class TourController : Controller
    {
        private readonly IDocumentDbRepository _ddb;
        private readonly IHubContext<TourHub> _hubcontext;

        public TourController(IHubContext<TourHub> hubcontext, IDocumentDbRepository ddb)
        {
            _hubcontext = hubcontext;
            _ddb = ddb;
        }

        public async Task<IActionResult> Index()
        {
            TourLogic tourLogic = new TourLogic(_ddb);
            TeamLogic teamLogic = new TeamLogic(_ddb);
            GuideLogic guideLogic = new GuideLogic(_ddb);


            List<Tour> tours = await tourLogic.Get();
            IndexTourViewModel viewModel = new IndexTourViewModel();
            if (tours != null)
            {
                //viewModel.NotStarted = tours.FindAll(x => x.StartedTour == null && x.EndedTour == null);
                //viewModel.Started = tours.FindAll(x => x.StartedTour != null && x.EndedTour == null);
                List<Team> teams = await teamLogic.Get();
                List<Guide> guides = await guideLogic.Get();
                viewModel.Teams = TeamHelper.AssignGuidesToTeam(teams, guides);
            }
            //viewModel.Started = viewModel.Started.OrderBy(x => x.StartedTour).ToList();
            return View(viewModel);
        }

        [HttpPatch]
        public async Task<Tour> StartTourAsync(string id = "")
        {
            TourLogic tourLogic = new TourLogic(_ddb);
            Tour tour = null;
            if ((tour = await tourLogic.StartTour(id)) != null) 
            {
                await _hubcontext.Clients.All.SendAsync("TourStarted", tour);
            }
            return tour;
        }

        [HttpPatch]
        public async Task<Tour> CompleteTour(string id = "")
        {
            TourLogic tourLogic = new TourLogic(_ddb);
            Tour tour = null;
            if ((tour = await tourLogic.CompleteTour(id)) != null)
            {
                await _hubcontext.Clients.All.SendAsync("TourCompleted", tour);
            }
            return tour;
        }

        [HttpPatch]
        public async Task<Tour> CancelTourAsync(string id = "")
        {
            TourLogic tourLogic = new TourLogic(_ddb);
            Tour tour = null;
            if ((tour = await tourLogic.CompleteTour(id)) != null)
            {
                await _hubcontext.Clients.All.SendAsync("TourCompleted", tour);
            }
            return tour;
        }

        [HttpPost]
        public async Task<Tour> NewTourAsync (IndexTourViewModel viewModel)
        {
            TourLogic tourLogic = new TourLogic(_ddb);
            Tour tour = new Tour()
            {
                Id = null,
                EndedTour = null,
                //GuideName = viewModel.GuideName,
                //uideTeam = viewModel.GuideTeam,
                StartedTour = null,
                VisitorName = viewModel.VisitorName
            };

            if (await tourLogic.Add(tour) != null)
            {
                await _hubcontext.Clients.All.SendAsync("NewRequestedTour", tour);
            }

            return tour;
        }
    }
}