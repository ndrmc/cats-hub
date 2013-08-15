
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface ISessionAttemptService
    {

        bool AddSessionAttempt(SessionAttempt entity);
        bool DeleteSessionAttempt(SessionAttempt entity);
        bool DeleteById(int id);
        bool EditSessionAttempt(SessionAttempt entity);
        SessionAttempt FindById(int id);
        List<SessionAttempt> GetAllSessionAttempt();
        List<SessionAttempt> FindBy(Expression<Func<SessionAttempt, bool>> predicate);


    }
}


