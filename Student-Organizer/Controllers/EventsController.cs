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
using Microsoft.AspNet.Identity.Owin;

namespace Student_Organizer.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        ApplicationUser myUser = System.Web.HttpContext.Current.GetOwinContext().
                GetUserManager<ApplicationUserManager>().
                FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());


        public ActionResult Index()
        {
            try
            {
                var eventList = db.Events.Where(g => g.UserId == myUser.Id).
                ToList().GroupBy(x => x.eventID).Select(y => y.First()).ToList();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string jsonEvents = SerializeCalendarEventList(eventList);
                return View(new Event() { calendarDataHolder = jsonEvents });
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }
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
            data.UserId = myUser.Id;
            data.eventID = (data.title + data.UserId + data.startDate + data.startTime);
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
            oldEvent.backgroundColor = data.backgroundColor;
            oldEvent.start = Convert.ToDateTime(data.startDate + " " + data.startTime);
            oldEvent.end = Convert.ToDateTime(data.endDate + " " + data.endTime);
            oldEvent.eventID = ResetEventID(data);
            db.SaveChanges();
            Response.Redirect("http://localhost:35591/Events/Index");
        }

        public string ResetEventID(Event myEvent)
        {
            string eventID = myEvent.title + myEvent.UserId + myEvent.startDate + myEvent.startTime;
            return eventID;
        }

        public void DeleteEvent(Event data)
        {
            Event oldEvent = GetEvents(data.eventID);
            db.Events.Remove(oldEvent);
            db.SaveChanges();
            Response.Redirect("http://localhost:35591/Events/Index");
        }
        public string SerializeCalendarEventList(List<Event> EventList)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(EventList);
            return json;

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

        public List<string> SplitStringToList(string list)
        {
            List<string> NewList = list.Replace(" ", "").Split(',').ToList();
            return NewList;
        }

        public List<Event> CopyEventList(List<Event> EventList)
        {
            List<Event> tempList = new List<Event>();
            for (int k = 0; k < EventList.Count; k++)
            {
                tempList.Add(EventList[k]);
                tempList[k].editable = false;
                tempList[k].backgroundColor = "gray";
            }
            return tempList;
        }

        public void ShareEvents(string emails, string backgroundColor)
        {
            List<string> ClassEnrollmentList = SplitStringToList(emails);

            List<Event> EventList = db.Events.Where(g => g.UserId == myUser.Id).Where(h=>h.backgroundColor == backgroundColor).Select(z=>z).ToList();
            List<Event> tempList = CopyEventList(EventList);

            for (int j = 0; j < ClassEnrollmentList.Count(); j++)
            {
                for (int i = 0; i < tempList.Count(); i++)
                {
                    var email = ClassEnrollmentList[j];
                    tempList[i].UserId = db.Users.Where(v => v.Email == email).FirstOrDefault().Id;
                    db.Events.Add(tempList[i]);
                }
                db.SaveChanges();
                tempList = CopyEventList(EventList);
            }
            Response.Redirect("http://localhost:35591/Events/Index");
        }

    }
}


