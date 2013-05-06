using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DRMFSS.BLL;

namespace DRMFSS.Test.ViewModel
{
    [TestClass]
    public class LossesAndAdjustmentsViewModelTest
    {
        private IUnitOfWork repository;
        private UserProfile user; 
        [TestInitialize]
        public void Setup()
        {
            repository = new UnitOfWork();
            user = new UserProfile { UserName = "Test" };
        }

        [TestCleanup]
        public void TearDown()
        {

        }


        [TestMethod]
        public void Should_Create_LoessesAndAdjustments_Object_ForCurrentUser_and_Context()
        {

        }
    }
}
