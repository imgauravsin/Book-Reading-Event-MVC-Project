using BusinessDomain.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User_Interface_BookEvent.Controllers;

namespace User_Interface_BookEvent.BookEvent.Security.Controllers
{
    public class AuthenticationController : Controller
    {

        private UserOperation uo;
        private CreatedEntry cf;
        private EventOperations eventOperations;

        public AuthenticationController()
        {
            uo = new UserOperation();
            cf = new CreatedEntry();
            eventOperations = new EventOperations();
        }

        // GET: Security/Authentication
        public ActionResult Login()
        {
            // System.Diagnostics.Debug.WriteLine("Aa Rhi");
            return View();
        }

        public ActionResult Validate()
        {

            string useremail = Request.Form["UserEmail"];
            string password = Request.Form["UserPassword"];

            System.Diagnostics.Debug.WriteLine(useremail);
            System.Diagnostics.Debug.WriteLine(password);

            if (useremail != null && useremail != "" && password != null && password != "")
            {

                int userId = uo.checkUser(useremail, password);
                if (userId != 0)
                {
                    Session["userId"] = userId;
                    System.Diagnostics.Debug.WriteLine((int)Session["userId"]);
                    return RedirectToAction("Home");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Register()
        {
            Session["userId"] = 0;
            return View();
        }

        public ActionResult Registeration()
        {
            string username = Request.Form["UserName"];
            string useremail = Request.Form["UserEmail"];
            string password = Request.Form["UserPassword"];

            System.Diagnostics.Debug.WriteLine(username);
            System.Diagnostics.Debug.WriteLine(useremail);
            System.Diagnostics.Debug.WriteLine(password);

            if (username == null || username == "" || useremail == null || useremail == "" || password == null || password == "")
            {
                return RedirectToAction("Register");
            }
            DataLayer.EntityModel.User usr = new DataLayer.EntityModel.User();
            usr.UserName = username;
            usr.UserEmail = useremail;
            usr.UserPassword = password;
            usr.UserRole = "U";

            bool result = uo.addUser(usr);
            if (result)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Register");
            }
        }

        public ActionResult Home()
        {
            var test = cf.CheckLoginUser();
            if (!test)
            {
                return Redirect("/Security/Authentication/Login");
            }
            var output = eventOperations.getPublicEvent();

            return View(output);
        }

        public ActionResult Logout()
        {

            Session["userId"] = 0;
            return Redirect("/Security/Authentication/Login");
        }
    }
}