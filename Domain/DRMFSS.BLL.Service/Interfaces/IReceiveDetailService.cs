
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using DRMFSS.BLL.MetaModels;
using DRMFSS.Web.Models;


namespace DRMFSS.BLL.Services
{
    public interface IReceiveDetailService
    {

        bool AddReceiveDetail(ReceiveDetail receiveDetail);
        bool DeleteReceiveDetail(ReceiveDetail receiveDetail);
        bool DeleteById(int id);
        bool EditReceiveDetail(ReceiveDetail receiveDetail);
        ReceiveDetail FindById(int id);
        List<ReceiveDetail> GetAllReceiveDetail();
        List<ReceiveDetail> FindBy(Expression<Func<ReceiveDetail, bool>> predicate);

        List<ReceiveDetail> GetByReceiveId(Guid receiveId);
        List<ReceiveDetailViewModelDto> ByReceiveIDetached(Guid? receiveId, string weightMeasurmentCode);
<<<<<<< HEAD
=======
        IEnumerable<ReceiveDetail> Get(
        IEnumerable<ReceiveDetail> Get(
            Expression<Func<ReceiveDetail, bool>> filter = null,
            Func<IQueryable<ReceiveDetail>, IOrderedQueryable<ReceiveDetail>> orderBy = null,
            string includeProperties = "");
>>>>>>> 19a612708fed306797436777698c2f1beef8a1aa

    }
}


