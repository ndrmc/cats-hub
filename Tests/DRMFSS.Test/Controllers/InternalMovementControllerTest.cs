using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DRMFSS.BLL.ViewModels;
using DRMFSS.BLL;
using DRMFSS.Web.Controllers;
using System.Web.Mvc;

namespace DRMFSS.Test.Controllers
{
    [TestClass]
    public class InternalMovementControllerTest
    {
        private IUnitOfWork repository;

        [TestInitialize]
        public void Setup()
        {
            repository = new UnitOfWork();
        }

        [TestMethod]
        public void Should_Add_InternalMovement()
        {
            InternalMovementViewModel actualViewModel = new InternalMovementViewModel();
            actualViewModel.FromStoreId = 3;
            actualViewModel.SelectedDate = DateTime.Now;
            actualViewModel.FromStackId = 4;
            actualViewModel.ReferenceNumber = "Test/001/";
            actualViewModel.CommodityId = 1;
            actualViewModel.ProgramId = 2;
            actualViewModel.ProjectCodeId = 33;
            actualViewModel.ShippingInstructionId = 46;
            actualViewModel.UnitId = 5;
            actualViewModel.QuantityInUnit = 100;
            actualViewModel.QuantityInMt = 200;
            actualViewModel.ToStoreId = 4;
            actualViewModel.ToStackId = 2;
            actualViewModel.ReasonId = 17;
            actualViewModel.Note = "Test transfer";
            actualViewModel.ApprovedBy = "Mr Test";

            ///expected values 
            InternalMovement expectdInternalMovementModel = new InternalMovement();
            expectdInternalMovementModel.PartitionID = 0;
            //expectdInternalMovementModel.TransactionGroupID
            expectdInternalMovementModel.TransferDate = actualViewModel.SelectedDate;
            expectdInternalMovementModel.ReferenceNumber = actualViewModel.ReferenceNumber;
            expectdInternalMovementModel.DReason = actualViewModel.ReasonId;
            expectdInternalMovementModel.Notes = actualViewModel.Note;
            expectdInternalMovementModel.ApprovedBy = actualViewModel.ApprovedBy;

            
            BLL.UserProfile actualUser = repository.UserProfile.GetUser("admin");

            Transaction expectedFromStoreTrasactionModel = new Transaction();
            expectedFromStoreTrasactionModel.PartitionID = 0;
            //expectedTrasactionModel.TransactionGroupID
            expectedFromStoreTrasactionModel.LedgerID = 2;
            expectedFromStoreTrasactionModel.HubOwnerID = actualUser.DefaultHub.HubOwner.HubOwnerID;
            //expectedTrasactionModel.AccountID = 
            expectedFromStoreTrasactionModel.HubID = actualUser.DefaultHub.HubID;
            expectedFromStoreTrasactionModel.StoreID = actualViewModel.FromStoreId;
            expectedFromStoreTrasactionModel.Stack = actualViewModel.FromStackId;
            expectedFromStoreTrasactionModel.ProjectCodeID = actualViewModel.ProjectCodeId;
            expectedFromStoreTrasactionModel.ShippingInstructionID = actualViewModel.ShippingInstructionId;
            expectedFromStoreTrasactionModel.ProgramID = actualViewModel.ProgramId;
            //expectedFromStoreTrasactionModel.ParentCommodityID
            //expectedFromStoreTrasactionModel.CommodityID
            expectedFromStoreTrasactionModel.QuantityInMT = actualViewModel.QuantityInMt;
            expectedFromStoreTrasactionModel.QuantityInUnit  = actualViewModel.QuantityInUnit;
            expectedFromStoreTrasactionModel.UnitID = actualViewModel.UnitId;

            Transaction expectedToStoreTrasactionModel = new Transaction();
            expectedToStoreTrasactionModel.PartitionID = 0;
            //expectedTrasactionModel.TransactionGroupID
            expectedToStoreTrasactionModel.LedgerID = 2;
            expectedToStoreTrasactionModel.HubOwnerID = actualUser.DefaultHub.HubOwner.HubOwnerID;
            //expectedTrasactionModel.AccountID = 
            expectedToStoreTrasactionModel.HubID = actualUser.DefaultHub.HubID;
            expectedToStoreTrasactionModel.StoreID = actualViewModel.FromStoreId;
            expectedToStoreTrasactionModel.Stack = actualViewModel.FromStackId;
            expectedToStoreTrasactionModel.ProjectCodeID = actualViewModel.ProjectCodeId;
            expectedToStoreTrasactionModel.ShippingInstructionID = actualViewModel.ShippingInstructionId;
            expectedToStoreTrasactionModel.ProgramID = actualViewModel.ProgramId;
            //expectedFromStoreTrasactionModel.ParentCommodityID
            //expectedFromStoreTrasactionModel.CommodityID
            expectedToStoreTrasactionModel.QuantityInMT = actualViewModel.QuantityInMt;
            expectedToStoreTrasactionModel.QuantityInUnit  = actualViewModel.QuantityInUnit;
            expectedToStoreTrasactionModel.UnitID = actualViewModel.UnitId;

            //var redirectToRouteResult = (RedirectToRouteResult)  new InternalMovementController().Create(actualViewModel);

            //repository.InternalMovement.AddNewInternalMovement(actualViewModel, actualUser);
            repository.Transaction.SaveInternalMovementTrasnsaction(actualViewModel, actualUser);

            //CollectionAssert.Contains(repository.Transaction.GetAll(), expectedFromStoreTrasactionModel);
            //CollectionAssert.Contains(repository.InternalMovement.GetAll(), expectdInternalMovementModel);
        }

        [TestMethod]
        public void Should_Edit_InternalMovement()
        {

        }
    }
}
