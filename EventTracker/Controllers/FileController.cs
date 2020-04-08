using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Tracker.Data;

namespace EventTracker.Controllers
{
    public class FileController : Controller
    {
        private TrackerEntities _context;

        public string User_ID = System.Web.HttpContext.Current.User.Identity.GetUserId().GetHashCode().ToString();

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
    }
}