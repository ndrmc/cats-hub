

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DRMFSS.BLL.ViewModels.Report;
using System.Linq;

namespace DRMFSS.BLL.Services
{

    public class ReceiveService : IReceiveService
    {
        private readonly IUnitOfWork _unitOfWork;


        public ReceiveService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddReceive(Receive entity)
        {
            _unitOfWork.ReceiveRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditReceive(Receive entity)
        {
            _unitOfWork.ReceiveRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteReceive(Receive entity)
        {
            if (entity == null) return false;
            _unitOfWork.ReceiveRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.ReceiveRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.ReceiveRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Receive> GetAllReceive()
        {
            return _unitOfWork.ReceiveRepository.GetAll();
        }
        public Receive FindById(int id)
        {
            return _unitOfWork.ReceiveRepository.FindById(id);
        }
        public List<Receive> FindBy(Expression<Func<Receive, bool>> predicate)
        {
            return _unitOfWork.ReceiveRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

        /// <summary>
        /// Bies the hub id.
        /// </summary>
        /// <param name="hubId">The hub id.</param>
        /// <returns></returns>
        public List<Receive> ByHubId(int hubId)
        {
            return _unitOfWork.ReceiveRepository.FindBy(r => r.HubID == hubId);
           
        }
        /// <summary>
        /// Return All Ports
        /// </summary>
        /// <returns></returns>
        public List<PortViewModel> GetALlPorts()
        {
            var receives = _unitOfWork.ReceiveRepository.GetAll();
            var ports = (from c in receives select new PortViewModel() { PortName = c.PortName }).Distinct().ToList();

            ports.Insert(0, new PortViewModel { PortName = "All Ports" });
            return ports;
        }

        public Receive FindByGuId(System.Guid id)
        {
            return _unitOfWork.ReceiveRepository.FindBy(t => t.ReceiveID == id).FirstOrDefault();

        }

    }
}

 
      
