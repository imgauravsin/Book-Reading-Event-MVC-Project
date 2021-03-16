using BusinessDomain.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace User_Interface_BookEvent.Controllers
{
    public class CreatedEntry : Controller
    {

        public bool CheckLoginUser()
        {
            int userId = (int)System.Web.HttpContext.Current.Session["userId"];

            if (userId > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CheckLogged()
        {
            bool loggedUserId = CheckLoginUser();
            // System.Diagnostics.Debug.WriteLine(loggedUserId + " Aa Rhi");
            if (loggedUserId)
            {
                // System.Diagnostics.Debug.WriteLine(loggedUserId + " Aa Rhi");
            }
            else
            {
                // System.Diagnostics.Debug.WriteLine(loggedUserId + " Redirect Ho rha");
                redirectLogin();
            }
        }

        public ActionResult redirectLogin()
        {
            // System.Diagnostics.Debug.WriteLine("Function Call Hua");
            return RedirectToAction("Login", "Authentication", "Security");
            // return RedirectToAction("/Security/Authentication/Login");
        }

        public int LoggedUserId()
        {

            var log = CheckLoginUser();
            int userId = 0;
            if (log)
            {
                userId = (int)System.Web.HttpContext.Current.Session["userId"];
            }

            return userId;
        }

        public string GetUserRole(int userId)
        {
            UserOperation userOperation = new UserOperation();
            var role = userOperation.getRole(userId);
            return role;
        }
    }
}