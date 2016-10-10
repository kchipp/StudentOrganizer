using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Student_Organizer.Models;
using System.Globalization;
using System.Web.Script.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Student_Organizer.Models
{
    [Serializable]
    public class Event
    {
        [Key]
        public int id { get; set; }
        public string eventID { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string startDate { get; set; }
        public string startTime { get; set; }
        public string endDate { get; set; }
        public string endTime { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string url { get; set; }

        public string calendarDataHolder;
        public List<Event> EventList;


        public Event()
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.start = start;
            this.end = end;
        }
    }
}
