using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.Repository;
using DRMFSS.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web.Mvc;
using Moq;

namespace DRMFSS.Test
{


    [TestClass()]
    public class CommodityControllerTest 
    {


        private TestContext testContextInstance;

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

        //public CommodityControllerTest()
        //{
        //    //TODO use the repository we prepared for testing the Commodity model
        //}

        public CommodityControllerTest()
        {

            // create some mock products to play with
            List<Commodity> testCommodities = new List<Commodity>
                {
                    new Commodity { CommodityID = 1, Name = "Wheat",LongName = "",CommodityTypeID = 1, ParentID = null },
                    new Commodity { CommodityID = 5, Name = "Yellow Wheat",LongName = "",CommodityTypeID = 1, ParentID = 1 },
                    new Commodity { CommodityID = 6, Name = "Green Wheat",LongName = "",CommodityTypeID = 1, ParentID = 1 },
                
                    new Commodity { CommodityID = 3, Name = "CSB",LongName = "",CommodityTypeID = 1, ParentID = null },
                    new Commodity { CommodityID = 8, Name = "Beans",LongName = "",CommodityTypeID = 1, ParentID = 3 },
                };

            // Mock the AdminUNit Repository using Moq
            Mock<IUnitOfWork> mockCommodityRepository = new Mock<IUnitOfWork>();

            // Return all the Commodities
            mockCommodityRepository.Setup(mr => mr.Commodity.GetAll()).Returns(testCommodities);

            // return a Commodity by CommodityId
            mockCommodityRepository.Setup(mr => mr.Commodity.FindById(
               It.IsAny<int>())).Returns((int i) => testCommodities.Where(x => x.CommodityID == i).SingleOrDefault());

            //return all parent commodities
            mockCommodityRepository.Setup(mr => mr.Commodity.GetAllParents())
                .Returns(testCommodities.Where(x => x.ParentID == null).ToList());

            //return all children commodities
            mockCommodityRepository.Setup(mr => mr.Commodity.GetAllSubCommodities())
                .Returns(testCommodities.Where(x => x.ParentID != null).ToList());

            //return a commodity by it's name
            mockCommodityRepository.Setup(mr => mr.Commodity.GetCommodityByName(
                 It.IsAny<string>())).Returns((string i) => testCommodities.Where(x => x.Name == i).SingleOrDefault());

            //retun all commodities by thier parent 
            mockCommodityRepository.Setup(mr => mr.Commodity.GetAllSubCommoditiesByParantId(
                It.IsAny<int>())).Returns((int i) => testCommodities.Where(x => x.ParentID == i).ToList());

            //return a commodity by it's name
            mockCommodityRepository.Setup(mr => mr.CommodityType.GetAll(
                 )).Returns(new List<BLL.CommodityType>());

            this.MockCommodityRepository = mockCommodityRepository.Object;
        }

        public readonly IUnitOfWork MockCommodityRepository = new UnitOfWork();

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


        
        [TestMethod()]
        public void Commodity_Controller_Constructor_With_Two_Repos_Test()
        {
            IUnitOfWork commodityRepository = this.MockCommodityRepository; // TODO: Initialize to an appropriate value
            ICommodityTypeRepository commodityTypeRepository = null; // TODO: Initialize to an appropriate value
            CommodityController target = new CommodityController(commodityRepository);
        }

        [TestMethod()]
        public void Commodity_Controller_Constructor_With_One_RepoTest()
        {
            IUnitOfWork repository = this.MockCommodityRepository; 
            CommodityController target = new CommodityController(repository);
        }

        [TestMethod()]
        public void Commodity_List_Partial_Test()
        {

            IUnitOfWork repository = this.MockCommodityRepository;
            CommodityController target = new CommodityController(repository);
            ActionResult expected = new ViewResult();
            ActionResult actual = null;

            //act call the index will call getall() from the repo and returns a list of comods
            actual = target.CommodityListPartial();
            
            PartialViewResult result = actual as PartialViewResult;

            Assert.IsNotNull(result); 

            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IEnumerable<Commodity>));

            Assert.IsNull(result.ViewBag.Title);
            Assert.IsNotNull(result.ViewBag.ParentID);
            Assert.IsNotNull(result.ViewBag.SelectedCommodityID);
            Assert.AreEqual("_CommodityPartial",result.ViewName);

            //just for testing that we are really using the test model objects
            var count = result.ViewData.Model as IEnumerable<Commodity>;
            Assert.AreEqual(2, count.Count()); 
   
        }

        [TestMethod()]
        public void Commodity_Create_Post_For_Valid_Model_Test()
        {
            IUnitOfWork repository = this.MockCommodityRepository; 
            CommodityController target = new CommodityController(repository);
            Commodity commodity = new Commodity
                                        {
                                            Name = "Gebse",
                                            LongName = "",
                                            CommodityTypeID = 1,
                                            ParentID = 1
                                        };

            int commodityPreCount = this.MockCommodityRepository.Commodity.GetAll().Count;
            Assert.AreEqual(5, commodityPreCount); 
            JsonResult expected = new JsonResult();
            ActionResult actual;

            //Act
            actual = target.Create(commodity);
            JsonResult result = actual as JsonResult;
            expected.Data = new { success = true };

            //the number of commodities should increase by 1
            int commodityPostCount = this.MockCommodityRepository.Commodity.GetAll().Count;
            Assert.AreEqual(commodityPreCount + 1, commodityPostCount); 
            Assert.AreEqual(expected.Data.ToString() , result.Data.ToString());
            
        }

        [TestMethod()]
        public void Commodity_Create_Post_For_InValid_Model_Test()
        {
            IUnitOfWork repository = this.MockCommodityRepository;
            CommodityController target = new CommodityController(repository);
            Commodity commodity = new Commodity
            {
                LongName = "",
                CommodityTypeID = 1,
                ParentID = 1
            };

            //SetModelError(target);

            int commodityPreCount = this.MockCommodityRepository.Commodity.GetAll().Count;
            Assert.AreEqual(5, commodityPreCount); // Verify the expected Number pre-insert
            PartialViewResult expected = new PartialViewResult();
            ActionResult actual;

      
            //Act
            actual = target.Create(commodity);
            
            PartialViewResult result = actual as PartialViewResult;
            Assert.IsNotNull(result);
            //no increase in the number of commodities
            int commodityPostCount = this.MockCommodityRepository.Commodity.GetAll().Count;
            Assert.AreEqual(commodityPreCount, commodityPostCount);

        }

        [TestMethod()]
        public void Commodity_Create_Parent_Commodity_Get_Test()
        {
           
            IUnitOfWork repository = this.MockCommodityRepository;

            CommodityController target = new CommodityController(repository); 
            int type = 1; 
            Nullable<int> Parent = null; //parent commodity is set to null cos it's the parent itself
            ActionResult expected = null; 
            ActionResult actual;
            actual = target.Create(type, Parent);

            PartialViewResult result = actual as PartialViewResult;

            Assert.IsNotNull(result);

            Assert.AreEqual(result.Model.GetType(), typeof(Commodity));
            
            Assert.IsNotNull(result.ViewBag.CommodityTypeID);
            //major diff between parent and child 
            Assert.IsNull(result.ViewBag.ParentID);
            Assert.AreEqual(result.ViewBag.isParent, false);
            Assert.IsNull(result.ViewBag.CommodityType);
            Assert.IsNull(result.ViewBag.ParentCommodity);

        }

        [TestMethod()]
        public void Commodity_Create_Sub_Commodity_Get_Test()
        {
            IUnitOfWork repository = this.MockCommodityRepository;
            CommodityController target = new CommodityController(repository);
            int type = 0; 
            Nullable<int> Parent = 1; //who is the parent commodity
            ActionResult expected = null;
            ActionResult actual;
            actual = target.Create(type, Parent);

            PartialViewResult result = actual as PartialViewResult;

            Assert.IsNotNull(result);

            Assert.AreEqual(result.Model.GetType(), typeof(Commodity));
            
            Assert.IsNotNull(result.ViewBag.CommodityTypeID);
            //major diff between parent and child
            Assert.IsNotNull(result.ViewBag.ParentID);
            Assert.AreEqual(result.ViewBag.isParent, true);
            Assert.IsNotNull(result.ViewBag.CommodityType);
            Assert.IsNotNull(result.ViewBag.ParentCommodity);
        }    
        
        [TestMethod()]
        public void Commodity_Delete_Get_Test()
        {
            
            IUnitOfWork repository = this.MockCommodityRepository;
            CommodityController target = new CommodityController(repository);
            int id = 1; //details of wheat
            ViewResult expected = new ViewResult();
            expected.ViewData.Model = new Commodity
            {
                CommodityID = 1,
                Name = "Wheat",
                LongName = "",
                CommodityTypeID = 1,
                ParentID = null
            };
            ActionResult actual;
            actual = target.Delete(id);

            ViewResult result = actual as ViewResult;

            Assert.IsNotNull(result);

            Assert.AreEqual(result.ViewData.Model.GetType(), typeof(Commodity));


            //displays the correct info for deletion process

            Commodity CommodityX = result.ViewData.Model as Commodity;
            Commodity CommodityY = expected.ViewData.Model as Commodity;
            Assert.AreEqual(CommodityY.CommodityID, CommodityX.CommodityID);
            Assert.AreEqual(CommodityY.Name, CommodityX.Name);
            Assert.AreEqual(CommodityY.LongName, CommodityX.LongName);
            Assert.AreEqual(CommodityY.CommodityTypeID, CommodityX.CommodityTypeID);
            Assert.AreEqual(CommodityY.ParentID, CommodityX.ParentID);

            Assert.AreEqual("Delete", result.ViewName);
            Assert.AreEqual(result.ViewData.ModelState.IsValid, true);

        }

        [TestMethod()]
        public void Commodity_Delete_Confirmed_Success_Test()
        {
            
            IUnitOfWork repository = this.MockCommodityRepository;
            CommodityController target = new CommodityController(repository);
            //Commodity commodity = new Commodity
            //{
            //    Name = "Gebse",
            //    LongName = "",
            //    CommodityTypeID = 1,
            //    ParentID = 1
            //};
            int id = 6; //delete the child yellow wheat commodity

            int commodityPreCount = this.MockCommodityRepository.Commodity.GetAll().Count;
            Assert.AreEqual(5, commodityPreCount);
            JsonResult expected = new JsonResult();
            ActionResult actual;
            const string testUrl = "/Commodity";

            //Act
            actual = target.DeleteConfirmed(id);
            RedirectToRouteResult result = actual as RedirectToRouteResult;
           
            //the number of commodities should decrease by 1
            int commodityPostCount = this.MockCommodityRepository.Commodity.GetAll().Count;
            Assert.AreEqual(commodityPreCount - 1, commodityPostCount);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));

        }

        [TestMethod()]
        public void Commodity_Delete_Confirmed_Faliure_Test()
        {

            IUnitOfWork repository = this.MockCommodityRepository;
            CommodityController target = new CommodityController(repository);
            Commodity commodity = new Commodity
            {
                Name = "Gebse",
                LongName = "",
                CommodityTypeID = 1,
                ParentID = null
            };
            int id = 1; //delete the wheat commodity

            int commodityPreCount = this.MockCommodityRepository.Commodity.GetAll().Count;
            Assert.AreEqual(5, commodityPreCount);
            JsonResult expected = new JsonResult();
            ActionResult actual;
            
            //Act
            actual = target.DeleteConfirmed(id);
            ViewResult result = actual as ViewResult;

            //the number of commodities should decrease by 1
            int commodityPostCount = this.MockCommodityRepository.Commodity.GetAll().Count;
            Assert.AreEqual(commodityPreCount, commodityPostCount);
            
            Assert.AreEqual(result.ViewBag.ERROR,true);
            Assert.IsNotNull(result.ViewBag.ERROR_MSG);

        }

        [TestMethod()]
        public void Commodity_Details_Test()
        {
            IUnitOfWork repository = this.MockCommodityRepository;
            CommodityController target = new CommodityController(repository);
            int id = 1; //details of wheat
            ViewResult expected = new ViewResult();
            expected.ViewData.Model = new Commodity
                                          {
                                              CommodityID = 1,
                                              Name = "Wheat",
                                              LongName = "",
                                              CommodityTypeID = 1,
                                              ParentID = null
                                          };
            ViewResult actual;
            actual = target.Details(id);
            ViewResult result = actual as ViewResult;

            Assert.IsNotNull(result);

            Assert.AreEqual(actual.ViewData.Model.GetType(), typeof (Commodity));

            Commodity CommodityX = result.ViewData.Model as Commodity;
            Commodity CommodityY = expected.ViewData.Model as Commodity;
            Assert.AreEqual(CommodityY.CommodityID, CommodityX.CommodityID);
            Assert.AreEqual(CommodityY.Name, CommodityX.Name);
            Assert.AreEqual(CommodityY.LongName, CommodityX.LongName);
            Assert.AreEqual(CommodityY.CommodityTypeID, CommodityX.CommodityTypeID);
            Assert.AreEqual(CommodityY.ParentID, CommodityX.ParentID);

            Assert.AreEqual("Details", result.ViewName);
            Assert.AreEqual(result.ViewData.ModelState.IsValid,true);
        }

        [TestMethod()]
        public void Commodity_Edit_Post_Valid_Model_Test()
        {
            IUnitOfWork repository = this.MockCommodityRepository;
            CommodityController target = new CommodityController(repository);
            Commodity commodity = new Commodity
            {
                  CommodityID = 1,
                  Name = "Wheat",
                  LongName = "",
                  CommodityTypeID = 1, 
                  ParentID = null
            };

            //SetModelError(target);

            JsonResult expected = new JsonResult();
            ActionResult actual;

            //Act
            actual = target.Edit(commodity);

            JsonResult result = actual as JsonResult;
            expected.Data = new { success = true };

           Assert.AreEqual(expected.Data.ToString(), result.Data.ToString());
         
            
        }

        [TestMethod()]
        public void Commodity_Edit_Post_InValid_Model_Test()
        {
            IUnitOfWork repository = this.MockCommodityRepository;
            CommodityController target = new CommodityController(repository);
            Commodity commodity = new Commodity
            {
                CommodityID = 1,
                Name = "aja",
                LongName = "",
                CommodityTypeID = 1,
                ParentID = 1
            };

            //SetModelError(target);

            PartialViewResult expected = new PartialViewResult();
            ActionResult actual;

            //Act
            actual = target.Edit(commodity);

            PartialViewResult result = actual as PartialViewResult;
            Assert.IsNotNull(result);


        }
        
        [TestMethod()]
         public void Commodity_Edit_Get_Test()
        {

            IUnitOfWork repository = this.MockCommodityRepository;
            CommodityController target = new CommodityController(repository);
            int id = 1;
            ActionResult actual;
            actual = target.Edit(id);

            PartialViewResult result = actual as PartialViewResult;

            Assert.IsNotNull(result);

            Assert.AreEqual(result.Model.GetType(), typeof(Commodity));

            Assert.IsNotNull(result.ViewBag.CommodityTypeID);

          //major diff between parent and child 
           //Assert.IsNull(result.ViewBag.ParentID);
            Assert.AreEqual(result.ViewBag.isParent, false);
            Assert.IsNull(result.ViewBag.CommodityType);
            Assert.IsNull(result.ViewBag.ParentCommodity);

        }

        [TestMethod()]
        public void Get_Parent_List_Test()
        {
            IUnitOfWork repository = this.MockCommodityRepository; 
            CommodityController target = new CommodityController(repository);
            ActionResult expected = null; 
            ActionResult actual;

            actual = target.GetParentList();

            JsonResult result = actual as JsonResult;

            
            Assert.AreEqual(result.Data.GetType(), typeof(SelectList));

            var count = result.Data as SelectList;
            Assert.AreEqual(2, count.Count());
        }

        [TestMethod()]
        public void Commodity_Controller_Index_Test()
        {
            //this three lines are good for testing null pointer exception validations
            //Mock<IUnitOfWork> mockCommodityRepository = new Mock<IUnitOfWork>();
            //CommodityController target = new CommodityController(mockCommodityRepository.Object); // TODO: Initialize to an appropriate value
            // mockCommodityRepository.Object.GetAll();//SetupGet(d => d.GetAllParents()).SetupGet(d => d.GetAll()).Returns(mockCommodityRepository.Object.GetAll());

            IUnitOfWork repository = this.MockCommodityRepository; 
            CommodityController target = new CommodityController(repository); 
            ActionResult expected = new ViewResult(); 
            
           
            //act call the index will call getall() from the repo and returns a list of comods
            ActionResult actual = target.Index(); 


            ViewResult result = actual as ViewResult;

            Assert.IsNotNull(result);
            
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IEnumerable<Commodity>));

            Assert.IsNull(result.ViewBag.Title);
            Assert.IsNotNull(result.ViewBag.ParentID);
            Assert.IsNotNull(result.ViewBag.SelectedCommodityID);
            Assert.IsNotNull(result.ViewBag.Parents);
            

            //just for testing that we are really using the test model objects
            var count = result.ViewData.Model as IEnumerable<Commodity>;
            Assert.AreEqual(count.Count(),5); 
        }

        [TestMethod()]
        public void Sub_Commodity_List_Partial_Test()
        {
            IUnitOfWork repository = this.MockCommodityRepository; 
            CommodityController target = new CommodityController(repository); 
            ActionResult expected = new ViewResult();
            
            //get all childrens of id (i.e. wheat for this case)
            int id = 1; 
            ActionResult actual;

            actual = target.SubCommodityListPartial(id);
            
            PartialViewResult result = actual as PartialViewResult;

            Assert.IsNotNull(result);

            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IEnumerable<Commodity>));
            Assert.AreEqual(result.ViewBag.SelectedCommodityID,id);
            Assert.AreEqual(result.ViewBag.ShowParentCommodity,true);
          
        }

        [TestMethod()]
        public void UpdateTest()
        {
            IUnitOfWork repository = this.MockCommodityRepository;
            CommodityController target = new CommodityController(repository);
            ActionResult expected = new ViewResult();
            Nullable<int> param = 1; // TODO: Initialize to an appropriate value
            ActionResult actual;

            //act call the index will call getall() from the repo and returns a list of comods
            actual = target.Update(param);
            
            PartialViewResult result = actual as PartialViewResult;

            Assert.IsNotNull(result);

            Assert.IsInstanceOfType(result.ViewData.Model, typeof(IEnumerable<Commodity>));

            Assert.IsNull(result.ViewBag.Title);
            Assert.IsNotNull(result.ViewBag.ParentID);
            Assert.IsNotNull(result.ViewBag.SelectedCommodityID);
            Assert.IsNotNull(result.ViewBag.Parents);


            //just for testing that we are really using the test model objects
            var count = result.ViewData.Model as IEnumerable<Commodity>;
            Assert.AreEqual(count.Count(), 5); 
        }
        
        [TestMethod()]
        public void CommodityControllerConstructorTest2()
        {
            CommodityController target = new CommodityController();
        }
    }
}
