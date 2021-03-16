using BusinessDomain.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace User_Interface_BookEvent.BookEvent.Common.Controllers
{
    public class HomeController : Controller
    {

        private EventOperations eventOperations;

        public HomeController()
        {
            eventOperations = new EventOperations();
        }

        // GET: Common/Home
        public ActionResult Index()
        {
            Session["userId"] = 0;
            var output = eventOperations.getPublicEvent();

            return View(output);
        }
    }
}