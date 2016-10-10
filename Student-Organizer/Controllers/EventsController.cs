using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Student_Organizer.Models;
using System.Web.Script.Serialization;
using System.Globalization;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.IO;

namespace Student_Organizer.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var eventList = db.Events.ToList();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(eventList);
            return View(new Event() { calendarDataHolder = json });
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void SaveEvent(Event data)
        {
            data.start = Convert.ToDateTime(data.startDate + " " + data.startTime);
            data.end = Convert.ToDateTime(data.endDate + " " + data.endTime);
            data.eventID = (data.title + data.startTime + data.endTime);
            db.Events.Add(data);
            db.SaveChanges();
            Response.Redirect("http://localhost:35591/Events/Index");
        }

        public Event GetEvents(string eventID)
        {
            Event existingEvent = new Event();
            existingEvent = db.Events.Where(g => g.eventID == eventID).First();
            return existingEvent;
        }



        public void UpdateEvent(Event data)
        {
            Event oldEvent = new Event();
            try
            {
                oldEvent = GetEvents(data.eventID);
            }
            catch (NullReferenceException e)
            {
                throw new Exception("That event does not exist.", e);
            }

            oldEvent.title = data.title;
            oldEvent.description = data.description;
            oldEvent.startDate = data.startDate;
            oldEvent.startTime = data.startTime;
            oldEvent.endDate = data.endDate;
            oldEvent.endTime = data.endTime;
            oldEvent.start = Convert.ToDateTime(data.startDate + " " + data.startTime);
            oldEvent.end = Convert.ToDateTime(data.endDate + " " + data.endTime);
            oldEvent.eventID = data.title + data.startDate + data.startTime;
            db.SaveChanges();
            Response.Redirect("http://localhost:35591/Events/Index");
        }

        public void DeleteEvent(Event data)
        {
            Event oldEvent = GetEvents(data.eventID);
            db.Events.Remove(oldEvent);
            db.SaveChanges();
            Response.Redirect("http://localhost:35591/Events/Index");
        }
        private Event DeserializeObject(string dataRow)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Event));
            FileStream myStream = new FileStream(dataRow, FileMode.Open);
            XmlReader myReader = XmlReader.Create(myStream);
            Event myEvent = new Event();

            myEvent = (Event)serializer.Deserialize(myReader);
            myStream.Close();
            return myEvent;
        }
    }
}


