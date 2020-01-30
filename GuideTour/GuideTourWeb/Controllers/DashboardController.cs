using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuideTourData.Models;
using GuideTourData.Services;
using GuideTourLogic.Logics;
using GuideTourTestData.DataProvider;
using GuideTourWeb.Helpers.ObjectHelpers;
using GuideTourWeb.Models;
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
            TeamLogic teamLogic = new TeamLogic(_ddb);
            TimeSpan entireTourDuration = new TimeSpan();
            DateTime? firstTour = new DateTime();
            DateTime? lastTour = new DateTime();

            IndexDashboardViewModel viewModel = new IndexDashboardViewModel();
            List<Tour> finishedTours = new List<Tour>();
            List<Tour> ongoingTours = new List<Tour>();
            List<Tour> canceldTours = new List<Tour>();
            List<Tour> fromIfGuideApp = new List<Tour>();
            try
            {
                List<Guide> guides = await guideLogic.Get();
                List<Tour> tours = await tourLogic.Get();
                List<Team> teams = await teamLogic.Get();
                //if (tours == null || tours.Count <= 0)
                    //await tourLogic.Add(TourGenerator.Generate(guides));
                if (tours != null)
                    tours.RemoveAll(x => x.StartedTour == null);
                firstTour = tours.Count > 0 ? tours[0].StartedTour : null;
                lastTour = tours.Count > 0 ? tours[0].StartedTour : null;
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
                    if (t.StartedTour.HasValue)
                    {
                        if (t.StartedTour.Value.Ticks < firstTour.Value.Ticks)
                            firstTour = t.StartedTour;
                        if (t.StartedTour.Value.Ticks > lastTour.Value.Ticks)
                            lastTour = t.StartedTour;
                    }
                        
                }
                viewModel.CntFinishedTours = finishedTours.Count;
                viewModel.CntOngoingTours = ongoingTours.Count;
                viewModel.WholeToursDuration = entireTourDuration;
                viewModel.AverageTourDuration = entireTourDuration.TotalSeconds > 0 ? entireTourDuration.Divide(tours.Count) : new TimeSpan();
                viewModel.AverageToursPerGuide = guides.Count > 0 ? Math.Round((double)tours.Count / guides.Count, 2) : 0;
                viewModel.AverageToursPerTeam = teams.Count > 0 ? Math.Round((double)tours.Count / teams.Count, 2) : 0;
                viewModel.CntCanceledTours = canceldTours.Count;
                viewModel.CntGuides = guides.Count;
                viewModel.FirstTour = firstTour;
                viewModel.LastTour = lastTour;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            

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
        public async Task<IActionResult> DataTours()
        {
            TourLogic tourLogic = new TourLogic(_ddb);
            TeamLogic teamLogic = new TeamLogic(_ddb);
            GuideLogic guideLogic = new GuideLogic(_ddb);
            List<TourViewModel> viewModels = new List<TourViewModel>();
            try
            {
                List<Team> teams = await teamLogic.Get();
                List<Guide> guides = await guideLogic.Get();
                List<Tour> tours = await tourLogic.Get();
                if (tours != null && teams != null && guides != null)
                {
                    viewModels = TourHelper.ToViewModel(tours, guides, teams);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return View(viewModels);
        }

        [Route("Dashboard/Data/Teams")]
        public async Task<IActionResult> DataTeams()
        {
            TeamLogic teamLogic = new TeamLogic(_ddb);
            List<Team> teams = await teamLogic.Get();
            DataTeamsDashboardViewModel viewModel = new DataTeamsDashboardViewModel();
            viewModel.Teams = teams;
            return View(viewModel);
        }

        [Route("Dashboard/Data/Guides")]
        public async Task<IActionResult> DataGuides()
        {
            GuideLogic guideLogic = new GuideLogic(_ddb);
            TeamLogic teamLogic = new TeamLogic(_ddb);
            List<Guide> guides = await guideLogic.Get();
            List<Team> teams = await teamLogic.Get();
            List<TeamViewModel> teamVMs = new List<TeamViewModel>();
            teamVMs = TeamHelper.AssignGuidesToTeam(teams, guides);
            DataGuidesDashboardViewModel viewModel = new DataGuidesDashboardViewModel();
            foreach (var t in teamVMs)
            {
                foreach (Guide g in t.Guides)
                {
                    GuideDataTableRow row = new GuideDataTableRow()
                    {
                        GuideId = g.Id,
                        Name = g.Name,
                        Email = g.Email,
                        TeamId = g.TeamId,
                        Teamname = t.TeamName
                    };
                    viewModel.Rows.Add(row);
                }
                
                
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<List<object>> GetChartDataForOverview()
        {
            TourLogic tourLogic = new TourLogic(_ddb);
            GuideLogic guideLogic = new GuideLogic(_ddb);
            TeamLogic teamLogic = new TeamLogic(_ddb);

            List<Tour> tours = await tourLogic.Get();
            List<Guide> guides = await guideLogic.Get();
            List<Team> teams = await teamLogic.Get();
            List<object> result = new List<object>();

            if (tours != null)
            {
                tours.RemoveAll(x => x.StartedTour == null);
                List<int> cntOfToursPerHalfHour = new List<int>();
                cntOfToursPerHalfHour = TourLogic.GetToursPerHalfHour(tours).Values.ToList();

                List<int> toursDuration = new List<int> { 0, 0, 0 };

                List<TourViewModel> tourVMs = TourHelper.ToViewModel(tours, guides, teams);
                Dictionary<string, int> tempToursPerTeam = new Dictionary<string, int>();
                TeamWithToursViewModel toursPerTeamViewModel = new TeamWithToursViewModel();
                teams.ForEach(x => tempToursPerTeam.Add(x.Name, 0));
                foreach (TourViewModel tVM in tourVMs)
                {
                    TimeSpan? time = tVM.EndedTour - tVM.StartedTour;
                    if (time.HasValue)
                    {
                        if (time.Value.TotalMinutes < 45)
                            toursDuration[0] += 1;
                        else if (time.Value.TotalMinutes >= 45 && time.Value.TotalMinutes < 75)
                            toursDuration[1] += 1;
                        else if (time.Value.TotalMinutes >= 75)
                            toursDuration[2] += 1;
                    }

                    tempToursPerTeam[tVM.Team] += 1;
                }
                toursPerTeamViewModel.Teamnames = tempToursPerTeam.Keys.ToList();
                toursPerTeamViewModel.NumOfTours = tempToursPerTeam.Values.ToList();

                
                
               


                result.Add(cntOfToursPerHalfHour);
                result.Add(toursPerTeamViewModel);
                result.Add(toursDuration);
            }
            return result;
        }
    }
}