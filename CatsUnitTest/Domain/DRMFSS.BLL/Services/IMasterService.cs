
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IMasterService
    {

        bool AddMaster(Master master);
        bool DeleteMaster(Master master);
        bool DeleteById(int id);
        bool EditMaster(Master master);
        Master FindById(int id);
        List<Master> GetAllMaster();
        List<Master> FindBy(Expression<Func<Master, bool>> predicate);


    }
}


