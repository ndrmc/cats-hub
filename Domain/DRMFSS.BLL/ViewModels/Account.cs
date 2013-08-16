using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL
{
    /// <summary>
    /// 
    /// </summary>
   public partial class Account
    {
        /// <summary>
        /// 
        /// </summary>
       public class Constants
       {
           /// <summary>
           /// 
           /// </summary>
           public const string DONOR = "Donor";
           public const string FDP = "FDP";
           public const string HUBOWNER = "HubOwner";
           public const string HUB = "Hub";
       }

       IUnitOfWork unitOfWork;
       /// <summary>
       /// Gets the name of the entity.
       /// </summary>
       /// <value>
       /// The name of the entity.
       /// </value>
        [NotMapped]
       public string EntityName
       {
           get
           {
               if (unitOfWork == null)
               {
                   unitOfWork = new UnitOfWork();
               }
               if (this.EntityID != 0)
               {
                   switch (this.EntityType)
                   {
                       case Constants.DONOR:
                           return unitOfWork.DonorRepository.FindById(this.EntityID).Name;
                           break;
                       case Constants.FDP:
                           return unitOfWork.FDPRepository.FindById(EntityID).Name;
                           break;
                       case Constants.HUBOWNER:
                           return unitOfWork.HubOwnerRepository.FindById(EntityID).Name;
                           break;
                       case Constants.HUB:
                           return unitOfWork.HubRepository.FindById(EntityID).Name;
                           break;
                   }
               }
               return "Unknown";
           }
       }
    }
}
