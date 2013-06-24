
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DRMFSS.BLL.Services
{
    public interface IInternalMovementService
    {

        bool AddInternalMovement(InternalMovement entity);
        bool DeleteInternalMovement(InternalMovement entity);
        bool DeleteById(int id);
        bool EditInternalMovement(InternalMovement entity);
        InternalMovement FindById(int id);
        public List<ViewModels.InternalMovementLogViewModel> GetAllInternalMovmentLog();
        List<InternalMovement> FindBy(Expression<Func<InternalMovement, bool>> predicate);

       void AddNewInternalMovement(ViewModels.InternalMovementViewModel viewModel, UserProfile user);
       List<ViewModels.InternalMovementLogViewModel> GetAllInternalMovmentLog();


    }
}


