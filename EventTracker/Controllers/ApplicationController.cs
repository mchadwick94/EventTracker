using System.Collections.Generic;
using System.Web.Mvc;

namespace EventTracker.Controllers
{
    public class ApplicationController : Controller
    {
        public Tracker.Services.Service.TrackerService _trackerService;

        public ApplicationController()
        {
            _trackerService = new Tracker.Services.Service.TrackerService();
            ViewBag.Artists = _trackerService.GetArtists();
            ViewBag.Countries = _trackerService.GetCountries();
        }

        // GET: Application
        public ActionResult GetCountries()
        {
            List<SelectListItem> CountriesList = new List<SelectListItem>();
            foreach (var item in _trackerService.GetCountries())
            {
                CountriesList.Add(
                    new SelectListItem()
                    {
                        Text = item.C_Name,
                        Value = item.Country_ID.ToString()
                    });
            }
            ViewBag.Countries = CountriesList;

            return View(ViewBag.Countries);
        }
    }
}