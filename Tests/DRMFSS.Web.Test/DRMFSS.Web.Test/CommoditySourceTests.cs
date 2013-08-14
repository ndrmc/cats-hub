using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DRMFSS.BLL;
using DRMFSS.BLL.Services;
using DRMFSS.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace DRMFSS.Web.Test
{
    [TestFixture]
    public class CommoditySourceTests
    {
        #region SetUp / TearDown

        private CommoditySourceController _commoditySourceController;
        [SetUp]
        public void Init()
        {
            var commoditySource = new List<CommoditySource>
                {
                    new CommoditySource {CommoditySourceID = 1, Name = "Donation"},
                    new CommoditySource {CommoditySourceID = 2, Name = "Loan"},
                    new CommoditySource {CommoditySourceID = 3, Name = "Local Purchase"},
                };
            var commoditySourceService = new Mock<ICommoditySourceService>();
            commoditySourceService.Setup(t => t.GetAllCommoditySource()).Returns(commoditySource);
            _commoditySourceController = new CommoditySourceController(commoditySourceService.Object);
        }

        [TearDown]
        public void Dispose()
        {
            _commoditySourceController.Dispose();
        }

        #endregion

        #region Tests

        [Test]
        public void CanViewIndex()
        {
            //ACT
            var viewResult = _commoditySourceController.Index();

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.NotNull(model);
            Assert.IsInstanceOf<IEnumerable<CommoditySource>>(model);
            Assert.AreEqual(3, ((IEnumerable<CommoditySource>)model).Count());
        }

        [Test]
        public void CanViewDetails()
        {
            //ACT
            var viewResult = _commoditySourceController.Details(1);

            //ASSERT
            Assert.NotNull(viewResult);
            var model = viewResult.Model;
            Assert.NotNull(model);
            Assert.IsInstanceOf<CommoditySource>(model);
        }

        #endregion
    }
}
