using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
   public class StackEventService:IStackEventService
    {
       private readonly IUnitOfWork _unitOfWork;

       public StackEventService(IUnitOfWork unitOfWork)
       {
           _unitOfWork = unitOfWork; 
       }
       #region Default Service Implementation
       public bool AddStackEvent(StackEvent entity)
       {
           _unitOfWork.StackEventRepository.Add(entity);
           _unitOfWork.Save();
           return true;

       }
       public bool EditStackEvent(StackEvent entity)
       {
           _unitOfWork.StackEventRepository.Edit(entity);
           _unitOfWork.Save();
           return true;

       }
       public bool DeleteStackEvent(StackEvent entity)
       {
           if (entity == null) return false;
           _unitOfWork.StackEventRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public bool DeleteById(int id)
       {
           var entity = _unitOfWork.StackEventRepository.FindById(id);
           if (entity == null) return false;
           _unitOfWork.StackEventRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<StackEvent> GetAllStackEvent()
       {
           return _unitOfWork.StackEventRepository.GetAll();
       }
       public StackEvent FindById(int id)
       {
           return _unitOfWork.StackEventRepository.FindById(id);
       }
       public List<StackEvent> FindBy(Expression<Func<StackEvent, bool>> predicate)
       {
           return _unitOfWork.StackEventRepository.FindBy(predicate);
       }
       #endregion

       public void Dispose()
       {
           _unitOfWork.Dispose();

       }
       
        public List<ViewModels.StackEventLogViewModel> GetAllStackEvents(UserProfile user)
        {
             
            

            var StackEvents = _unitOfWork.StackEventRepository.GetAll();
            var events = (from c in StackEvents select new ViewModels.StackEventLogViewModel { EventDate = c.EventDate, StackEventType = c.StackEventType.Name, Description = c.Description, Recommendation = c.Recommendation, FollowUpDate = c.FollowUpDate.Value }).ToList();
                
            return events;
        }

        public List<ViewModels.StackEventLogViewModel> GetAllStackEventsByStoreIdStackId(UserProfile user, int StackId, int StoreId)
        {
            var StackEvents = _unitOfWork.StackEventRepository.FindBy(c => c.StackNumber == StackId && c.StoreID == StoreId).ToList();

            var events = (from c in StackEvents where (c.StackNumber == StackId && c.StoreID == StoreId) select new ViewModels.StackEventLogViewModel { EventDate = c.EventDate, StackEventType = c.StackEventType.Name, Description = c.Description, Recommendation = c.Recommendation, FollowUpDate = c.FollowUpDate.Value }).ToList();
            return events;
        }

       
    }
}







   
 
      
