using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;
using DRMFSS.BLL.ViewModels;
using DRMFSS.BLL.ViewModels.Dispatch;

namespace DRMFSS.BLL.Repository
{
    public partial class OtherDispatchAllocationRepository :GenericRepository<CTSContext,OtherDispatchAllocation>, IOtherDispatchAllocationRepository
    {
        public OtherDispatchAllocationRepository(CTSContext _db, IUnitOfWork uow)
        {
            db = _db;
            repository = uow;
        } 
        public void Save(ViewModels.Dispatch.OtherDispatchAllocationViewModel model)
        {
            OtherDispatchAllocation oAllocation = new OtherDispatchAllocation();
            if (model.OtherDispatchAllocationID != null)
            {
                oAllocation = repository.OtherDispatchAllocation.FindById(model.OtherDispatchAllocationID.Value);

                oAllocation.OtherDispatchAllocationID = model.OtherDispatchAllocationID.Value;
                oAllocation.ProgramID = model.ProgramID.Value;
                oAllocation.HubID = model.FromHubID.Value;
                oAllocation.ToHubID = model.ToHubID.Value;
                oAllocation.ReasonID = model.ReasonID;
                oAllocation.ReferenceNumber = model.ReferenceNumber;
                oAllocation.AgreementDate = model.AgreementDate;
                oAllocation.CommodityID = model.CommodityID;
                oAllocation.EstimatedDispatchDate = model.EstimatedDispatchDate;
                oAllocation.IsClosed = model.IsClosed;
                oAllocation.ProjectCodeID = repository.ProjectCode.GetProjectCodeId(model.ProjectCode);
                oAllocation.ShippingInstructionID =
                    repository.ShippingInstruction.GetShipingInstructionId(model.ShippingInstruction);
                oAllocation.UnitID = model.UnitID;
                oAllocation.QuantityInUnit = model.QuantityInUnit;
                oAllocation.QuantityInMT = model.QuantityInMT;
                oAllocation.QuantityInUnit = model.QuantityInUnit;
                oAllocation.Remark = model.Remark;
                //Modify Banty :From SaveChanges(oAllocation) to SaveChanges()
             repository.OtherDispatchAllocation.SaveChanges(oAllocation);

            }
            else
            {
                oAllocation.PartitionID = (model.PartitionID.HasValue) ? model.PartitionID.Value : 0;
                if (model.OtherDispatchAllocationID.HasValue)
                {
                    oAllocation.OtherDispatchAllocationID = model.OtherDispatchAllocationID.Value;
                }
                oAllocation.ProgramID = model.ProgramID.Value;
                oAllocation.HubID = model.FromHubID.Value;
                oAllocation.ToHubID = model.ToHubID.Value;
                oAllocation.ReasonID = model.ReasonID;
                oAllocation.ReferenceNumber = model.ReferenceNumber;
                oAllocation.AgreementDate = model.AgreementDate;
                oAllocation.CommodityID = model.CommodityID;
                oAllocation.EstimatedDispatchDate = model.EstimatedDispatchDate;
                oAllocation.IsClosed = model.IsClosed;
                oAllocation.ProjectCodeID = repository.ProjectCode.GetProjectCodeId(model.ProjectCode);
                oAllocation.ShippingInstructionID =
                    repository.ShippingInstruction.GetShipingInstructionId(model.ShippingInstruction);
                oAllocation.UnitID = model.UnitID;
                oAllocation.QuantityInUnit = model.QuantityInUnit;
                oAllocation.QuantityInMT = model.QuantityInMT;
                oAllocation.QuantityInUnit = model.QuantityInUnit;
                oAllocation.Remark = model.Remark;
                repository.OtherDispatchAllocation.Add(oAllocation);
            }

        }


        public ViewModels.Dispatch.OtherDispatchAllocationViewModel GetViewModelByID(Guid otherDispatchAllocationId)
        {
            var model = (from v in db.OtherDispatchAllocations
                         where v.OtherDispatchAllocationID == otherDispatchAllocationId
                         select v).FirstOrDefault();
            if (model != null)
            {
                var val = new OtherDispatchAllocationViewModel()
                              {
                                  PartitionID = 0,
                                  OtherDispatchAllocationID = model.OtherDispatchAllocationID,
                                  ProgramID = model.ProgramID,
                                  FromHubID = model.HubID,
                                  ToHubID = model.ToHubID,
                                  ReasonID = model.ReasonID,
                                  ReferenceNumber = model.ReferenceNumber,
                                  AgreementDate = model.AgreementDate,
                                  CommodityID = model.CommodityID,
                                  EstimatedDispatchDate = model.EstimatedDispatchDate,
                                  IsClosed = model.IsClosed,
                                  ProjectCode = model.ProjectCode.Value,
                                  ShippingInstruction = model.ShippingInstruction.Value,
                                  UnitID = model.UnitID,
                                  QuantityInUnit = model.QuantityInUnit,
                                  QuantityInMT = model.QuantityInMT,
                                  CommodityTypeID = model.Commodity.CommodityTypeID,
                                  Remark = model.Remark,
                              };
                return val;
            }
            return null;
        }


        public List<OtherDispatchAllocation> GetAllToCurrentOwnerHubs(UserProfile user)
        {
            return (from v in db.OtherDispatchAllocations
                    where v.HubID == user.DefaultHub.HubID && v.Hub1.HubOwnerID == user.DefaultHub.HubOwnerID
                    select v
                   ).OrderByDescending(o => o.AgreementDate).ToList();
        }

        public List<OtherDispatchAllocation> GetAllToOtherOwnerHubs(UserProfile user)
        {
            return (from v in db.OtherDispatchAllocations
                    where v.HubID == user.DefaultHub.HubID && v.Hub1.HubOwnerID != user.DefaultHub.HubOwnerID
                    select v
                   ).OrderByDescending(o => o.AgreementDate).ToList();
        }


        public List<OtherDispatchAllocationDto> GetCommitedLoanAllocationsDetached(UserProfile user, bool? closedToo, int? CommodityType)
        {

            List<OtherDispatchAllocationDto> LoanList = new List<OtherDispatchAllocationDto>();

            var Loans = (from v in db.OtherDispatchAllocations
                         where v.HubID == user.DefaultHub.HubID && v.Hub1.HubOwnerID != user.DefaultHub.HubOwnerID
                         select v
                        );


            if (closedToo == null || closedToo == false)
            {
                Loans = Loans.Where(p => p.IsClosed == false);
            }
            else
            {
                Loans = Loans.Where(p => p.IsClosed == true);
            }

            if (CommodityType.HasValue)
            {
                Loans = Loans.Where(p => p.Commodity.CommodityTypeID == CommodityType.Value);
            }
            else
            {
                Loans = Loans.Where(p => p.Commodity.CommodityTypeID == 1);//by default
            }

            foreach (var otherDispatchAllocation in Loans)
            {
                var loan = new OtherDispatchAllocationDto();

                loan.OtherDispatchAllocationID = otherDispatchAllocation.OtherDispatchAllocationID;
                loan.ReferenceNumber = otherDispatchAllocation.ReferenceNumber;
                loan.CommodityName = otherDispatchAllocation.Commodity.Name;
                loan.EstimatedDispatchDate = otherDispatchAllocation.EstimatedDispatchDate;
                loan.AgreementDate = otherDispatchAllocation.AgreementDate;
                loan.SINumber = otherDispatchAllocation.ShippingInstruction.Value;
                loan.ProjectCode = otherDispatchAllocation.ProjectCode.Value;
                loan.IsClosed = otherDispatchAllocation.IsClosed;
                
                loan.QuantityInUnit = otherDispatchAllocation.QuantityInUnit;
                loan.RemainingQuantityInUnit = otherDispatchAllocation.QuantityInUnit;

                if (user.PreferedWeightMeasurment.ToUpperInvariant() == "QN")
                {
                    loan.QuantityInMT = Math.Abs(otherDispatchAllocation.QuantityInMT)*10;
                    loan.RemainingQuantityInMt = (otherDispatchAllocation.RemainingQuantityInMt)*10;
                }
                else
                {
                    loan.QuantityInMT = Math.Abs(otherDispatchAllocation.QuantityInMT);
                    loan.RemainingQuantityInMt = (otherDispatchAllocation.RemainingQuantityInMt);
                }
                LoanList.Add(loan);
            }
            return LoanList;
        }


        public List<OtherDispatchAllocationDto> GetCommitedTransferAllocationsDetached(UserProfile user, bool? closedToo, int? CommodityType)
        {
            List<OtherDispatchAllocationDto> TransferList = new List<OtherDispatchAllocationDto>();

            var Transafers = (from v in db.OtherDispatchAllocations
                              where v.HubID == user.DefaultHub.HubID && v.Hub1.HubOwnerID == user.DefaultHub.HubOwnerID
                              select v
                        );


            if (closedToo == null || closedToo == false)
            {
                Transafers = Transafers.Where(p => p.IsClosed == false);
            }
            else
            {
                Transafers = Transafers.Where(p => p.IsClosed == true);
            }

            if (CommodityType.HasValue)
            {
                Transafers = Transafers.Where(p => p.Commodity.CommodityTypeID == CommodityType.Value);
            }
            else
            {
                Transafers = Transafers.Where(p => p.Commodity.CommodityTypeID == 1);//by default
            }

            foreach (var otherDispatchAllocation in Transafers)
            {
                var transfer = new OtherDispatchAllocationDto();

                transfer.OtherDispatchAllocationID = otherDispatchAllocation.OtherDispatchAllocationID;
                transfer.ReferenceNumber = otherDispatchAllocation.ReferenceNumber;
                transfer.CommodityName = otherDispatchAllocation.Commodity.Name;
                transfer.EstimatedDispatchDate = otherDispatchAllocation.EstimatedDispatchDate;
                transfer.AgreementDate = otherDispatchAllocation.AgreementDate;
                transfer.SINumber = otherDispatchAllocation.ShippingInstruction.Value;
                transfer.ProjectCode = otherDispatchAllocation.ProjectCode.Value;
                transfer.IsClosed = otherDispatchAllocation.IsClosed;
                
                transfer.QuantityInUnit = otherDispatchAllocation.QuantityInUnit;
                transfer.RemainingQuantityInUnit = otherDispatchAllocation.RemainingQuantityInUnit;

                if (user.PreferedWeightMeasurment.ToUpperInvariant() == "QN")
                {
                    transfer.QuantityInMT = Math.Abs(otherDispatchAllocation.QuantityInMT) * 10;
                    transfer.RemainingQuantityInMt = (otherDispatchAllocation.RemainingQuantityInMt) * 10;
                }
                else
                {
                    transfer.QuantityInMT = Math.Abs(otherDispatchAllocation.QuantityInMT);
                    transfer.RemainingQuantityInMt = (otherDispatchAllocation.RemainingQuantityInMt);
                }
                TransferList.Add(transfer);
            }
            return TransferList;
        }

        public void CloseById(Guid id)
        {
            var delAllocation = db.OtherDispatchAllocations.FirstOrDefault(allocation => allocation.OtherDispatchAllocationID == id);
            if (delAllocation != null) delAllocation.IsClosed = true;
            db.SaveChanges();
        }



        public bool DeleteByID(int id)
        {
            return false;
        }

        public bool DeleteByID(Guid id)
        {
            var original = FindById(id);
            if(original==null) return false;
            db.OtherDispatchAllocations.Remove(original);
            return true;
        }

        public OtherDispatchAllocation FindById(int id)
        {
            return null;
        }

        public OtherDispatchAllocation FindById(Guid id)
        {
            return db.OtherDispatchAllocations.FirstOrDefault(t => t.OtherDispatchAllocationID == id);
        }
    }
}