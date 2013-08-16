using System.Collections.Generic;
using System.Linq;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.MetaModels;
using System.ComponentModel.DataAnnotations;
using DRMFSS.Web.Models;
using System;


namespace DRMFSS.BLL.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ReceiveDetailRepository :GenericRepository<CTSContext,ReceiveDetail>, IReceiveDetailRepository
    {
        public ReceiveDetailRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        } 
        /// <summary>
        /// Gets the by receive id.
        /// </summary>
        /// <param name="receiveId">The receive id.</param>
        /// <returns></returns>
        public List<ReceiveDetail> GetByReceiveId(Guid receiveId)
        {
            return db.ReceiveDetails.Where(p => p.ReceiveID == receiveId).ToList();
        }


        public List<ReceiveDetailViewModelDto> ByReceiveIDetached(Guid? receiveId,string weightMeasurmentCode)
        {
            List<ReceiveDetailViewModelDto> receiveDetails = new List<ReceiveDetailViewModelDto>();

            var query = (from rD in db.ReceiveDetails 
                         where rD.ReceiveID == receiveId
                         select new ReceiveDetailViewModelDto()
                                    {
                                        ReceiveID = rD.ReceiveID,
                                        ReceiveDetailID = rD.ReceiveDetailID,
                                        CommodityName = rD.Commodity.Name,
                                        UnitName = rD.Unit.Name,
                                        ReceivedQuantityInMT = rD.TransactionGroup.Transactions.FirstOrDefault().QuantityInMT,
                                        ReceivedQuantityInUnit = rD.TransactionGroup.Transactions.FirstOrDefault().QuantityInUnit,
                                        SentQuantityInMT = Math.Abs(rD.SentQuantityInMT),
                                        SentQuantityInUnit = rD.SentQuantityInUnit
                                        
                                    });
            foreach (var receiveDetailViewModelDto in query)
            {

                if(weightMeasurmentCode == "qn")
                {
                    receiveDetailViewModelDto.ReceivedQuantityInMT = Math.Abs(receiveDetailViewModelDto.ReceivedQuantityInMT)*10;
                    receiveDetailViewModelDto.SentQuantityInMT = Math.Abs(receiveDetailViewModelDto.SentQuantityInMT)*10;
                    
                }else
                {
                    receiveDetailViewModelDto.ReceivedQuantityInMT = Math.Abs(receiveDetailViewModelDto.ReceivedQuantityInMT);
                    receiveDetailViewModelDto.SentQuantityInMT = Math.Abs(receiveDetailViewModelDto.SentQuantityInMT);
                }
                receiveDetailViewModelDto.ReceivedQuantityInUnit =
                    Math.Abs(receiveDetailViewModelDto.ReceivedQuantityInUnit);
                receiveDetails.Add(receiveDetailViewModelDto);
            }
            return receiveDetails;
        }

        public bool DeleteByID(int id)
        {
            var original = FindById(id);
            if (original == null) return false;
            db.ReceiveDetails.Remove(original);

            return true;
        }

        public bool DeleteByID(System.Guid id)
        {
            return false;
        }

        public ReceiveDetail FindById(int id)
        {
            return null;
        }

        public ReceiveDetail FindById(System.Guid id)
        {
            return db.ReceiveDetails.FirstOrDefault(t => t.ReceiveDetailID == id);

        }
    }
}
