using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

namespace DRMFSS.BLL
{
    public partial class DispatchAllocation
    {

        public Decimal AmountInUnit
        {
            set { ; }
            get { return this.Amount; }
        }

        public decimal RemainingQuantityInQuintals
        {
            set { ; }
            get {
                    return this.Amount - DispatchedAmount;
                }
        }

        public decimal RemainingQuantityInUnit
        {
            set { ; }
            get
            {
                return this.Amount - DispatchedAmountInUnit;
            }
        }

        public decimal DispatchedAmount
        {
            set { ; }
            get { return GetRelatedDispatchsAmountInQuintals(); }
        }

        public Decimal DispatchedAmountInUnit
        {
            set { ; }
            get { return GetRelatedDispatchsAmountInUnit(); }
        }

        public decimal GetRelatedDispatchsAmountInQuintals()
        {
            return this.Dispatches.SelectMany(dispatch => dispatch.DispatchDetails).Sum(detail => (detail.DispatchedQuantityInMT*10));
        }

        public decimal GetRelatedDispatchsAmountInUnit()
        {
            return this.Dispatches.SelectMany(dispatch => dispatch.DispatchDetails).Sum(detail => (detail.DispatchedQuantityInUnit));
        }
    }
}
