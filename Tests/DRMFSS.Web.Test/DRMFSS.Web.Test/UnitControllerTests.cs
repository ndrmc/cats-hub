using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DRMFSS.BLL;
using DRMFSS.BLL.Services;
using NUnit.Framework;
using DRMFSS.Web.Controllers;
using Moq;

namespace DRMFSS.Web.Test
{
    [TestFixture]
    public class UnitControllerTests
    {
        #region SetUp / TearDown

        private UnitController _unitController;
        [SetUp]
        public void Init()
        {
            var units = new List<Unit>()
                {
                    new Unit() {Name = "Kg", UnitID = 1},
                    new Unit() {Name = "Ml", UnitID = 2},
                };
            var unitService = new Mock<IUnitService>();
            unitService.Setup(t => t.GetAllUnit()).Returns(units);
            _unitController = new UnitController(unitService.Object);

        }

        [TearDown]
        public void Dispose()
        {
            _unitController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanViewIndex()
        {
            //ACT
            var result = _unitController.Index();
            var model = ((ViewResult)result).Model;
            //Assert

            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsInstanceOf<IEnumerable<Unit>>(model);
            Assert.AreEqual(2, ((IEnumerable<Unit>)model).Count());
        }

        #endregion
    }
}
