using BusinessDomain.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User_Interface_BookEvent.Controllers;

namespace User_Interface_BookEvent.BookEvent.Admin.Controllers
{
    public class AdminController : Controller
    {

        private UserOperation userOperation;
        private EventOperations eventOperations;
        private CreatedEntry createEntry;

        public AdminController()
        {
            userOperation = new UserOperation();
            eventOperations = new EventOperations();
            createEntry = new CreatedEntry();
        }

        public ActionResult AllUsers()
        {
            var test = createEntry.CheckLoginUser();
            if (!test)
            {
                return Redirect("/Security/Authentication/Login");
            }
            string userRole = createEntry.GetUserRole(createEntry.LoggedUserId());

            if (userRole == "A")
            {
                var output = userOperation.getUserData();
                return View(output);
            }
            else
            {
                return Redirect("/Security/Authentication/Home");
            }
        }

        public ActionResult AllEvents()
        {
            var test = createEntry.CheckLoginUser();
            if (!test)
            {
                return Redirect("/Security/Authentication/Login");
            }

            string userRole = createEntry.GetUserRole(createEntry.LoggedUserId());

            if (userRole == "A")
            {
                var output = eventOperations.getEvents();
                return View(output);
            }
            else
            {
                return Redirect("/Security/Authentication/Home");
            }
        }
    }
}