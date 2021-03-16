using DataLayer;
using DataLayer.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Operations
{
    public class InvitationOperations
    {

        private BookEventContext db;

        public InvitationOperations()
        {
            db = new BookEventContext();
        }

        public bool addInvitation(Invitation invitation)
        {
            db.invitation.Add(invitation);
            db.SaveChanges();
            return true;
        }

        public IEnumerable<Invitation> getInvitation(int userId)
        {
            var output = db.invitation.ToList().Where(d => d.UserId == userId);
            return output;
        }

        // Function To Invite the User
        public bool inviteUser(int eventId, int[] userId)
        {

            Invitation invitation = new Invitation();

            for (int i = 0; i < userId.Length; i++)
            {
                invitation.EventId = eventId;
                invitation.UserId = userId[i];
                invitation.InvitationActive = 1;

                db.invitation.Add(invitation);
                db.SaveChanges();
            }
            return true;
        }
        public int totalInventiations(int id)
        {
            var output = db.invitation.Count(d => d.UserId == id);

            return output;
        }

    }
}
