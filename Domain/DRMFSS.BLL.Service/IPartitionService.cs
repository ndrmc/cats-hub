
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IPartitionService
    {

        bool AddPartition(Partition entity);
        bool DeletePartition(Partition entity);
        bool DeleteById(int id);
        bool EditPartition(Partition entity);
        Partition FindById(int id);
        List<Partition> GetAllPartition();
        List<Partition> FindBy(Expression<Func<Partition, bool>> predicate);
         List<ViewModels.ReplicationViewModel> GetHubsSyncrtonizationDetails(int publication);

    }
}


