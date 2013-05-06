using DRMFSS.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web.Mvc;
using DRMFSS.Web.Models;
using DRMFSS.BLL;
using System.ComponentModel;
using Moq;
using System.Collections.Generic;

namespace DRMFSS.Test
{
    
    
    /// <summary>
    ///This is a test class for AdminUnitControllerTest and is intended
    ///to contain all AdminUnitControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AdminUnitControllerTest :ControllerTestBase
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for AdminUnitController Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void AdminUnitControllerConstructorTest()
        {
            try
            {
                AdminUnitController target = new AdminUnitController();
            }
            catch(Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        /// <summary>
        ///A test for AdminUnits
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void AdminUnits_AdminUnits_WithAdminUnitType_Test()
        {
            mockRepository.Setup(m => m.AdminUnit.GetRegions()).Returns(new List<BLL.AdminUnit>());
            mockRepository.Setup(m => m.AdminUnit.GetAdminUnitType(1)).Returns(new BLL.AdminUnitType());
            AdminUnitController target = new AdminUnitController(mockRepository.Object); // TODO: Initialize to an appropriate value
            Nullable<int> id = new Nullable<int>(); // TODO: Initialize to an appropriate value
            id = 1;
            var viewName = "Lists/AdminUnits." + id + "";
            PartialViewResult actual;
            actual = (PartialViewResult)target.AdminUnits(id);
            Assert.AreEqual(viewName, actual.ViewName);
        }

        [TestMethod()]
        public void AdminUnits_AdminUnits_WithOutAdminUnitType_Test()
        {
            AdminUnitController target = new AdminUnitController(); // TODO: Initialize to an appropriate value
            Nullable<int> id = new Nullable<int>(); // TODO: Initialize to an appropriate value
            //var viewName = "Lists/AdminUnits." + id + "";
            id = null;
            EmptyResult actual;
            try
            {
                actual = (EmptyResult)target.AdminUnits(id);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.

        [TestMethod]
        public void AdminUnits_Create_Positive_Post_Test()
        {
            AdminUnitModel unit = new AdminUnitModel();
            mockRepository.Setup(p => p.AdminUnit.Add(new BLL.AdminUnit())).Verifiable();
            AdminUnitController target = new AdminUnitController(mockRepository.Object);
            JsonResult expected = new JsonResult();
            expected.Data = new { success = true };
            //string expected = "{ success = true }";
            JsonResult actual;
            actual = (JsonResult)target.Create(unit);
            Assert.IsNotNull(actual.Data);
            Assert.AreEqual(expected.Data.ToString(), actual.Data.ToString());
            
        }
        [TestMethod]
        public void AdminUnits_Create_Negative_Post_Test()
        {
            
            mockRepository.Setup(p => p.AdminUnit.Add(new BLL.AdminUnit())).Verifiable();
            AdminUnitController target = new AdminUnitController(mockRepository.Object); // TODO: Initialize to an appropriate value
            AdminUnitModel unit = new AdminUnitModel(); // TODO: Initialize to an appropriate value
            base.SetModelError(target);
            string viewName = "Create";
            ViewResult actual;
            actual = (ViewResult)target.Create(unit);
            Assert.IsNotNull(actual);
            Assert.AreEqual(viewName, actual.ViewName);
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
       [TestMethod]
        public void Admin_Unit_Create_Region_GET_Test()
        {
            AdminUnitController target = new AdminUnitController(); // TODO: Initialize to an appropriate value
            int typeid = 2; // TODO: Initialize to an appropriate value
            PartialViewResult actual;
            string viewName = "CreateRegion";
            
            actual = (PartialViewResult)target.Create(typeid);
            Assert.AreEqual(viewName, actual.ViewName);
            
        }
        [TestMethod]
        public void Admin_Unit_Create_Zone_GET_Test()
        {
            AdminUnitController target = new AdminUnitController(); // TODO: Initialize to an appropriate value
            int typeid = 3; // TODO: Initialize to an appropriate value
            string viewName = "CreateZone";
            PartialViewResult actual;
            actual = (PartialViewResult)target.Create(typeid);
            Assert.AreEqual(viewName, actual.ViewName);

        }
        [TestMethod]
        public void Admin_Unit_Create_Woreda_GET_Test()
        {
            AdminUnitController target = new AdminUnitController(); // TODO: Initialize to an appropriate value
            int typeid = 4; // TODO: Initialize to an appropriate value
            string viewName = "CreateWoreda";
            ViewResult actual;
            actual = (ViewResult)target.Create(typeid);
            Assert.AreEqual(viewName, actual.ViewName);
        }

        [TestMethod()]
        public void Admin_Unit_Delete_GET_Test()
        {
            mockRepository.Setup(m => m.AdminUnit.DeleteByID(1)).Verifiable();
            AdminUnitController target = new AdminUnitController(mockRepository.Object); // TODO: Initialize to an appropriate value
            int id = 1; // TODO: Initialize to an appropriate value
            ViewResult actual;
            actual = (ViewResult)target.Delete(id);
            Assert.AreEqual("Delete", actual.ViewName);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void Admin_Unit_Delete_Post_Positive_Test()
        {
            
            mockRepository.Setup(p => p.AdminUnit.Delete(new BLL.AdminUnit())).Verifiable();
            mockRepository.Setup(p => p.AdminUnit.FindById(0)).Returns(new BLL.AdminUnit());
            AdminUnitController target = new AdminUnitController(mockRepository.Object); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            RedirectToRouteResult actual;
            actual = (RedirectToRouteResult)target.Delete(id, new BLL.AdminUnit());
            Assert.IsTrue(actual.RouteValues.ContainsValue("Index"));
        }

        [TestMethod()]
        public void Admin_Unit_Delete_Post_Negative_Test()
        {
            mockRepository.Setup(p => p.AdminUnit.DeleteByID(0)).Verifiable();
            mockRepository.Setup(p => p.AdminUnit.FindById(0)).Returns(new BLL.AdminUnit());
            AdminUnitController target = new AdminUnitController(mockRepository.Object); // TODO: Initialize to an appropriate value
            base.SetModelError(target);
            int id = 0; // TODO: Initialize to an appropriate value
            ViewResult actual;
            actual = (ViewResult)target.Delete(id);
            Assert.AreEqual("Delete", actual.ViewName);
        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void Admin_Unit_Edit_GET_Test()
        {
            mockRepository.Setup(p => p.AdminUnit.FindById(0)).Returns(new AdminUnit());
            AdminUnitController target = new AdminUnitController(mockRepository.Object); // TODO: Initialize to an appropriate value
            int id = 0; // TODO: Initialize to an appropriate value
            //ActionResult expected = null; // TODO: Initialize to an appropriate value
            string viewName = "Edit";
            PartialViewResult actual;
            actual = (PartialViewResult)target.Edit(id);
            Assert.AreEqual(viewName, actual.ViewName);
        }
        /// <summary>
        ///A test for Edit
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void Admin_Unit_Edit_POST_Positive_Test()
        {
            
            mockRepository.Setup(p => p.AdminUnit.SaveChanges(new BLL.AdminUnit())).Verifiable();
            AdminUnitController target = new AdminUnitController(mockRepository.Object); // TODO: Initialize to an appropriate value
            JsonResult expected = new JsonResult();
            expected.Data = new { success = true };
            int id = 1;
            //string expected = "{ success = true }";
            JsonResult actual;
            actual = (JsonResult)target.Edit(id,new AdminUnit());
            Assert.IsNotNull(actual.Data);
            Assert.AreEqual(expected.Data.ToString(), actual.Data.ToString());
        }

        [TestMethod()]
        public void Admin_Unit_Edit_Negative_POST_Test()
        {
           
            mockRepository.Setup(p => p.AdminUnit.SaveChanges(new BLL.AdminUnit())).Verifiable();
            mockRepository.Setup(p => p.AdminUnit.FindById(0)).Verifiable();
            AdminUnitController target = new AdminUnitController(mockRepository.Object); // TODO: Initialize to an appropriate value
            
            base.SetModelError(target);

            PartialViewResult actual;
            int id = 1;
            var actualResult = (PartialViewResult)target.Edit(id, new AdminUnit());
            Assert.AreEqual("Edit", actualResult.ViewName);
        }

        /// <summary>
        ///A test for GetChildren
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void AdminUnits_GetChildren_JSON_Test()
        {
            mockRepository.Setup(m=>m.AdminUnit.GetChildren(0)).Returns(new List<BLL.AdminUnit>());
            AdminUnitController target = new AdminUnitController(mockRepository.Object); // TODO: Initialize to an appropriate value
            int unitId = 0; // TODO: Initialize to an appropriate value
            JsonResult actual;
            actual = (JsonResult)target.GetChildren(unitId);
            Assert.IsNotNull(actual.Data);
            //TypeDescriptor.AddProviderTransparent(new AssociatedMetaDataTypeTypeDescriptionProvider(),null)
        }
        
        /// <summary>
        ///A test for GetRegions
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void AdminUnits_GetRegions_JSON_Test()
        {
            mockRepository.Setup(m => m.AdminUnit.GetRegions()).Returns(new List<BLL.AdminUnit>());
            AdminUnitController target = new AdminUnitController(mockRepository.Object); // TODO: Initialize to an appropriate value
            JsonResult actual;
            actual = (JsonResult)target.GetRegions();
            Assert.IsNotNull(actual.Data);
        }

        /// <summary>
        ///A test for Index
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void AdminUnits_Index_GET_Test()
        {
            mockRepository.Setup(m => m.AdminUnit.GetAdminUnitTypes()).Returns(new List<BLL.AdminUnitType>());
            AdminUnitController target = new AdminUnitController(mockRepository.Object); // TODO: Initialize to an appropriate value
            //ActionResult expected = null; // TODO: Initialize to an appropriate value
            string viewName = "Index";
            ViewResult actual;
            actual = (ViewResult)target.Index();
            Assert.AreEqual(viewName, actual.ViewName);
        }
    }
}
