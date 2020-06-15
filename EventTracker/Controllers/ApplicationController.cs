using EventTracker.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
            }

        public ActionResult ReturnPreviousPage()
            {
            return Redirect(Request.UrlReferrer.ToString());
            }

        public ActionResult GetAllCities(string id) //--MODIFIED FUNCTION RETRIEVED FROM https://stackoverflow.com/questions/41564427/how-to-refresh-html-dropdowngrouplist-after-another-dropdown-changes
            {                            /*This is used to update a dropdown list of cities to only include those which are within the country selected by a primary dropdown list*/
            if (string.IsNullOrEmpty(id))
                {
                return Json(null);
                }

            string connString = "Data Source=DESKTOP-DI24F6A\\SQLDEVELOPER;Initial Catalog=EventTracker;Integrated Security=True";
            SqlConnection MyConn = new SqlConnection(connString);
            SqlCommand MySqlCmd = MyConn.CreateCommand();
            SqlDataReader adapter;
            MySqlCmd.CommandText = @"EXEC RetrieveCities @Country = '" + id + "';";
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


        }
    }