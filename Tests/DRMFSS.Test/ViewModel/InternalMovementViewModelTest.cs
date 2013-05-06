using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DRMFSS.BLL.ViewModels;
using DRMFSS.BLL;

namespace DRMFSS.Test.ViewModel
{
    [TestClass]
    public class InternalMovementViewModelTest
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
        public void Should_Create_InternalMovementViewModel_Object_ForCurentUser_and_Context()
        {
            var viewModel = new InternalMovementViewModel(repository, user);
            Assert.IsNotNull(viewModel);
            
        }
    }
}
