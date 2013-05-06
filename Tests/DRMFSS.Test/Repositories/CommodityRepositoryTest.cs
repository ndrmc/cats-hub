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
    public class CommodityRepositoryTest
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


        public CommodityRepositoryTest()
        {

            //List<Commodity> testChildren = new List<Commodity>
            //    {
            //        new Commodity { CommodityID = 5, Name = "Yellow Wheat",LongName = "",CommodityTypeID = 1, ParentID = 1 },
            //        new Commodity { CommodityID = 7, Name = "Green Wheat",LongName = "",CommodityTypeID = 1, ParentID = 1 },
            //   };
            
            
            // create some mock products to play with
            List<Commodity> testCommodities = new List<Commodity>
                {
                           
                           
                    new Commodity { CommodityID = 1, Name = "Wheat",LongName = "",CommodityTypeID = 1, ParentID = null },
                    new Commodity { CommodityID = 5, Name = "Yellow Wheat",LongName = "",CommodityTypeID = 1, ParentID = 1 },
                    new Commodity { CommodityID = 7, Name = "Green Wheat",LongName = "",CommodityTypeID = 1, ParentID = 1 },
                
                    new Commodity { CommodityID = 3, Name = "CSB",LongName = "",CommodityTypeID = 1, ParentID = null },
                    new Commodity { CommodityID = 8, Name = "Beans",LongName = "",CommodityTypeID = 1, ParentID = 3 },
                };

            // Mock the Commodity Repository using Moq
            Mock<ICommodityRepository> mockCommodityRepository = new Mock<ICommodityRepository>();

            // Return all the Commodities
            mockCommodityRepository.Setup(mr => mr.GetAll()).Returns(testCommodities);

            // return a Commodity by CommodityId
            mockCommodityRepository.Setup(mr => mr.FindById(
               It.IsAny<int>())).Returns((int i) => testCommodities.Where(x => x.CommodityID == i).Single());

            //return all parent commodities
            mockCommodityRepository.Setup(mr => mr.GetAllParents())
                .Returns(testCommodities.Where(x => x.ParentID == null).ToList());
            
            //return all children commodities
            mockCommodityRepository.Setup(mr => mr.GetAllSubCommodities())
                .Returns(testCommodities.Where(x => x.ParentID != null).ToList());

            //return a commodity by it's name
            mockCommodityRepository.Setup(mr => mr.GetCommodityByName(
                 It.IsAny<string>())).Returns((string i) => testCommodities.Where(x => x.Name == i).Single());
            
            //retun all commodities by thier parent 
            mockCommodityRepository.Setup(mr=>mr.GetAllSubCommoditiesByParantId(
                It.IsAny<int>())).Returns((int i) => testCommodities.Where(x => x.ParentID == i).ToList());

            // return a Commodity by CommodityId
            mockCommodityRepository.Setup(mr => mr.DeleteByID(
               It.IsAny<int>())).Returns(
               (int i)=>
                   {
                       var original = testCommodities.Single
                           (q => q.CommodityID == i);

                       var childsCount = testCommodities.Count(c => c.ParentID == i);

                       if (original == null || childsCount != 0)
                       {
                           return false;
                       }
                       else
                       {
                           testCommodities.Remove(original);
                           return true;
                       }
                       

                   });

            //test deletion of commodity by id
            mockCommodityRepository.Setup(mr => mr.Delete(
                       It.IsAny<Commodity>())).Returns(
                       (Commodity target) =>
                       {
                           var original = testCommodities.Single
                               (q => q.CommodityID == target.CommodityID);

                           var childsCount = testCommodities.Count(c => c.ParentID == target.CommodityID);

                           if (original == null || childsCount != 0)
                           {
                               return false;
                           }
                           else
                           {
                               testCommodities.Remove(original);
                               return true;
                           }


                       });

            //returns bool if we can save one additional(adding )scheme
            mockCommodityRepository.Setup(mr => mr.SaveChanges(It.IsAny<Commodity>())).Returns(
                (Commodity target) =>
                {
                    
                    var original = testCommodities.Single
                            (q => q.CommodityID == target.CommodityID);
 
                        if (original == null)
                        {
                            return false;
                        }
                        original.Name = target.Name;
                        original.LongName = target.LongName;
                        original.CommodityTypeID = target.CommodityTypeID;
                        original.ParentID = target.ParentID;
                    return true;
                });

            //TODO remove the lines below duplicate of the 
            mockCommodityRepository.Setup(mr => mr.Add(It.IsAny<Commodity>())).Returns(
                (Commodity target) =>
                {
                    if (target.CommodityID.Equals(default(int)))
                    {
                        target.CommodityID = testCommodities.Count() + 1;
                        testCommodities.Add(target);
                    }
                    else
                    {
                        var original = testCommodities.Single
                            (q => q.CommodityID == target.CommodityID);

                        if (original == null)
                        {
                            return false;
                        }
                        original.Name = target.Name;
                        original.LongName = target.LongName;
                        original.CommodityTypeID = target.CommodityTypeID;
                        original.ParentID = target.ParentID;
                    }

                    return true;
                });

            this.MockCommodityRepository = mockCommodityRepository.Object;
        }

        /// <summary>
        /// Our Mock commodity Repository for use in testing
        /// </summary>
        public ICommodityRepository MockCommodityRepository;

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
        public void Get_Commodity_By_Name_Test()
        {
            string name = "Green Wheat";
            Commodity actual = this.MockCommodityRepository.GetCommodityByName(name); // TODO: Initialize to an appropriate value
            Commodity expected = new Commodity { CommodityID = 7, Name = "Green Wheat", LongName = "", CommodityTypeID = 1, ParentID = 1 };
            
            Assert.IsInstanceOfType(actual,typeof(Commodity));
            Assert.AreEqual(actual.GetType(), expected.GetType());
            Assert.AreEqual(actual.CommodityID, expected.CommodityID);
            Assert.AreEqual(actual.Name, expected.Name);
            Assert.AreEqual(actual.CommodityTypeID, expected.CommodityTypeID);
            Assert.AreEqual(actual.ParentID, expected.ParentID);
        
        }

        [TestMethod()]
        public void Get_All_Parents_Test()
        {
            //Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            List<Commodity> expected = new List<Commodity>
                {
                    new Commodity { CommodityID = 1, Name = "Wheat",LongName = "",CommodityTypeID = 1, ParentID = null },
                    new Commodity { CommodityID = 3, Name = "CSB",LongName = "",CommodityTypeID = 1, ParentID = null },
               };
            
            List<Commodity> actual = this.MockCommodityRepository.GetAllParents();

            Assert.IsInstanceOfType(actual, expected.GetType()); // Test type
            Assert.AreEqual(actual.Count,expected.Count);
            //are all elemetnts found
            foreach (var commodityExp in expected)
            {
                Assert.IsTrue(actual.Contains(actual.Find(c => c.CommodityID == commodityExp.CommodityID)));    
            }
            
            //Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Get_All_Sub_Commodities_Test()
        {
            //Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            List<Commodity> expected = new List<Commodity>
                {
                    new Commodity { CommodityID = 5, Name = "Yellow Wheat",LongName = "",CommodityTypeID = 1, ParentID = 1 },
                    new Commodity { CommodityID = 7, Name = "Green Wheat",LongName = "",CommodityTypeID = 1, ParentID = 1 },
                    new Commodity { CommodityID = 8, Name = "Beans",LongName = "",CommodityTypeID = 1, ParentID = 3 },
               };

            List<Commodity> actual = this.MockCommodityRepository.GetAllSubCommodities();

            Assert.IsInstanceOfType(actual, expected.GetType()); // Test type
            Assert.AreEqual(actual.Count, expected.Count);
            //are all elemetnts found
            foreach (var commodityExp in expected)
            {
                Assert.IsTrue(actual.Contains(actual.Find(c => c.CommodityID == commodityExp.CommodityID)));
            }

            //Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Get_All_Sub_Commodities_By_ParantId_Test()
        {
            //Commodity target = new Commodity(); // TODO: Initialize to an appropriate value


            List<Commodity> expected = new List<Commodity>
                {
                    new Commodity { CommodityID = 5, Name = "Yellow Wheat",LongName = "",CommodityTypeID = 1, ParentID = 1 },
                    new Commodity { CommodityID = 7, Name = "Green Wheat",LongName = "",CommodityTypeID = 1, ParentID = 1 },
               };

            List<Commodity> actual = this.MockCommodityRepository.GetAllSubCommoditiesByParantId(1);

            Assert.IsInstanceOfType(actual, expected.GetType()); // Test type
            Assert.AreEqual(actual.Count, expected.Count);
            //are all elemetnts found
            foreach (var commodityExp in expected)
            {
                Assert.IsTrue(actual.Contains(actual.Find(c => c.CommodityID == commodityExp.CommodityID)));
            }

            //Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Can_Return_Commodity_By_Id()
        {
            // Try finding a product by id
            Commodity testProduct = this.MockCommodityRepository.FindById(3);

            Assert.IsNotNull(testProduct); // Test if null
            Assert.IsInstanceOfType(testProduct, typeof(Commodity)); // Test type
            Assert.AreEqual("CSB", testProduct.Name); // Verify it is the right product
        }

      }

}
