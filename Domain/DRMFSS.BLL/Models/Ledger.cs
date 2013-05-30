using System;
using System.Collections.Generic;

namespace DRMFSS.BLL
{
    public partial class Ledger
    {
        public Ledger()
        {
            this.Transactions = new List<Transaction>();
        }

        public int LedgerID { get; set; }
        public string Name { get; set; }
        public int LedgerTypeID { get; set; }
        public virtual LedgerType LedgerType { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
