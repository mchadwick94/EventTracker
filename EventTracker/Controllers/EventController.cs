using EventTracker.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Tracker.Data;

namespace EventTracker.Controllers
    {
    public class EventController : ApplicationController
        {
        public int _eventID;
        public string Country_ISO;
        protected string User_ID;
        private TrackerEntities _context;

        public EventController()
            {
            User_ID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            List<string> UsersEvents = new List<string>();
            foreach (var item in _trackerService.GetUserEvents(User_ID))
                {
                UsersEvents.Add(item.Event_ID.ToString());
                }
            ViewBag.MyEvents = UsersEvents;

            ViewBag.Lineup = _trackerService.GetLineUp(_eventID);

            _context = new TrackerEntities();

            //Populate Country Viewbag
            List<SelectListItem> CountriesList = new List<SelectListItem>();
            foreach (var item in _trackerService.GetCountries())
                {
                CountriesList.Add(
                    new SelectListItem()
                        {
                        Text = item.C_Name,
                        Value = item.C_Iso
                        });
                ViewBag.Countries = CountriesList;
                };
            }

        public ActionResult GetCities(string id) //--MODIFIED FUNCTION RETRIEVED FROM https://stackoverflow.com/questions/41564427/how-to-refresh-html-dropdowngrouplist-after-another-dropdown-changes
            {                            /*This is used to update a dropdown list of cities to only include those which are within the country selected by a primary dropdown list*/
            if (string.IsNullOrEmpty(id))
                {
                return Json(null);
                }

            string connString = "Data Source=DESKTOP-DI24F6A\\SQLDEVELOPER;Initial Catalog=EventTracker;Integrated Security=True";
            SqlConnection MyConn = new SqlConnection(connString);
            SqlCommand MySqlCmd = MyConn.CreateCommand();
            SqlDataReader adapter;
            MySqlCmd.CommandText = @"EXEC RetrieveCitiesWhereExistingVenues @Country = '" + id + "';";
            DataTable dt = new DataTable("citiesTable");
            MyConn.Open();
            adapter = MySqlCmd.ExecuteReader();
            dt.Load(adapter);

            MyConn.Close();
            List<CityVM> CityList = dt.AsEnumerable().Select(m => new CityVM() //populates var data with venues where the country_ID matches the id value.
                {
                Value = m.Field<int>("City_ID"),
                Text = m.Field<string>("C_NAME"),
                }).OrderBy(x => x.Venue_Names).ToList();
            return Json(CityList); //return data variaible as json.
            }

        public ActionResult GetVenues(int id) //--FUNCTION RETRIEVED FROM https://stackoverflow.com/questions/41564427/how-to-refresh-html-dropdowngrouplist-after-another-dropdown-changes
            {                            /*This is used to update a dropdown list of cities to only include those which are within the country selected by a primary dropdown list*/
            if (id == 0)
                {
                return Json(null);
                }

            List<SelectListItem> VenueList = new List<SelectListItem>();
            var data = _trackerService.GetVenues().Where(x => x.V_City == id).GroupBy(x => x.Venue_ID).Select(o => new VenueVM() //populates var data with venues where the country_ID matches the id value.
                {
                V_Value = o.Key,
                V_Name = o.Select(t => t.V_Name).SingleOrDefault().ToString()
                });
            return Json(data); //return data variaible as json.
            }

        // GET: Complete Event List
        public ActionResult GetEvents()
            {
            List<SelectListItem> CountriesWhereEventsList = new List<SelectListItem>();
            string connString = "Data Source=DESKTOP-DI24F6A\\SQLDEVELOPER;Initial Catalog=EventTracker;Integrated Security=True";
            SqlConnection MyConn = new SqlConnection(connString);
            SqlCommand SelectCountriesWhereEvents = MyConn.CreateCommand();
            SqlDataReader adapter;
            SelectCountriesWhereEvents.CommandText = @"exec SelectCountriesWhereEvents;";
            DataTable dt = new DataTable("CountriesWhereEventsTable");
            MyConn.Open();
            adapter = SelectCountriesWhereEvents.ExecuteReader();
            dt.Load(adapter);

            foreach (DataRow row in dt.Rows)
                {
                CountriesWhereEventsList.Add(
                    new SelectListItem()
                        {
                        Value = row.Field<string>("C_ISO"),
                        Text = row.Field<string>("C_NAME"),
                        });
                }
            MyConn.Close();
            ViewBag.CountriesWhereEvents = CountriesWhereEventsList;

            return View();
            }

        //___---------------------

        public ActionResult GetFilteredEvents(SearchEventModel searchModel)
            {
            if (HttpContext.User.Identity.IsAuthenticated != false)
                {
                }
            var business = new EventBusinessLogic();
            var model = business.GetFilteredEvents(searchModel);
            return View(model);
            }

        public ActionResult GetEventsForVenue(SearchEventModel searchModel)
            {
            ViewBag.Venue = Convert.ToInt32(Request.QueryString["Venue_ID"]);

            var business = new EventBusinessLogic();
            var model = business.GetFilteredEvents(searchModel);
            return View(model);
            }

        //=-------------------------

        //Returns the details of a specific event.
        public ActionResult GetEventDetails(int Event_ID, tbl_events _event)
            {
            List<string> UsersSeenLineup = new List<string>();
            _event = _trackerService.GetEventDetails(Event_ID);
            foreach (var item in _trackerService.GetSeenArtists(User_ID))
                {
                if (item.Event_ID == _event.Event_ID)
                    {
                    UsersSeenLineup.Add(item.Artist_ID.ToString());
                    }
                }
            ViewBag.UsersSeenOnLineup = UsersSeenLineup;
            return View(_trackerService.GetEventDetails(Event_ID));
            }

        //ADD EVENT TO A USERS HISTORY
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)] //Allows the Get and Post action to be done immediately to avoid redirecting the user to a 'Create' page.
        public ActionResult AddToUser(tbl_eventhistory _event)
            {
            try
                {
                _event.User_ID = User.Identity.GetUserId();
                _trackerService.AddToUser(_event);
                return RedirectToAction("ReturnPreviousPage", new { controller = "Application" });
                }
            catch
                {
                return View(_trackerService.GetEvents());
                }
            }

        // GET: Displays the full list of events for a specific user.
        public ActionResult GetUserEvents(string User_ID)
            {
            return View(_trackerService.GetUserEvents(User_ID));
            }

        public ActionResult GetEventHistoryDetails(int Event_ID)
            {
            return View(_trackerService.GetEventHistoryDetails(Event_ID));
            }

        //DELETES an event from a users event history
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)] //Allows events to be deleted without the user having to confirm it on a different page.
        public ActionResult DeleteFromUserHistory(int History_ID, tbl_eventhistory _event)
            {
            try
                {
                _event = _trackerService.GetEventHistoryDetails(_event.History_ID);
                _trackerService.DeleteFromUserHistory(_event);
                return RedirectToAction("ReturnPreviousPage", new { controller = "Application" });
                }
            catch
                {
                return View(_trackerService.GetEventHistoryDetails(History_ID));
                }
            }

        //Get an events Lineup
        public ActionResult GetLineUp(int Event_ID)
            {
            _eventID = Event_ID;
            List<string> Lineup = new List<string>();
            foreach (var item in _trackerService.GetLineUp(Event_ID))
                {
                Lineup.Add(item.Artist_ID.ToString());
                }
            ViewBag.Lineup = Lineup;
            return View(_trackerService.GetLineUp(Event_ID));
            }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)] //Allows the user to add to a lineup without being redirected to a create page.
        public ActionResult AddToLineup(int Event_ID, tbl_eventlineup _lineup)
            {
            try
                {
                _trackerService.AddToLineup(_lineup);
                return RedirectToAction("GetLineup", new { _lineup.Event_ID });
                }
            catch
                {
                return View(_trackerService.GetLineUp(_lineup.Event_ID));
                }
            }

        //Retrieves the details of a specific events lineup.
        public ActionResult GetLineupDetails(int Lineup_ID)
            {
            return View(_trackerService.GetLineupDetails(Lineup_ID));
            }

        /*[HttpGet] //Allows the user to delete an artist from an events lineup
        public ActionResult deleteFromLineup(int Lineup_ID)
        {
            return View(_trackerService.GetLineupDetails(Lineup_ID));
        }*/

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult DeleteFromLineup(tbl_eventlineup _lineup)
            {
            try
                {
                _lineup = _trackerService.GetLineupDetails(_lineup.Lineup_ID);
                _trackerService.DeleteFromLineup(_lineup);
                return RedirectToAction("GetLineup", new { controller = "Event", _lineup.Event_ID });
                }
            catch
                {
                return View();
                }
            }
        }
    }