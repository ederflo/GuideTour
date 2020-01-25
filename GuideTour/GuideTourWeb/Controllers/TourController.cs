using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuideTourData;
using GuideTourData.DataAccess;
using GuideTourData.Models;
using GuideTourData.Services;
using GuideTourLogic.Logics;
using GuideTourLogic.Services;
using GuideTourWeb.Helpers.ObjectHelpers;
using GuideTourWeb.Hubs;
using GuideTourWeb.Models;
using GuideTourWeb.Models.MqttModels;
using GuideTourWeb.Models.TourViewModels;
using GuideTourWeb.Mqtt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;
using Newtonsoft.Json;

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
            IndexTourViewModel viewModel = null;

            TeamImporter teamImporter = new TeamImporter(_ddb);
            TeacherImporter teacherImporter = new TeacherImporter(_ddb);
            try
            {
                //await teamImporter.ImportTeams();
                //await teacherImporter.ImportTeachers();
                List<Team> teams = await teamLogic.Get();
                List<Guide> guides = await guideLogic.Get();
                List<Tour> tours = await tourLogic.Get();

                viewModel = new IndexTourViewModel();

                if (tours != null && teams != null && guides != null)
                {
                    List<TourViewModel> tourVMs = TourHelper.ToViewModel(tours, guides, teams);
                    viewModel.NotStarted = tourVMs.FindAll(x => x.StartedTour == null && x.EndedTour == null);
                    viewModel.Started = tourVMs.FindAll(x => x.StartedTour != null && x.EndedTour == null);
                    GuideHelper.SliceNames(guides, 14);
                    TeamHelper.SliceNames(teams, 14);
                    viewModel.Teams = TeamHelper.AssignGuidesToTeam(teams, guides);
                }
                viewModel.Started = viewModel.Started.OrderBy(x => x.StartedTour).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return View(viewModel);
        }

        [HttpPatch]
        public async Task<TourViewModel> StartTour(string id = "")
        {
            TourLogic tourLogic = new TourLogic(_ddb);
            TourHelper tourHelper = new TourHelper(_ddb);
            TourViewModel result = null;
            Tour tour;
            try
            {
                if ((tour = await tourLogic.StartTour(id)) != null)
                    if ((result = await tourHelper.ToViewModel(tour)) != null)
                    {
                        await _hubcontext.Clients.All.SendAsync("TourStarted", result);
                        TourMqttModel tourMqtt = TourHelper.ToMqttModel(result, tour.IfGuideAppId);
                        MqttService.Instance.client.Publish(MqttService.StartAckUrl,
                            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(tourMqtt)));
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return result;
        }

        [HttpPatch]
        public async Task<TourViewModel> CompleteTour(string id = "")
        {
            TourLogic tourLogic = new TourLogic(_ddb);
            TourHelper tourHelper = new TourHelper(_ddb);
            TourViewModel result = null;
            Tour tour = null;
            try
            {
                if ((tour = await tourLogic.CompleteTour(id)) != null)
                    if ((result = await tourHelper.ToViewModel(tour)) != null)
                    {
                        await _hubcontext.Clients.All.SendAsync("TourCompleted", result);
                        TourMqttModel tourMqtt = TourHelper.ToMqttModel(result, tour.IfGuideAppId);
                        MqttService.Instance.client.Publish(MqttService.EndedAckUrl,
                            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(tourMqtt)));
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return result;
        }

        [HttpPatch]
        public async Task<TourViewModel> CancelTour(string id = "")
        {
            TourLogic tourLogic = new TourLogic(_ddb);
            TourHelper tourHelper = new TourHelper(_ddb);
            TourViewModel result = null;
            Tour tour;
            try
            {
                if ((tour = await tourLogic.CancelTour(id)) != null)
                    if ((result = await tourHelper.ToViewModel(tour)) != null)
                    {
                        await _hubcontext.Clients.All.SendAsync("TourCancelled", result);
                        TourMqttModel tourMqtt = TourHelper.ToMqttModel(result, tour.IfGuideAppId);
                        MqttService.Instance.client.Publish(MqttService.CanceldAck,
                            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(tourMqtt)));
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }       
            return result;
        }

        [HttpPost]
        public async Task<TourViewModel> NewTour(IndexTourViewModel viewModel)
        {
            TourLogic tourLogic = new TourLogic(_ddb);
            TourHelper tourHelper = new TourHelper(_ddb);
            TourViewModel result = null;
            if (viewModel == null || string.IsNullOrEmpty(viewModel.GuideId) || string.IsNullOrEmpty(viewModel.GuideTeamId))
                return null;
            Tour tour;
            try
            {
                tour = TourLogic.NewTour(viewModel.GuideId, viewModel.TeacherId, viewModel.VisitorName);
                if ((tour = await tourLogic.Add(tour)) != null)
                    if ((result = await tourHelper.ToViewModel(tour)) != null)
                    {
                        await _hubcontext.Clients.All.SendAsync("NewRequestedTour", result);
                    }
            } 
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return result;
        }

        [HttpPost]
        public async Task<string> CheckPermissions(string teacherId = "", int pinCode = -1)
        {
            TeacherLogic teacherLogic = new TeacherLogic(_ddb);
            try
            {
                if (!string.IsNullOrEmpty(teacherId))
                {
                    teacherId = await teacherLogic.CheckLastAction(teacherId);
                }
                else if (pinCode > 0)
                {
                    teacherId = await teacherLogic.CheckPinCode(pinCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            return string.IsNullOrEmpty(teacherId) ? "BigFail" : teacherId;
        }
    }
}