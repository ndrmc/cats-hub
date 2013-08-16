using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DRMFSS.BLL.ViewModels.Dispatch
{
   public class DispatchHomeViewModel
    {
       //TODO:Generated IDispatchAllocationService
       //public DispatchHomeViewModel(IDispatchAllocationService dispatchAllocationService, UserProfile user)
       //{

       //    //ToFDPs = dispatchAllocationService.GetCommitedAllocationsByHubDetached(user.DefaultHub.HubID, user.PreferedWeightMeasurment.ToUpperInvariant(), null, null, null);

       //    //Loans = repository.OtherDispatchAllocation.GetAllToOtherOwnerHubs(user);

       //    //Transfers = repository.OtherDispatchAllocation.GetAllToCurrentOwnerHubs(user);

       //    //AdminUnits = new List<AdminUnit>() { repository.AdminUnit.FindById(1) };

       //    //CommodityTypes = repository.CommodityType.GetAll();

       //    //CommodityTypeID = 1; //food is the default
       //}

       public int CommodityTypeID { get; set; }

       public List<OtherDispatchAllocation> Transfers { get; set; }
       public List<OtherDispatchAllocation> Loans { get; set; }
       public List<DispatchAllocationViewModelDto> ToFDPs { get; set; }
       //public List<AdminUnit> Regions { get; set; }
       public List<AdminUnit> AdminUnits { get; set; }
       public List<CommodityType> CommodityTypes { get; set; }
    }
}
