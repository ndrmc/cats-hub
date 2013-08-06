
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface ISessionHistoryService
    {

        bool AddSessionHistory(SessionHistory entity);
        bool DeleteSessionHistory(SessionHistory entity);
        bool DeleteById(int id);
        bool EditSessionHistory(SessionHistory entity);
        SessionHistory FindById(int id);
        List<SessionHistory> GetAllSessionHistory();
        List<SessionHistory> FindBy(Expression<Func<SessionHistory, bool>> predicate);


    }
}


