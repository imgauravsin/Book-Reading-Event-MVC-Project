using DataLayer;
using DataLayer.EntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Operations
{
    public class EventOperations
    {

         
        private BookEventContext db;

        public EventOperations()
        {
            db = new BookEventContext();
        }

        // Function To Create Event
        public Boolean addEvents(Event events)
        {
            if (events == null)
            {
                return false;
            }

            if (events.EventDate == null || events.EventDescription == null || events.EventLocation == null ||
                events.EventName == null || events.EventOtherDetails == null || events.EventStartTime == null || events.EventDuration>4)
            {
                return false;
            }

            db.events.Add(events);
            db.SaveChanges();
            // ModelState.Clear();
            return true;
        }

        // Function To List All the Evevt Created By User
        public List<Event> getEvents()
        {
            var output = db.events.ToList();
            return output;
        }

        // Function To Update The Event By Event ID
        public bool editEvents(Event ev, int userId)
        {
            ev.UserId = userId;
            db.Entry(ev).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        // Functio TO Get Event Detail By Event Id
        public Event getEventDetails(int id)
        {
            var output = db.events.Single(x => x.EventId == id);
            return output;
        }

        // Function To Remove Event
        public bool deleteEvent(int id)
        {
            var output = db.events.Single(x => x.EventId == id);
            if (output == null)
            {
                return false;
            }

            db.events.Remove(output);
            db.SaveChanges();

            InvitationOperations inv = new InvitationOperations();
            var outputs = db.invitation.Where(d => d.EventId == id);
            foreach (var x in outputs)
            {
                db.invitation.Remove(x);
            }

            return true;
        }

        // Get All Event For Logged User
        public IEnumerable<Event> getCreatedEvent(int userId)
        {

            var output = getEvents().Where(d => d.UserId == userId);

            return output;
        }

        // Function To List All Invited Event
        public List<Event> getInvitedEvent()
        {
            var output = db.events.ToList();
            return output;
        }

        // Function To Find All Public Events
        public IEnumerable<Event> getPublicEvent()
        {
            var output = getEvents().Where(d => d.EventType == 2);
            return output;
        }
       
    }
}
