using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DRMFSS.BLL;
using System.Collections.Generic;
using Moq;

namespace DRMFSS.Test
{


    [TestClass()]
    public class CommodityTypeRepositoryTest
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
        public void CommodityTypeRepositoryConstructorTest()
        {
             IUnitOfWork commodityRepository = new UnitOfWork();
        }

        public CommodityTypeRepositoryTest()
        {

            List<CommodityType> testCommoditTypes = new List<CommodityType>
                {
                                  
                    new CommodityType {  CommodityTypeID = 1,Name = "Food" },
                    new CommodityType {  CommodityTypeID = 2, Name = "Non Food"},
                    new CommodityType {  CommodityTypeID = 3, Name = "Equipments"}
                };

            // Mock the Commoditytype Repository using Moq
            Mock<IUnitOfWork> mockCommodityTypeRepository = new Mock<IUnitOfWork>();

            // Return all the Commodity types
            mockCommodityTypeRepository.Setup(mr => mr.CommodityType.GetAll()).Returns(testCommoditTypes);

            // return a Commoditytype by CommoditytypeId
            mockCommodityTypeRepository.Setup(mr => mr.CommodityType.FindById(
               It.IsAny<int>())).Returns((int i) => testCommoditTypes.Where(x => x.CommodityTypeID == i).Single());

            //return a commoditytype by it's name
            mockCommodityTypeRepository.Setup(mr => mr.CommodityType.GetCommodityByName(
                 It.IsAny<string>())).Returns((string i) => testCommoditTypes.Where(x => x.Name == i).Single());

            // delete a Commoditytype by CommodityId
            mockCommodityTypeRepository.Setup(mr => mr.CommodityType.DeleteByID(
               It.IsAny<int>())).Returns(
               (int i) =>
               {
                   var original = testCommoditTypes.Single
                       (q => q.CommodityTypeID == i);

                   //see if there is a reference in side the commodity table
                   var testCommodities = new UnitOfWork().Commodity.GetAll();

                   var childsCount = testCommodities.Count(c => c.CommodityTypeID == i);

                   if (original == null || childsCount != 0)
                   {
                       return false;
                   }
                   else
                   {
                       testCommoditTypes.Remove(original);
                       return true;
                   }


               });

            //test deletion of commodity
                    mockCommodityTypeRepository.Setup(mr => mr.CommodityType.Delete(
                       It.IsAny<CommodityType>())).Returns(
                       (CommodityType target) =>
                       {
                           var original = testCommoditTypes.Single
                               (q => q.CommodityTypeID == target.CommodityTypeID);

                           //var childsCount = testCommodities.Count(c => c.ParentID == target.CommodityID);

                           //see if there is a reference in side the commodity table
                           var testCommoditiesCollection = new UnitOfWork().Commodity.GetAll();

                           var childsCount = testCommoditiesCollection.Count(c => c.CommodityTypeID == target.CommodityTypeID);

                           if (original == null || childsCount != 0)
                           {
                               return false;
                           }
                           else
                           {
                               testCommoditTypes.Remove(original);
                               return true;
                           }


                       });

            //returns bool if we can save one (editing )scheme
            mockCommodityTypeRepository.Setup(mr => mr.CommodityType.SaveChanges(It.IsAny<CommodityType>())).Returns(
                (CommodityType target) =>
                {

                    var original = testCommoditTypes.Single
                            (q => q.CommodityTypeID == target.CommodityTypeID);

                    if (original == null)
                    {
                        return false;
                    }
                    original.Name = target.Name;
                    return true;
                });

            //TODO remove the lines below duplicate of the 
            mockCommodityTypeRepository.Setup(mr => mr.CommodityType.Add(It.IsAny<CommodityType>())).Returns(
                (CommodityType target) =>
                {
                    if (target.CommodityTypeID.Equals(default(int)))
                    {
                        target.CommodityTypeID = testCommoditTypes.Count() + 1;
                        testCommoditTypes.Add(target);
                    }
                    else
                    {
                        var original = testCommoditTypes.Single
                            (q => q.CommodityTypeID == target.CommodityTypeID);

                        if (original == null)
                        {
                            return false;
                        }
                        original.Name = target.Name;
                    }

                    return true;
                });

            this.MockCommodityTypeRepository = mockCommodityTypeRepository.Object;
        }

        /// <summary>
        /// Our Mock commodity type Repository for use in testing
        /// </summary>
        public IUnitOfWork MockCommodityTypeRepository;

        // Web should not test this part as it's not ours some how
        //[TestMethod()]
        //public void AddTest()
        //{
        //    CommodityTypeRepository target = new CommodityTypeRepository(); // TODO: Initialize to an appropriate value
        //    CommodityType entity = null; // TODO: Initialize to an appropriate value
        //    bool expected = false; // TODO: Initialize to an appropriate value
        //    bool actual;
        //    actual = target.Add(entity);
        //    Assert.AreEqual(expected, actual);
        //}

        //[TestMethod()]
        //public void DeleteTest()
        //{
        //    CommodityTypeRepository target = new CommodityTypeRepository(); // TODO: Initialize to an appropriate value
        //    CommodityType entity = null; // TODO: Initialize to an appropriate value
        //    bool expected = false; // TODO: Initialize to an appropriate value
        //    bool actual;
        //    actual = target.Delete(entity);
        //    Assert.AreEqual(expected, actual);
        //}

        //[TestMethod()]
        //public void DeleteByIDTest()
        //{
        //    CommodityTypeRepository target = new CommodityTypeRepository(); // TODO: Initialize to an appropriate value
        //    int id = 0; // TODO: Initialize to an appropriate value
        //    bool expected = false; // TODO: Initialize to an appropriate value
        //    bool actual;
        //    actual = target.DeleteByID(id);
        //    Assert.AreEqual(expected, actual);
        //}

        [TestMethod()]
        public void Find_Commodity_Type_By_Id_Test()
        {
            IUnitOfWork target = new UnitOfWork();
            // Try finding a product by id
            CommodityType testProduct = this.MockCommodityTypeRepository.CommodityType.FindById(2);
 
            Assert.IsNotNull(testProduct); 
            Assert.IsInstanceOfType(testProduct, typeof(CommodityType)); // Test type
            Assert.AreEqual("Non Food", testProduct.Name); // Verify it is the right product
        }

        //[TestMethod()]
        //public void GetAllTest()
        //{
        //    CommodityTypeRepository target = new CommodityTypeRepository(); // TODO: Initialize to an appropriate value
        //    List<CommodityType> expected = null; // TODO: Initialize to an appropriate value
        //    List<CommodityType> actual;
        //    actual = target.GetAll();
        //    Assert.AreEqual(expected, actual);
        //}

        //[TestMethod()]
        //public void SaveChangesTest()
        //{
        //    CommodityTypeRepository target = new CommodityTypeRepository(); // TODO: Initialize to an appropriate value
        //    CommodityType entity = null; // TODO: Initialize to an appropriate value
        //    bool expected = false; // TODO: Initialize to an appropriate value
        //    bool actual;
        //    actual = target.SaveChanges(entity);
        //    Assert.AreEqual(expected, actual);
        //}
    }
}
