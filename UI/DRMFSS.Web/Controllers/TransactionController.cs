using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRMFSS.BLL;

namespace DRMFSS.Web.Controllers
{
     [Authorize]
    public class TransactionController : BaseController
    {
        //
        // GET: /Transaction/
        IUnitOfWork repository = new UnitOfWork();
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult Journal()
        {
            
            return View(repository.Transaction.GetAll().OrderByDescending(o=> o.TransactionID).ToList());
        }

        public ActionResult Ledger()
        {
            ViewBag.Ledgers = repository.Ledger.GetAll();
            ViewBag.Commodities = repository.Commodity.GetAllParents();

            return View();
        }

        public ActionResult LedgerPartial()
        {
            int account = Convert.ToInt32(Request["account"]);
            int commodity = Convert.ToInt32(Request["commodity"]);
            int ledger = Convert.ToInt32(Request["ledger"]);

            var transactions = (repository.Transaction.GetTransactionsForLedger(ledger,account,commodity).OrderByDescending(o => o.TransactionID).ToList());
            return PartialView(transactions);
        }


        public ActionResult GetAccounts(int LedgerID)
        {
            List<Account> accounts = repository.Transaction.GetActiveAccountsForLedger(LedgerID);

            return Json(new SelectList( accounts, "AccountID", "EntityName"), JsonRequestBehavior.AllowGet);
        }

    }
}
