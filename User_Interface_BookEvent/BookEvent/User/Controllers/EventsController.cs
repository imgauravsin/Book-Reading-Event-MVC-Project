using BusinessDomain.Operations;
using DataLayer.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User_Interface_BookEvent.Controllers;

namespace User_Interface_BookEvent.BookEvent.User.Controllers
{
    public class EventsController : Controller
    {

        private bool result = false;

        private EventOperations eventoperation;
        private CreatedEntry createEntry;
        private InvitationOperations invitationOperations;
        private UserOperation userOperation;

        public EventsController()
        {
            eventoperation = new EventOperations();
            createEntry = new CreatedEntry();
            invitationOperations = new InvitationOperations();
            userOperation = new UserOperation();
        }

        // GET: User/Events
        public ActionResult Index()
        {
            var test = createEntry.CheckLoginUser();
            if (!test)
            {
                return Redirect("/Security/Authentication/Login");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Create(Event events)
        {
            var test = createEntry.CheckLoginUser();
            if (!test)
            {
                return Redirect("/Security/Authentication/Login");
            }

            events.UserId = createEntry.LoggedUserId();

            result = eventoperation.addEvents(events);

            return RedirectToAction("Index");
        }

        // Function To View Edit Form of Event
        public ActionResult Edit(int id)
        {
            var test = createEntry.CheckLoginUser();
            if (!test)
            {
                return Redirect("/Security/Authentication/Login");
            }

            if (id == 0)
            {
                return HttpNotFound();
            }
            var output = eventoperation.getEventDetails(id);
            return View(output);
        }

        // Function To Edit the Event
        public ActionResult UpdateEvent(Event ev)
        {
            var test = createEntry.CheckLoginUser();
            if (!test)
            {
                return Redirect("/Security/Authentication/Login");
            }

            eventoperation.editEvents(ev, createEntry.LoggedUserId());

            return Redirect("/User/Events/Index");
        }

        // Function TO View Invitation Form
        public ActionResult Invite(int id)
        {
            var test = createEntry.CheckLoginUser();
            if (!test)
            {
                return Redirect("/Security/Authentication/Login");
            }
            var output = eventoperation.getEventDetails(id);
            ViewBag.eventId = id;
            ViewBag.userlist = userOperation.UserIds(createEntry.LoggedUserId());
            return View(output);
        }

        // Function To Invite Registered User
        public ActionResult InviteUser()
        {
            string userId = Request.Form["userId"];
            string eventId = Request.Form["eventId"];
            char[] spearator = { ',', ' ' };
            string[] userIds = userId.Split(spearator, StringSplitOptions.RemoveEmptyEntries);
            System.Diagnostics.Debug.WriteLine(userId.Length);
            System.Diagnostics.Debug.WriteLine(userId);

            invitationOperations.inviteUser(Int32.Parse(eventId), Array.ConvertAll(userIds, int.Parse));
            return Redirect("/User/Events/ViewMyEvent");
        }

        // Function To Event Created By User
        public ActionResult ViewMyEvent()
        {
            var test = createEntry.CheckLoginUser();
            if (!test)
            {
                return Redirect("/Security/Authentication/Login");
            }

            var output = eventoperation.getCreatedEvent(createEntry.LoggedUserId());

            return View(output);
        }

        // Function To View Public Event
        public ActionResult PublicEvents()
        {
            var output = eventoperation.getPublicEvent();
            return View(output);
        }

        // Function To View Event Detail
        public ActionResult ViewPublicDetail(int id)
        {
            ViewBag.Count = invitationOperations.totalInventiations(id);
            var output = eventoperation.getEventDetails(id);
            return View(output);
        }

        // Function To View Upcoming Event
        public ActionResult UpcomingEvents()
        {
            var output = eventoperation.getPublicEvent();
            return View(output);
        }

        // Function To View Past Event
        public ActionResult PastEvents()
        {
            var output = eventoperation.getPublicEvent();
            return View(output);
        }

        // Function to Show Delete Form of an Event
        public ActionResult Delete(int id)
        {
            var test = createEntry.CheckLoginUser();
            if (!test)
            {
                return Redirect("/Security/Authentication/Login");
            }

            var output = eventoperation.getEventDetails(id);
            ViewBag.eventid = id;
            return View();
        }

        // Function To Delete An Event
        public ActionResult DeleteEvent(int id)
        {
            bool result = eventoperation.deleteEvent(id);
            return Redirect("/User/Events/ViewMyEvent");
        }

        public ActionResult Invitations()
        {
            var test = createEntry.CheckLoginUser();
            if (!test)
            {
                return Redirect("/Security/Authentication/Login");
            }
            // System.Diagnostics.Debug.WriteLine(cf.LoggedUserId());
            var userInvitations = invitationOperations.getInvitation(createEntry.LoggedUserId());
            List<Event> getEvent = new List<Event>();
            foreach (var output in userInvitations)
            {
                // System.Diagnostics.Debug.WriteLine(output.EventId);
                getEvent.Add(eventoperation.getEventDetails(output.EventId));
            }
            return View(getEvent);
        }
    }
}