using System.Linq;
using DRMFSS.BLL;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Moq;
using System.Data.Objects.DataClasses;

namespace DRMFSS.Test
{


    [TestClass()]
    public class AdminUnitTest
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

            
         public AdminUnitTest()
         {
             // create some mock products to play with
             List<AdminUnit> testAdminUnits = new List<AdminUnit>
                {
                    new AdminUnit { AdminUnitID = 2, Name = "Tigray",AdminUnitTypeID = 2, ParentID = 1 },
                    new AdminUnit { AdminUnitID = 3, Name = "Amhara",AdminUnitTypeID = 2, ParentID = 1 },
                    new AdminUnit { AdminUnitID = 7, Name = "Semen Shewa",AdminUnitTypeID = 3, ParentID = 3 },

                };

             // Mock the AdminUNit Repository using Moq
             Mock<IAdminUnitRepository> mockAdminUnitRepository = new Mock<IAdminUnitRepository>();

             // Return all the AdminUNits
             mockAdminUnitRepository.Setup(mr => mr.GetAll()).Returns(testAdminUnits);

             // return the region of the specified zone
             mockAdminUnitRepository.Setup(mr=>mr.GetRegionByZoneId(
                  It.IsAny<int>())).Returns((int i) => testAdminUnits.Where(x => x.AdminUnitID == i).Single().ParentID.Value);

             //return Admin units whose type is of region
             mockAdminUnitRepository.Setup(mr=>mr.GetRegions()).Returns(
                 testAdminUnits.Where(x => x.AdminUnitTypeID == 2).ToList());

             //retrun childrens of Admin unit gien a ceratin id
             mockAdminUnitRepository.Setup(mr=>mr.GetChildren(
                 It.IsAny<int>())).Returns((int i) => testAdminUnits.Where(x => x.ParentID == i).ToList());


            // return a AdminUnit by Id
             mockAdminUnitRepository.Setup(mr => mr.FindById(
                It.IsAny<int>())).Returns((int i) => testAdminUnits.Where(x => x.AdminUnitID == i).Single());

            //TODO mockAdminUnitRepository.Setup(mr=>mr.GetByUnitType )


            // Allows us to test saving a AdminUNit
            mockAdminUnitRepository.Setup(mr => mr.SaveChanges(It.IsAny<AdminUnit>())).Returns(
                (AdminUnit target) =>
                {
                    if (target.AdminUnitID.Equals(default(int)))
                    {
                        target.AdminUnitID = testAdminUnits.Count() + 1;
                        testAdminUnits.Add(target);
                    }
                    else
                    {
                        var original = testAdminUnits.Where(q => q.AdminUnitID == target.AdminUnitID).Single();

                        if (original == null)
                        {
                            return false;
                        }

                        original.Name = target.Name;
                    }

                    return true;
                });

            this.MockAdminUnitsRepository = mockAdminUnitRepository.Object;
         }

         /// <summary>
         /// Our Mock AdminUNit Repository for use in testing
         /// </summary>
         public readonly IAdminUnitRepository MockAdminUnitsRepository;

        [TestMethod()]
        public void AdminUnitConstructorTest()
        {
            AdminUnit target = new AdminUnit();
        }

        [TestMethod()]
        public void GetRegionsTest()
        {
            int AdminUnitType = 2;
            List<AdminUnit> testProducts = this.MockAdminUnitsRepository.GetRegions();
            foreach (var testProduct in testProducts)
            {
                Assert.AreEqual(AdminUnitType,testProduct.AdminUnitTypeID);
            }
            
        }

        [TestMethod()]
        public void GetRegionByZoneIdTest()
        {
            int zoneId = 7;//semen shewa

            int actual = this.MockAdminUnitsRepository.GetRegionByZoneId(zoneId);
            int expected = 3; //Amhara

           Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetChildrenTest()
        {
            int ParentUnitID = 3;

            List<AdminUnit> expected = new List<AdminUnit>
                                           {
                              new AdminUnit {AdminUnitID = 7, Name = "Semen Shewa", AdminUnitTypeID = 3, ParentID = 3},                 
                                           }; 
            List<AdminUnit> actual = this.MockAdminUnitsRepository.GetChildren(ParentUnitID);

            Assert.IsNotNull(actual); // Test if null
            Assert.IsInstanceOfType(actual, typeof(List<AdminUnit>)); // Test type
            Assert.AreEqual(actual.First().GetType(),expected.First().GetType());
            Assert.AreEqual(actual.First().AdminUnitID,expected.First().AdminUnitID);
            Assert.AreEqual(actual.First().Name, expected.First().Name);
            Assert.AreEqual(actual.First().AdminUnitTypeID, expected.First().AdminUnitTypeID);
            Assert.AreEqual(actual.First().ParentID, expected.First().ParentID);
            Assert.AreEqual(actual.Count, expected.Count);
            Assert.AreEqual(ParentUnitID, expected.First().ParentID);

        }

        [TestMethod()]
        public void GetByUnitTypeTest()
        {
            //int typeId = 0; // TODO: Initialize to an appropriate value
            //List<AdminUnit> expected = null; // TODO: Initialize to an appropriate value
            //List<AdminUnit> actual;
            //actual = AdminUnit.GetByUnitType(typeId);
            //Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetAllAdminUnitsTest()
        {
            // Try finding all AdminUnits
            List<AdminUnit> testAdminUnits = this.MockAdminUnitsRepository.GetAll();

            Assert.IsNotNull(testAdminUnits); // Test if null
            Assert.AreEqual(3, testAdminUnits.Count); // Verify the correct Number
        }

        [TestMethod()]
        public void GetAdminUnitTypesTest()
        {
            //AdminUnit target = new AdminUnit(); // TODO: Initialize to an appropriate value
            //List<AdminUnitType> expected = null; // TODO: Initialize to an appropriate value
            //List<AdminUnitType> actual;
            //actual = target.GetAdminUnitTypes();
            //Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetAdminUnitTypeTest()
        {
            //AdminUnit target = new AdminUnit(); // TODO: Initialize to an appropriate value
            //int id = 0; // TODO: Initialize to an appropriate value
            //AdminUnitType expected = null; // TODO: Initialize to an appropriate value
            //AdminUnitType actual;
            //actual = target.GetAdminUnitType(id);
            //Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FindAdminUnitByIdTest()
        {

            string AdminUnitName = "Tigray";
            // Try finding a product by id
            AdminUnit actual = this.MockAdminUnitsRepository.FindById(2);

            Assert.IsNotNull(actual); // Test if null
            Assert.IsInstanceOfType(actual, typeof(AdminUnit)); // Test type
            Assert.AreEqual(AdminUnitName, actual.Name); // Verify it is the right product

        }

        [TestMethod()]
        public void EditTest()
        {
            //AdminUnit target = new AdminUnit(); // TODO: Initialize to an appropriate value
            //AdminUnit entity = null; // TODO: Initialize to an appropriate value
            //target.SaveChanges(entity);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            //AdminUnit target = new AdminUnit(); // TODO: Initialize to an appropriate value
            //AdminUnit entity = null; // TODO: Initialize to an appropriate value
            //target.Delete(entity);
        }

        [TestMethod()]
        public void CreateAdminUnitTest()
        {
            //int adminUnitID = 0; // TODO: Initialize to an appropriate value
            //AdminUnit expected = null; // TODO: Initialize to an appropriate value
            //AdminUnit actual;
            //actual = AdminUnit.CreateAdminUnit(adminUnitID);
            //Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void AddTest()
        {
            //AdminUnit target = new AdminUnit(); // TODO: Initialize to an appropriate value
            //AdminUnit entity = null; // TODO: Initialize to an appropriate value
            //target.Add(entity);
        }

        /// <summary>
        ///A test for ParentID
        ///</summary>
        [TestMethod()]
        public void ParentIDTest()
        {
            AdminUnit target = new AdminUnit(); // TODO: Initialize to an appropriate value
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
            AdminUnit target = new AdminUnit(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Name = expected;
            actual = target.Name;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FDPs
        ///</summary>
        [TestMethod()]
        public void FDPsTest()
        {
            AdminUnit target = new AdminUnit(); // TODO: Initialize to an appropriate value
            EntityCollection<FDP> expected = null; // TODO: Initialize to an appropriate value
            EntityCollection<FDP> actual;
            target.FDPs = expected;
            actual = target.FDPs;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AdminUnitTypeReference
        ///</summary>
        [TestMethod()]
        public void AdminUnitTypeReferenceTest()
        {
            AdminUnit target = new AdminUnit(); // TODO: Initialize to an appropriate value
            EntityReference<AdminUnitType> expected = null; // TODO: Initialize to an appropriate value
            EntityReference<AdminUnitType> actual;
            target.AdminUnitTypeReference = expected;
            actual = target.AdminUnitTypeReference;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AdminUnitTypeID
        ///</summary>
        [TestMethod()]
        public void AdminUnitTypeIDTest()
        {
            AdminUnit target = new AdminUnit(); // TODO: Initialize to an appropriate value
            Nullable<int> expected = new Nullable<int>(); // TODO: Initialize to an appropriate value
            Nullable<int> actual;
            target.AdminUnitTypeID = expected;
            actual = target.AdminUnitTypeID;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AdminUnitType
        ///</summary>
        [TestMethod()]
        public void AdminUnitTypeTest()
        {
            AdminUnit target = new AdminUnit(); // TODO: Initialize to an appropriate value
            AdminUnitType expected = null; // TODO: Initialize to an appropriate value
            AdminUnitType actual;
            target.AdminUnitType = expected;
            actual = target.AdminUnitType;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AdminUnitID
        ///</summary>
        [TestMethod()]
        public void AdminUnitIDTest()
        {
            AdminUnit target = new AdminUnit(); // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            target.AdminUnitID = expected;
            actual = target.AdminUnitID;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AdminUnit2Reference
        ///</summary>
        [TestMethod()]
        public void AdminUnit2ReferenceTest()
        {
            AdminUnit target = new AdminUnit(); // TODO: Initialize to an appropriate value
            EntityReference<AdminUnit> expected = null; // TODO: Initialize to an appropriate value
            EntityReference<AdminUnit> actual;
            target.AdminUnit2Reference = expected;
            actual = target.AdminUnit2Reference;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AdminUnit2
        ///</summary>
        [TestMethod()]
        public void AdminUnit2Test()
        {
            AdminUnit target = new AdminUnit(); // TODO: Initialize to an appropriate value
            AdminUnit expected = null; // TODO: Initialize to an appropriate value
            AdminUnit actual;
            target.AdminUnit2 = expected;
            actual = target.AdminUnit2;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AdminUnit1
        ///</summary>
        [TestMethod()]
        public void AdminUnit1Test()
        {
            AdminUnit target = new AdminUnit(); // TODO: Initialize to an appropriate value
            EntityCollection<AdminUnit> expected = null; // TODO: Initialize to an appropriate value
            EntityCollection<AdminUnit> actual;
            target.AdminUnit1 = expected;
            actual = target.AdminUnit1;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CreateAdminUnit
        ///</summary>
        [TestMethod()]
        public void CreateAdminUnitTest1()
        {
            int adminUnitID = 0; // TODO: Initialize to an appropriate value
            AdminUnit expected = null; // TODO: Initialize to an appropriate value
            AdminUnit actual;
            actual = AdminUnit.CreateAdminUnit(adminUnitID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AdminUnit Constructor
        ///</summary>
        [TestMethod()]
        public void AdminUnitConstructorTest1()
        {
            AdminUnit target = new AdminUnit();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
