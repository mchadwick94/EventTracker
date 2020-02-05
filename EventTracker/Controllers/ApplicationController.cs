using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tracker.Data;
using Tracker.Services;

namespace EventTracker.Controllers
{
    public class ApplicationController : Controller
    {
        public Tracker.Services.Service.TrackerService _trackerService;

        public ApplicationController()
        {
            _trackerService = new Tracker.Services.Service.TrackerService();

            ViewBag.Artists = _trackerService.GetArtists();
        }
        // GET: Application
        public ActionResult Index()
        {
            return View();
        }
    }
}