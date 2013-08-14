using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DRMFSS.BLL;
using DRMFSS.Web.Controllers;
using NUnit.Framework;

namespace DRMFSS.Web.Test
{
    [TestFixture]
    public class ContactControllerTests
    {
        #region SetUp / TearDown

        private ContactController _contactController;
        [SetUp]
        public void Init()
        {
            var contacts = new List<Contact>
                {
                    new Contact{ ContactID = 1, FDPID = 1, FirstName = "Abebe", LastName = "Kebede", PhoneNo = "251116123456"},
                    new Contact{ ContactID = 2, FDPID = 2, FirstName = "Abebech", LastName = "Zeleke", PhoneNo = "251911123456"}
                };
        }

        [TearDown]
        public void Dispose()
        { }

        #endregion

        #region Tests

        [Test]
        public void Test()
        {
        }

        #endregion
    }
}
