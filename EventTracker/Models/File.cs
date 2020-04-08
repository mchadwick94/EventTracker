using System.ComponentModel.DataAnnotations;
using Tracker.Data;

namespace EventTracker.Models
{
    public class File
    {
        public int File_ID { get; set; }

        [StringLength(255)]
        public string File_Name { get; set; }

        [StringLength(100)]
        public string Content_Type { get; set; }

        public byte[] Content { get; set; }
        public FileType FileType { get; set; }
        public int Artist_ID { get; set; }
        public virtual tbl_artists tbl_artists { get; set; }
    }
}