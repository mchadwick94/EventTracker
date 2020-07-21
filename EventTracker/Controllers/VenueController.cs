using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            User_ID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            List<string> UsersEvents = new List<string>();
            foreach (var item in _trackerService.GetUserEvents(User_ID))
                {
                UsersEvents.Add(item.Event_ID.ToString());
                }
            ViewBag.MyEvents = UsersEvents;

            _context = new TrackerEntities();
            }

        public ActionResult VenueFilter()
            {
            List<SelectListItem> CountriesWhereVenuesList = new List<SelectListItem>();
            string connString = "Data Source=DESKTOP-DI24F6A\\SQLDEVELOPER;Initial Catalog=EventTracker;Integrated Security=True";
            SqlConnection MyConn = new SqlConnection(connString);
            SqlCommand SelectCountriesWhereVenuesList = MyConn.CreateCommand();
            SqlDataReader adapter;
            SelectCountriesWhereVenuesList.CommandText = @"exec SelectCountriesWhereVenues;";
            DataTable dt = new DataTable("CountriesWhereEventsTable");
            MyConn.Open();
            adapter = SelectCountriesWhereVenuesList.ExecuteReader();
            dt.Load(adapter);

            foreach (DataRow row in dt.Rows)
                {
                CountriesWhereVenuesList.Add(
                    new SelectListItem()
                        {
                        Value = row.Field<string>("C_ISO"),
                        Text = row.Field<string>("C_NAME").ToUpper(),
                        });
                }
            MyConn.Close();
            ViewBag.CountriesWhereVenues = CountriesWhereVenuesList;
            return View();
            }

        // GET: Venue
        public ActionResult GetVenues()
            {
            ViewBag.Countries = new ApplicationController().ViewBag.Countries;
            return View(_trackerService.GetVenues());
            }

        public ActionResult GetVenuesByCity(int City_ID)
            {
            return View(_trackerService.GetVenuesByCity(City_ID));
            }

        public ActionResult GetVenueHome(int Venue_ID)
            {
            return View(_trackerService.GetVenueDetails(Venue_ID));
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

        public ActionResult VenueEventsFilter()
            {
            return View();
            }
        }
    }