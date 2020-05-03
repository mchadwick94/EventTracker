using EventTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
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

        //CREATE a new event
        [HttpGet]
        public ActionResult CreateVenue()
        {
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
    }
}