using System.Linq;
using DRMFSS.BLL;
using DRMFSS.BLL.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Moq;
using System.Data.Objects.DataClasses;

namespace DRMFSS.Test
{


    [TestClass()]
    public class CommodityTest
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

        public CommodityTest()
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

            this.MockCommodityRepository = mockCommodityRepository.Object;
        }

        /// <summary>
        /// Our Mock AdminUNit Repository for use in testing
        /// </summary>
        public readonly ICommodityRepository MockCommodityRepository;

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
        public void GetCommodityByNameTest()
        {
            string name = "Green Wheat";
            Commodity actual = this.MockCommodityRepository.GetCommodityByName(name); // TODO: Initialize to an appropriate value
            Commodity expected = new Commodity { CommodityID = 6, Name = "Green Wheat", LongName = "", CommodityTypeID = 1, ParentID = 1 };
            
            Assert.IsInstanceOfType(actual,typeof(Commodity));
            Assert.AreEqual(actual.GetType(), expected.GetType());
            Assert.AreEqual(actual.CommodityID, expected.CommodityID);
            Assert.AreEqual(actual.Name, expected.Name);
            Assert.AreEqual(actual.CommodityTypeID, expected.CommodityTypeID);
            Assert.AreEqual(actual.ParentID, expected.ParentID);
        
        }

        [TestMethod()]
        public void GetAllParentsTest()
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
        public void GetAllSubCommoditiesTest()
        {
            //Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            List<Commodity> expected = new List<Commodity>
                {
                    new Commodity { CommodityID = 5, Name = "Yellow Wheat",LongName = "",CommodityTypeID = 1, ParentID = 1 },
                    new Commodity { CommodityID = 6, Name = "Green Wheat",LongName = "",CommodityTypeID = 1, ParentID = 1 },
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
        public void GetAllSubCommoditiesByParantIdTest()
        {
            //Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            List<Commodity> expected = new List<Commodity>
                {
                    new Commodity { CommodityID = 5, Name = "Yellow Wheat",LongName = "",CommodityTypeID = 1, ParentID = 1 },
                    new Commodity { CommodityID = 6, Name = "Green Wheat",LongName = "",CommodityTypeID = 1, ParentID = 1 },
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

        /// <summary>
        ///A test for Transactions1
        ///</summary>
        [TestMethod()]
        public void Transactions1Test()
        {
            Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            EntityCollection<Transaction> expected = null; // TODO: Initialize to an appropriate value
            EntityCollection<Transaction> actual;
            target.Transactions1 = expected;
            actual = target.Transactions1;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Transactions
        ///</summary>
        [TestMethod()]
        public void TransactionsTest()
        {
            Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            EntityCollection<Transaction> expected = null; // TODO: Initialize to an appropriate value
            EntityCollection<Transaction> actual;
            target.Transactions = expected;
            actual = target.Transactions;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ReceiveDetails
        ///</summary>
        [TestMethod()]
        public void ReceiveDetailsTest()
        {
            Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            EntityCollection<ReceiveDetail> expected = null; // TODO: Initialize to an appropriate value
            EntityCollection<ReceiveDetail> actual;
            target.ReceiveDetails = expected;
            actual = target.ReceiveDetails;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ReceiptAllocations
        ///</summary>
        [TestMethod()]
        public void ReceiptAllocationsTest()
        {
            Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            EntityCollection<ReceiptAllocation> expected = null; // TODO: Initialize to an appropriate value
            EntityCollection<ReceiptAllocation> actual;
            target.ReceiptAllocations = expected;
            actual = target.ReceiptAllocations;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ParentID
        ///</summary>
        [TestMethod()]
        public void ParentIDTest()
        {
            Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            Nullable<int> expected = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> actual;
            target.ParentID = expected;
            actual = target.ParentID;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Name
        ///</summary>
        [TestMethod()]
        public void NameTest()
        {
            Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LongName
        ///</summary>
        [TestMethod()]
        public void LongNameTest()
        {
            Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.LongName = expected;
            actual = target.LongName;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GiftCertificateDetails
        ///</summary>
        [TestMethod()]
        public void GiftCertificateDetailsTest()
        {
            Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            EntityCollection<GiftCertificateDetail> expected = null; // TODO: Initialize to an appropriate value
            EntityCollection<GiftCertificateDetail> actual;
            target.GiftCertificateDetails = expected;
            actual = target.GiftCertificateDetails;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DispatchDetails
        ///</summary>
        [TestMethod()]
        public void DispatchDetailsTest()
        {
            Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            EntityCollection<DispatchDetail> expected = null; // TODO: Initialize to an appropriate value
            EntityCollection<DispatchDetail> actual;
            target.DispatchDetails = expected;
            actual = target.DispatchDetails;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DispatchAllocations
        ///</summary>
        [TestMethod()]
        public void DispatchAllocationsTest()
        {
            Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            EntityCollection<DispatchAllocation> expected = null; // TODO: Initialize to an appropriate value
            EntityCollection<DispatchAllocation> actual;
            target.DispatchAllocations = expected;
            actual = target.DispatchAllocations;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CommodityTypeReference
        ///</summary>
        [TestMethod()]
        public void CommodityTypeReferenceTest()
        {
            Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            EntityReference<CommodityType> expected = null; // TODO: Initialize to an appropriate value
            EntityReference<CommodityType> actual;
            target.CommodityTypeReference = expected;
            actual = target.CommodityTypeReference;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CommodityTypeID
        ///</summary>
        [TestMethod()]
        public void CommodityTypeIDTest()
        {
            Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.CommodityTypeID = expected;
            actual = target.CommodityTypeID;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CommodityType
        ///</summary>
        [TestMethod()]
        public void CommodityTypeTest()
        {
            Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            CommodityType expected = null; // TODO: Initialize to an appropriate value
            CommodityType actual;
            target.CommodityType = expected;
            actual = target.CommodityType;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CommodityID
        ///</summary>
        [TestMethod()]
        public void CommodityIDTest()
        {
            Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.CommodityID = expected;
            actual = target.CommodityID;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Commodity2Reference
        ///</summary>
        [TestMethod()]
        public void Commodity2ReferenceTest()
        {
            Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            EntityReference<Commodity> expected = null; // TODO: Initialize to an appropriate value
            EntityReference<Commodity> actual;
            target.Commodity2Reference = expected;
            actual = target.Commodity2Reference;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Commodity2
        ///</summary>
        [TestMethod()]
        public void Commodity2Test()
        {
            Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            Commodity expected = null; // TODO: Initialize to an appropriate value
            Commodity actual;
            target.Commodity2 = expected;
            actual = target.Commodity2;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Commodity1
        ///</summary>
        [TestMethod()]
        public void Commodity1Test()
        {
            Commodity target = new Commodity(); // TODO: Initialize to an appropriate value
            EntityCollection<Commodity> expected = null; // TODO: Initialize to an appropriate value
            EntityCollection<Commodity> actual;
            target.Commodity1 = expected;
            actual = target.Commodity1;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateCommodity
        ///</summary>
        [TestMethod()]
        public void CreateCommodityTest()
        {
            int commodityID = 0; // TODO: Initialize to an appropriate value
            string name = string.Empty; // TODO: Initialize to an appropriate value
            int commodityTypeID = 0; // TODO: Initialize to an appropriate value
            Commodity expected = null; // TODO: Initialize to an appropriate value
            Commodity actual;
            actual = Commodity.CreateCommodity(commodityID, name, commodityTypeID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Commodity Constructor
        ///</summary>
        [TestMethod()]
        public void CommodityConstructorTest()
        {
            Commodity target = new Commodity();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
