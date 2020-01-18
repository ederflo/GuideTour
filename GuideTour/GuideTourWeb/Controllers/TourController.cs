﻿using System;
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
using MongoDB.Bson;

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


            //GuideTourLogic.Services.TeamImporter teamImporter = new GuideTourLogic.Services.TeamImporter(_ddb);
            //bool success = await teamImporter.ImportTeams();

            List<Team> teams = await teamLogic.Get();
            List<Guide> guides = await guideLogic.Get();
            List<Tour> tours = await tourLogic.Get();
            IndexTourViewModel viewModel = new IndexTourViewModel();

            if (tours != null && teams != null && guides != null)
            {
                List<TourViewModel> tourVMs = TourHelper.ToViewModel(tours, guides, teams); 
                viewModel.NotStarted = tourVMs.FindAll(x => x.StartedTour == null && x.EndedTour == null);
                viewModel.Started = tourVMs.FindAll(x => x.StartedTour != null && x.EndedTour == null);
                viewModel.Teams = TeamHelper.AssignGuidesToTeam(teams, guides);
            }
            viewModel.Started = viewModel.Started.OrderBy(x => x.StartedTour).ToList();
            return View(viewModel);
        }

        [HttpPatch]
        public async Task<TourViewModel> StartTourAsync(string id = "")
        {
            TourLogic tourLogic = new TourLogic(_ddb);
            TourHelper tourHelper = new TourHelper(_ddb);
            TourViewModel result = null;
            Tour tour = null;
            if ((tour = await tourLogic.StartTour(id)) != null)
                if ((result = await tourHelper.ToViewModel(tour)) != null)
                    await _hubcontext.Clients.All.SendAsync("TourStarted", result);
            return result;
        }

        [HttpPatch]
        public async Task<TourViewModel> CompleteTour(string id = "")
        {
            TourLogic tourLogic = new TourLogic(_ddb);
            TourHelper tourHelper = new TourHelper(_ddb);
            TourViewModel result = null;
            Tour tour = null;
            if ((tour = await tourLogic.CompleteTour(id)) != null)
                if ((result = await tourHelper.ToViewModel(tour)) != null)
                    await _hubcontext.Clients.All.SendAsync("TourCompleted", result);
            return result;
        }

        [HttpPatch]
        public async Task<TourViewModel> CancelTourAsync(string id = "")
        {
            TourLogic tourLogic = new TourLogic(_ddb);
            TourHelper tourHelper = new TourHelper(_ddb);
            TourViewModel result = null;
            Tour tour;
            if ((tour = await tourLogic.CancelTour(id)) != null)
                if ((result = await tourHelper.ToViewModel(tour)) != null)
                    await _hubcontext.Clients.All.SendAsync("TourCancelled", result);
            return result;
        }

        [HttpPost]
        public async Task<TourViewModel> NewTourAsync (IndexTourViewModel viewModel)
        {
            TourLogic tourLogic = new TourLogic(_ddb);
            TourHelper tourHelper = new TourHelper(_ddb);
            TourViewModel result = null;
            Tour tour = new Tour()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                EndedTour = null,
                StartedTour = null,
                VisitorName = viewModel.VisitorName,
                Canceld = false,
                GuideId = viewModel.GuideId
            };
            if ((tour = await tourLogic.Add(tour)) != null)
                if ((result = await tourHelper.ToViewModel(tour)) != null)
                    await _hubcontext.Clients.All.SendAsync("NewRequestedTour", result);
            return result;
        }
    }
}