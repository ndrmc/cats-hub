
using DRMFSS.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Security.Principal;
using System.Web.Mvc;
using Moq;

namespace DRMFSS.Test
{
    /// <summary>
    /// Summary description for Test
    /// </summary>
    [TestClass]
    public class ControllerTestBase
    {
        public ControllerTestBase()
        {
            mockRepository = new Mock<BLL.IUnitOfWork>();
        }
        protected void InitController(System.Web.Mvc.Controller controller, TestUserType userType)
        {
            controller.ControllerContext = new ControllerContext()
            {
                Controller = controller,
                RequestContext = new System.Web.Routing.RequestContext(new MockHttpContext(userType), new System.Web.Routing.RouteData())
            };
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        protected Mock<BLL.IUnitOfWork> mockRepository;

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

        private class MockHttpContext : HttpContextBase
        {
            private readonly IPrincipal _adminUser = new GenericPrincipal(new GenericIdentity("admin"), new string[] { "Admin", "Data Entry" } /* roles */);
            private readonly IPrincipal _datatEntryUser = new GenericPrincipal(new GenericIdentity("admin"), new string[] {"Data Entry" } /* roles */);
            private readonly IPrincipal _public = new GenericPrincipal(new GenericIdentity("admin"), new string[] { "Data Entry" } /* roles */);


            //private readonly IPrincipal _user = new GenericPrincipal(new GenericIdentity("admin"), new string[] { "Admin", "Data Entry" } /* roles */);

            private TestUserType userType;
            public MockHttpContext()
                : base()
            {
                userType = TestUserType.Public;
            }

            public MockHttpContext(TestUserType userType)
                : base()
            {
                this.userType = userType;
            }

            public override IPrincipal User
            {
                get
                {
                    if (userType == TestUserType.Admin)
                        return _adminUser;
                    else if (userType == TestUserType.DataEntry)
                        return _datatEntryUser;
                    else 
                        return _public;
                }
                set
                {
                    base.User = value;
                }
            }

            
        }

        internal void SetModelError(Controller target)
        {
            target.ModelState.AddModelError("Test Key", "Tes Error Message");
        }
    }
}
