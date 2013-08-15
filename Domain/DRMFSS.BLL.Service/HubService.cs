using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public class HubService : IHubService
    {

        private readonly IUnitOfWork _unitOfWork;


        public HubService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region Default Service Implementation
        public bool AddHub(Hub hub)
        {
            _unitOfWork.HubRepository.Add(hub);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteHub(Hub hub)
        {
            if (hub == null) return false;
            _unitOfWork.HubRepository.Delete(hub);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.HubRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.HubRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public bool EditHub(Hub hub)
        {
            _unitOfWork.HubRepository.Edit(hub);
            _unitOfWork.Save();
            return true;
        }

        public Hub FindById(int id)
        {
            return _unitOfWork.HubRepository.FindById(id);
        }

        public List<Hub> GetAllHub()
        {
            return _unitOfWork.HubRepository.GetAll();
        }

        public List<Hub> FindBy(System.Linq.Expressions.Expression<Func<Hub, bool>> predicate)
        {
            return _unitOfWork.HubRepository.FindBy(predicate);
        }
        #endregion

        public List<ViewModels.Common.StoreViewModel> GetAllStoreByUser(UserProfile user)
        {
            if (user == null || user.DefaultHub == null)
            {
                return new List<StoreViewModel>();
            }
            //var stores = (from c in user.DefaultHub.Stores select new ViewModels.Common.StoreViewModel { StoreId = c.StoreID, StoreName = string.Format("{0} - {1} ", c.Name, c.StoreManName) }).OrderBy(c => c.StoreName).ToList();
            var stores = (from c in _unitOfWork.StoreRepository.GetStoreByHub(user.DefaultHub.HubID) select new ViewModels.Common.StoreViewModel { StoreId = c.StoreID, StoreName = string.Format("{0} - {1} ", c.Name, c.StoreManName) }).OrderBy(c => c.StoreName).ToList();
            //stores.Insert(0, new ViewModels.Common.StoreViewModel { StoreName = "Total Hub" });  //I need it for report only so I will modify it on report
            return stores;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
