using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
   public interface IStackEventService
    {

       bool AddStackEvent(StackEvent entity);
       bool DeleteStackEvent(StackEvent entity);
       bool DeleteById(int id);
       bool EditStackEvent(StackEvent entity);
       StackEvent FindById(int id);
       List<StackEvent> GetAllStackEvent();
       List<StackEvent> FindBy(Expression<Func<StackEvent, bool>> predicate);

       List<ViewModels.StackEventLogViewModel> GetAllStackEvents(UserProfile user);
       List<ViewModels.StackEventLogViewModel> GetAllStackEventsByStoreIdStackId(UserProfile user, int StackId, int StoreId);
      
    }
}

      


      
