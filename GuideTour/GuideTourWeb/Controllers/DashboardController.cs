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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Teams()
        {
            return View();
        }

        public IActionResult Guides()
        {
            return View();
        }

        public IActionResult Tours()
        {
            return View();
        }
    }
}