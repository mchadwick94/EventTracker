using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Tracker.Data;

namespace EventTracker.Controllers
{
    public class FileController : ApplicationController
    {
        private TrackerEntities _context;

        public FileController()
        {
            _context = new TrackerEntities();
        }

        //
        // GET: /File/ The code obtains the correct file based on the id value passed in the query string. Then it returns the image to the browser as a FileResult.
        public ActionResult Index(int File_ID)
        {
            var fileToRetrieve = _context.tbl_artistImages.Find(File_ID);
            return File(fileToRetrieve.Content, fileToRetrieve.Content_Type);
        }

        public ActionResult GetImageId(int Artist_ID)
        {
            int File_ID;
            tbl_artistImages _image = _context.tbl_artistImages.SingleOrDefault(s => s.Artist_ID == Artist_ID); //fetches all files associated with the artist regardless of type.
            if (_image == null)
            {
                File_ID = 1209;

            }
            else
            {
                File_ID = _trackerService.GetImageId(Artist_ID).File_ID;

            }
            var fileToRetrieve = _context.tbl_artistImages.Find(File_ID);
            return File(fileToRetrieve.Content, fileToRetrieve.Content_Type);
        }
    }
}