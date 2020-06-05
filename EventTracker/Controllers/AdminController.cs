using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tracker.Data;
using System.Data.Entity;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using EventTracker.Models;
using System.Data.SqlClient;
using System.Data;

namespace EventTracker.Controllers
    {
    public class AdminController : ApplicationController
        {
        private EventTracker.Models.ApplicationDbContext _AppContext;
        private TrackerEntities _context;
        protected string User_ID;

        public AdminController()
            {
            _AppContext = new EventTracker.Models.ApplicationDbContext();
            _context = new TrackerEntities();
            }
        [Authorize(Roles = "Admin")]
        public ActionResult GetUsers()
            {
            return View(_AppContext.Users.ToList());
            }
        [Authorize(Roles = "Admin")]
        //Index view of Countries
        public ActionResult CountryIndex()
            {
            return View(_trackerService.GetCountries());
            }
        [Authorize(Roles = "Admin")]
        // GET: Admin
        public ActionResult AdminHome()
            {
            return View();
            }

        [Authorize(Roles = "Admin")]
        // GET: Admin/Details/5
        public ActionResult ArtistIndex()
            {
            return View(_trackerService.GetArtists());
            }

        [Authorize(Roles = "Admin")]
        // Inserts a new artist into the database.
        [HttpGet]
        public ActionResult ArtistCreate()
            {
            return View();
            }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult ArtistCreate(tbl_artists _artist, HttpPostedFileBase upload, tbl_artistImages image)
            {
            if (String.IsNullOrEmpty(_artist.Artist_Name))//Checks if the field 'Artist_Name' is null, if so, throws an error.
                {
                ModelState.AddModelError("Artist_Name", "Need an Artists Name");
                }
            if (ModelState.IsValid)
                {
                var doesArtistExist = _context.tbl_artists.Any(x => x.Artist_Name == _artist.Artist_Name); //Creates a variable which is assigned the value of any artist in tbl_artists matching the Artist_Name given in the form (_artist)
                if (doesArtistExist) //If there is a value assigned to the variable
                    {
                    ModelState.AddModelError("Artist_Name", "This Artist already exists"); //throw an error
                    return View();
                    }
                _trackerService.NewArtist(_artist); //Else add a new artist to the database with the Artist_Name given in the form.

                //====================== THIS BLOCK IS REGARDING ADDING IMAGES TO ARTIST
                if (upload != null && upload.ContentLength > 0)
                    {
                    image.Artist_ID = _artist.Artist_ID;
                    image.File_Name = System.IO.Path.GetFileName(upload.FileName);
                    image.Content_Type = upload.ContentType;
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                        image.Content = reader.ReadBytes(upload.ContentLength);
                        }
                    _trackerService.AddArtistImage(image);
                    }

                //=========================
                return RedirectToAction("ArtistCreate");
                }
            else
                {
                return View();
                }
            }
        [Authorize(Roles = "Admin")]
        //Edit an artists details
        [HttpGet] //Retrieves the details of the event being edited
        public ActionResult ArtistEdit(int Artist_ID)
            {
            tbl_artists _artist = _context.tbl_artists.Include(s => s.tbl_artistImages).SingleOrDefault(s => s.Artist_ID == Artist_ID); //fetches all files associated with the artist regardless of type.
            if (_artist == null)
                {
                return HttpNotFound("An Artist wasn't entered.");
                }
            return View(_trackerService.GetArtistDetails(Artist_ID));
            }
        [Authorize(Roles = "Admin")]
        [HttpPost] //Posts the new variables into the database at the specific event being edited
        public ActionResult ArtistEdit(int Artist_ID, tbl_artists _artist, HttpPostedFileBase upload, tbl_artistImages newImage, tbl_artistImages oldImage)
            {
            var Model = new TrackerEntities();
            try
                {
                if (upload != null && upload.ContentLength > 0)
                    {
                    if (_context.tbl_artistImages.Any(f => f.Artist_ID == _artist.Artist_ID))
                        {
                        oldImage = _context.tbl_artistImages.FirstOrDefault(s => s.Artist_ID == Artist_ID);

                        newImage.Artist_ID = _artist.Artist_ID;
                        newImage.File_Name = System.IO.Path.GetFileName(upload.FileName);
                        newImage.Content_Type = upload.ContentType;
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                            {
                            newImage.Content = reader.ReadBytes(upload.ContentLength);
                            }
                        _trackerService.EditArtistImage(oldImage, newImage);
                        }
                    else
                        {
                        newImage.Artist_ID = _artist.Artist_ID;
                        newImage.File_Name = System.IO.Path.GetFileName(upload.FileName);
                        newImage.Content_Type = upload.ContentType;
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                            {
                            newImage.Content = reader.ReadBytes(upload.ContentLength);
                            }
                        _trackerService.AddArtistImage(newImage);
                        }
                    }
                _trackerService.EditArtist(_artist);

                return RedirectToAction("ArtistIndex");
                }
            catch
                {
                return View(_trackerService.GetArtistDetails(Artist_ID));
                }
            }

        //________________________________________________________________
        [Authorize(Roles = "Admin")]
        // GET: Complete Event List
        public ActionResult EventIndex()
            {
            if (HttpContext.User.Identity.IsAuthenticated != false)
                {
                User_ID = System.Web.HttpContext.Current.User.Identity.GetUserId();
                List<string> UsersEvents = new List<string>();
                foreach (var item in _trackerService.GetUserEvents(User_ID))
                    {
                    UsersEvents.Add(item.Event_ID.ToString());
                    }
                ViewBag.MyEvents = UsersEvents;
                }

            return View(_trackerService.GetEvents());
            }
        [Authorize(Roles = "Admin")]
        //CREATE a new event
        [HttpGet]
        public ActionResult EventCreate(string C_Iso)
            {
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
                }
            return View();
            }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EventCreate(tbl_events _event, HttpPostedFileBase upload, tbl_eventImages image)
            {
            try
                {
                if (String.IsNullOrEmpty(_event.Event_Name))//Checks if the field 'Artist_Name' is null, if so, throws an error.
                    {
                    ModelState.AddModelError("Event_Name", "Need an Event Name");
                    }

                var doesEventExist = _context.tbl_events.Any(x => x.Event_Name == _event.Event_Name); //Creates a variable which is assigned the value of any Event in tbl_events matching the Event_Name given in the form (_event)
                if (doesEventExist) //If there is a value assigned to the variable
                    {
                    ModelState.AddModelError("Event_Name", "This Event already exists"); //throw an error
                    return View();
                    }

                //______________________________________________________

                _trackerService.CreateEvent(_event); //Else add a new artist to the database with the Artist_Name given in the form.

                //====================== THIS BLOCK IS REGARDING ADDING IMAGES TO ARTIST

                if (upload != null && upload.ContentLength > 0)
                    {
                    image.Event_ID = _event.Event_ID;
                    image.File_Name = System.IO.Path.GetFileName(upload.FileName);
                    image.Content_Type = upload.ContentType;
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                        {
                        image.Content = reader.ReadBytes(upload.ContentLength);
                        }
                    _trackerService.AddEventImage(image);
                    }

                //=========================

                return RedirectToAction("EventIndex");
                }
            catch
                {
                return RedirectToAction("EventIndex");
                }
            }

        [Authorize(Roles = "Admin")]
        [HttpGet] //Retrieves the details of the event being edited
        public ActionResult EventEdit(int Event_ID, string Country_ID, int City_ID)
            {
            List<SelectListItem> VenuesList = new List<SelectListItem>();

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
                }
            foreach (var item in _trackerService.GetVenuesByCountry(Country_ID).Where(x => x.V_City == City_ID))
                {
                VenuesList.Add(
                    new SelectListItem()
                        {
                        Text = item.V_Name,
                        Value = item.Venue_ID.ToString()
                        });
                ViewBag.Venues = VenuesList.OrderBy(x => x.Text);
                }
            string connString = "Data Source=DESKTOP-DI24F6A\\SQLDEVELOPER;Initial Catalog=EventTracker;Integrated Security=True";
            SqlConnection MyConn = new SqlConnection(connString);
            SqlCommand MySqlCmd = MyConn.CreateCommand();
            SqlDataReader adapter;
            MySqlCmd.CommandText = @"EXEC RetrieveCitiesWhereExistingVenues @Country = '" + Country_ID + "';";
            DataTable dt = new DataTable("citiesTable");
            MyConn.Open();
            adapter = MySqlCmd.ExecuteReader();
            dt.Load(adapter);

            MyConn.Close();
            List<CityVM> CityList = dt.AsEnumerable().Select(m => new CityVM() //populates var data with venues where the country_ID matches the id value.
                {
                Value = m.Field<int>("City_ID"),
                Text = m.Field<string>("C_NAME"),
                }).ToList();
            ViewBag.Cities = CityList.OrderBy(x => x.Text);

            return View(_trackerService.GetEventDetails(Event_ID));
            }

        [Authorize(Roles = "Admin")]
        [HttpPost] //Posts the new variables into the database at the specific event being edited
        public ActionResult EventEdit(int Event_ID, tbl_events _event, HttpPostedFileBase upload, tbl_eventImages newImage, tbl_eventImages oldImage)
            {
            var Model = new TrackerEntities();

            try
                {
                if (upload != null && upload.ContentLength > 0)
                    {
                    if (_context.tbl_eventImages.Any(f => f.Event_ID == _event.Event_ID))
                        {
                        oldImage = _context.tbl_eventImages.FirstOrDefault(s => s.Event_ID == Event_ID);

                        newImage.Event_ID = _event.Event_ID;
                        newImage.File_Name = System.IO.Path.GetFileName(upload.FileName);
                        newImage.Content_Type = upload.ContentType;
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                            {
                            newImage.Content = reader.ReadBytes(upload.ContentLength);
                            }
                        _trackerService.EditEventImage(oldImage, newImage);
                        }
                    else
                        {
                        newImage.Event_ID = _event.Event_ID;
                        newImage.File_Name = System.IO.Path.GetFileName(upload.FileName);
                        newImage.Content_Type = upload.ContentType;
                        using (var reader = new System.IO.BinaryReader(upload.InputStream))
                            {
                            newImage.Content = reader.ReadBytes(upload.ContentLength);
                            }
                        _trackerService.AddEventImage(newImage);
                        }
                    _trackerService.EditEvent(_event);
                    }
                _trackerService.EditEvent(_event);
                return RedirectToAction("GetEventDetails", "Event", new { Event_ID = Event_ID });
                }
            catch
                {
                return RedirectToAction("EventIndex");
                }
            }

        public ActionResult VenueIndex()
            {
            return View(_trackerService.GetVenues());
            }

        [Authorize(Roles = "Admin")]
        //CREATE a new event
        [HttpGet]
        public ActionResult VenueCreate()
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult VenueCreate(tbl_venues _venue)
            {
            try
                {
                _trackerService.CreateVenue(_venue);
                return RedirectToAction("VenueIndex");
                }
            catch
                {
                return View();
                }
            }
        [Authorize(Roles = "Admin")]
        [HttpGet] //Retrieves the details of the venue being edited
        public ActionResult VenueEdit(int Venue_ID, string selectedCountry)
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
        [Authorize(Roles = "Admin")]
        [HttpPost] //Posts the new variables into the database at the specific venue being edited
        public ActionResult VenueEdit(int Venue_ID, tbl_venues _venue)
            {
            try
                {
                _trackerService.EditVenue(_venue);
                return RedirectToAction("VenueIndex");
                }
            catch
                {
                return View(_trackerService.GetVenueDetails(Venue_ID));
                }
            }
        [Authorize(Roles = "Admin")]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult VenueDelete(tbl_venues venue)
            {
            try
                {
                venue = _trackerService.GetVenueDetails(venue.Venue_ID);
                _trackerService.DeleteVenue(venue);
                return RedirectToAction("VenueIndex");
                }
            catch
                {
                return View();
                }
            }
        }
    }