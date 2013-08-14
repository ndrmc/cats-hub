using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL;
using DRMFSS.BLL.Services;
using Ninject;

namespace DRMFSS.Web.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver()
        {
            this.kernel = new StandardKernel();
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IFDPService>().To<FDPService>();
            kernel.Bind<IAdminUnitService>().To<AdminUnitService>();
            kernel.Bind<ICommodityService>().To<CommodityService>();
            kernel.Bind<ITransporterService>().To<TransporterService>();
            kernel.Bind<IShippingInstructionService>().To<ShippingInstructionService>();
            kernel.Bind<ITransactionService>().To<TransactionService>();
            kernel.Bind<IUnitService>().To<UnitService>();
            kernel.Bind<IUserProfileService>().To<UserProfileService>();
            kernel.Bind<IUserRoleService>().To<UserRoleService>();
            kernel.Bind<IUserHubService>().To<UserHubService>();
            kernel.Bind<ICommodityTypeService>().To<CommodityTypeService>();
            kernel.Bind<ICommodityGradeService>().To<CommodityGradeService>();
        }
    }
}