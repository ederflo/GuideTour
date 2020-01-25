using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuideTourData.Services;
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
        public IActionResult Index()
        {
            return View();
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