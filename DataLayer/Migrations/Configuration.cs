namespace DataLayer.Migrations
{
    using DataLayer.EntityModel;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataLayer.BookEventContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataLayer.BookEventContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var users = new List<User>
            {
                new User{ UserName = "Gaurav Singh", UserEmail = "myadmin@bookevents.com", UserPassword = "admin@123", UserRole = "A", },
                new User { UserName = "User", UserEmail = "user@gmail.com", UserPassword = "user@123", UserRole = "U", }
            };

            users.ForEach(s => context.user.AddOrUpdate(p => p.UserEmail, s));
            context.SaveChanges();

            var events = new List<Event> {
                new Event
                {
                    EventName = "Republic Day", EventDate = DateTime.Parse("2020-01-26"), EventLocation = "Delhi",
                    EventStartTime = "12:00", EventType = 2, EventDuration = 4, EventDescription = "Consititutions Dya",
                    EventOtherDetails = "26 Jan ", UserId = 1, EventActive = 1,
                }
            };

            events.ForEach(s => context.events.AddOrUpdate(p => p.EventName, s));
            context.SaveChanges();

            var invitations = new List<Invitation> {
                new Invitation{ InvitationActive = 1, EventId = 1, UserId = 2}
            };
            invitations.ForEach(s => context.invitation.AddOrUpdate(s));
            context.SaveChanges();

            var comment = new List<Comment> {
                new Comment{ UserComment = "Nice Event", UserId = 2, EventId = 1}
            };

            comment.ForEach(s => context.comment.AddOrUpdate(s));
            context.SaveChanges();
        }
    }
}
