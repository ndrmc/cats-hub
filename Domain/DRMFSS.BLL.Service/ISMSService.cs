
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface ISMSService
    {

        bool AddSMS(SMS entity);
        bool DeleteSMS(SMS entity);
        bool DeleteById(int id);
        bool EditSMS(SMS entity);
        SMS FindById(int id);
        List<SMS> GetAllSMS();
        List<SMS> FindBy(Expression<Func<SMS, bool>> predicate);


    }
}


