using System.Collections.Generic;
using System.Web.Mvc;
using Tracker.Data;

namespace EventTracker.Controllers
    {
    public class VenueController : ApplicationController
        {
        private TrackerEntities _context;

        public string User_ID;

        public VenueController()
            {
            _context = new TrackerEntities();
            }

        // GET: Venue
        public ActionResult GetVenues()
            {
            return View(_trackerService.GetVenues());
            }

        // GET: Venue
        public ActionResult GetVenuesByCountry(string C_Iso)
            {
            return View(_trackerService.GetVenuesByCountry(C_Iso));
            }

        //CREATE a new event
        [HttpGet]
        public ActionResult CreateVenue()
            {//Populate Country Viewbag
            List<SelectListItem> CountriesList = new List<SelectListItem>();
            foreach (var item in _trackerService.GetCountries())
                {
                CountriesList.Add(
                    new SelectListItem()
                        {
                        Text = item.C_Name,
                        Value = item.C_Iso.ToString()
                        });
                ViewBag.Countries = CountriesList;
                }
            return View();
            }

        [HttpPost]
        public ActionResult CreateVenue(tbl_venues _venue)
            {
            try
                {
                _trackerService.CreateVenue(_venue);
                return RedirectToAction("GetVenues");
                }
            catch
                {
                return View();
                }
            }

        public ActionResult GetVenueDetails(int Venue_ID)
            {
            return View(_trackerService.GetVenueDetails(Venue_ID));
            }

        [HttpGet] //Retrieves the details of the venue being edited
        public ActionResult EditVenue(int Venue_ID, string selectedCountry)
            {
            List<SelectListItem> CountriesList = new List<SelectListItem>();
            foreach (var item in _trackerService.GetCountries())
                {
                CountriesList.Add(
                    new SelectListItem()
                        {
                        Text = item.C_Name,
                        Value = item.C_Iso.ToString()
                        });
                ViewBag.Countries = CountriesList;
                }
            return View(_trackerService.GetVenueDetails(Venue_ID));
            }

        [HttpPost] //Posts the new variables into the database at the specific venue being edited
        public ActionResult EditVenue(int Venue_ID, tbl_venues _venue)
            {
            try
                {
                _trackerService.EditVenue(_venue);
                return RedirectToAction("GetVenues");
                }
            catch
                {
                return View(_trackerService.GetVenueDetails(Venue_ID));
                }
            }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult DeleteVenue(tbl_venues venue)
            {
            try
                {
                venue = _trackerService.GetVenueDetails(venue.Venue_ID);
                _trackerService.DeleteVenue(venue);
                return RedirectToAction("GetVenues");
                }
            catch
                {
                return View();
                }
            }

        public void Redirect(string V_Name, string V_StreetAddress, string V_City)
            {
            string SearchName = V_Name + ",+" + V_StreetAddress + ", +" + V_City;
            ViewBag.UrlMapString = "https://google.com/maps/search/" + SearchName;

            Response.Redirect(ViewBag.UrlMapString);
            }
        }
    }