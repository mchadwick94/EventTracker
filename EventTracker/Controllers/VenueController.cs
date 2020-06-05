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

        public ActionResult GetVenueDetails(int Venue_ID)
            {
            return View(_trackerService.GetVenueDetails(Venue_ID));
            }

        public void Redirect(string V_Name, string V_StreetAddress, string V_City)
            {
            string SearchName = V_Name + ",+" + V_StreetAddress + ", +" + V_City;
            ViewBag.UrlMapString = "https://google.com/maps/search/" + SearchName;

            Response.Redirect(ViewBag.UrlMapString);
            }
        }
    }