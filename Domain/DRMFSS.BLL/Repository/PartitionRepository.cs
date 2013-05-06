using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DRMFSS.BLL.Interfaces;
//using Replication.Sync.Common;
using System.Configuration;

namespace DRMFSS.BLL.Repository
{
    public partial class PartitionRepository : IPartitionRepository
    {
       public List<ViewModels.ReplicationViewModel> GetHubsSyncrtonizationDetails(int publication)
        {
            List<ViewModels.ReplicationViewModel> replications = new List<ViewModels.ReplicationViewModel>();
            //SubscribersMonitorHelper helper;

            //if (publication == 1)
            //{
            //    helper  = new SubscribersMonitorHelper { publicationDbName = ConfigurationManager.AppSettings["publicationDbName"], publicationName = ConfigurationManager.AppSettings["publicationNameOne"], publisherName = ConfigurationManager.AppSettings["publisherName"] };

            //}
            //else
            //{
            //    helper = new SubscribersMonitorHelper { publicationDbName = ConfigurationManager.AppSettings["publicationDbName"], publicationName = ConfigurationManager.AppSettings["publicationNameTwo"], publisherName = ConfigurationManager.AppSettings["publisherName"] };
            //}

            //foreach (SubscriberDetail subscriberDetail in helper.GetSubscribersDetail(helper))
            //{
            //    ViewModels.ReplicationViewModel replicationViewModel = new ViewModels.ReplicationViewModel();
            //    var hub = (from c in db.Hubs where c.Name == subscriberDetail.SubscriberName select c).FirstOrDefault();
            //    if (hub != null)
            //    {
            //        var partiton = (from c in db.Partitions where c.HubID == hub.HubID select c).FirstOrDefault();
            //        replicationViewModel.PartitionId = partiton.PartitionID;
            //        replicationViewModel.HubName = hub.Name;
            //        replicationViewModel.PartitionCreatedDate = partiton.PartitionCreatedDate;
            //        replicationViewModel.LastUpdated = partiton.LastUpdated.Value;
            //        replicationViewModel.LastSyncTime = subscriberDetail.LastSyncTime;
            //        if (int.Parse(subscriberDetail.Conflict) > 0)
            //            replicationViewModel.HasConflict = true;
            //        else
            //            replicationViewModel.HasConflict = false;
            //        replicationViewModel.IsActive = partiton.IsActive;
            //        replicationViewModel.LastAction = subscriberDetail.LastAction;
            //        replicationViewModel.ActionTime = subscriberDetail.ActionTime;
            //        replications.Add(replicationViewModel);
            //    }
            //}

            return replications;
        }

        
    }
}
