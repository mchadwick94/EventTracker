using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tracker.Data.IDAO
{
    public interface ITrackerDAO
    {
        IList<Tracker.Data.tbl_events> GetEvents();



        IList<Tracker.Data.tbl_artists> GetArtists();




        IList<Tracker.Data.tbl_users> GetUsers();
        IList<Tracker.Data.tbl_eventhistory> GetUserEvents(int Event_ID);

    }
}
