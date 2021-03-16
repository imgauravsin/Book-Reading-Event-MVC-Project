using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using User_Interface_BookEvent.BookEvent.Admin.Controllers;

namespace User_Interface_BookEvent.Tests.Controllers
{
    /// <summary>
    /// Summary description for AdminControllerTest
    /// </summary>
    [TestClass]
    public class AdminControllerTest
    {

        [TestMethod]
        public void AllUsers()
        {
            // Arrange
            AdminController controller = new AdminController();
            string viewName = "AllUsers";

            // Act
            ViewResult result = controller.AllUsers() as ViewResult;

            // Assert
            Assert.AreEqual(viewName, result.ViewName);
        }

        [TestMethod]
        public void AllEvents()
        {
            // Arrange
            AdminController controller = new AdminController();
            string viewName = "AllEvents";

            // Act
            ViewResult result = controller.AllEvents() as ViewResult;

            // Assert
            Assert.AreEqual(viewName, result.ViewName);
        }
    }
}
