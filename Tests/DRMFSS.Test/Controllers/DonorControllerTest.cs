using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DRMFSS.Web.Controllers;

namespace DRMFSS.Test.Controllers
{
    /// <summary>
    /// Summary description for DonorControllerTest
    /// </summary>
    [TestClass]
    public class DonorControllerTest : ControllerTestBase
    {
        public DonorControllerTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Index_View_Test()
        {
            DonorController controller = new DonorController();
            ViewResult view = controller.Index();
            Assert.AreEqual("",view.ViewName);
        }

        [TestMethod]
        public void Index_View_Data_Test()
        {
            DonorController controller = new DonorController();
            ViewResult view = controller.Index();
            Assert.IsInstanceOfType(view.ViewData.Model, typeof( List<BLL.Donor> ));
        }

        [TestMethod]
        public void Index_View_Data_Count_Test()
        {
            DonorController controller = new DonorController();
            ViewResult view = controller.Index();
            Assert.AreEqual(((List<BLL.Donor>)view.ViewData.Model).Count, new BLL.CTSContext().Donors.Count());
        }


    }
}
