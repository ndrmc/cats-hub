using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Objects;
using System.Web;
using System.Reflection;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.IO;

namespace DRMFSS.BLL
{
    partial class DRMFSSEntities1
    {
        /// <summary>
        /// 
        /// </summary>
        public enum AuditActions
        {
            I,
            U,
            D
        }

        private string userName;


        private List<BLL.Audit> auditTrailList = new List<BLL.Audit>();

        /// <summary>
        /// Called when [context created].
        /// </summary>
        partial void OnContextCreated()
        {
            HttpContext context = HttpContext.Current;
            if (context != null && context.Request.IsAuthenticated && context.User != null)
            {
                userName = context.User.Identity.Name;
            }
            else
            {
                userName = "Anonymous";
            }
            this.SavingChanges += new EventHandler(DRMFSSEntities_SavingChanges);
        }

        /// <summary>
        /// Handles the SavingChanges event of the DRMFSSEntities control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void DRMFSSEntities_SavingChanges(object sender, EventArgs e)
        {
            IEnumerable<ObjectStateEntry> changes =
                this.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Deleted |
                                                              EntityState.Modified);
            foreach (ObjectStateEntry stateEntryEntity in changes)
            {
                if (!stateEntryEntity.IsRelationship &&
                    stateEntryEntity.Entity != null &&
                    !(stateEntryEntity.Entity is BLL.Audit) && !(stateEntryEntity.Entity is BLL.Role) &&
                    !(stateEntryEntity.Entity is BLL.UserProfile) &&
                    !(stateEntryEntity.Entity is BLL.ForgetPasswordRequest))
                {
//is a normal entry, not a relationship
                    auditTrailList = this.AuditTrailFactory(stateEntryEntity, userName);
                }
            }

            if (auditTrailList.Count > 0)
            {
                foreach (var audit in auditTrailList)
                {
                    //add all audits 
                    //TODO: Remove this try catch
                    audit.AuditID = Guid.NewGuid();
                    this.AddToAudits(audit);
                }
            }
        }

        //TODO: Revise this method and make it work again.
        /// <summary>
        /// Audits the trail factory.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="UserName">Name of the user.</param>
        /// <returns></returns>
        private List<BLL.Audit> AuditTrailFactory(ObjectStateEntry entry, string UserName)
        {
            //return null;
            IUnitOfWork repository = new UnitOfWork();
            List<BLL.Audit> audits = new List<BLL.Audit>();
            
            try
            {
                int UserId = 0;
                BLL.UserProfile cuUser = repository.UserProfile.GetUser(UserName);
                if (cuUser != null)
                {
                    UserId = cuUser.UserProfileID;
                }

                if (entry.State == EntityState.Added)
                {
                    //entry is Added 
                    foreach (var propName in entry.EntitySet.ElementType.Members)
                    {
                       
                        BLL.Audit audit = new Audit();
                        audit.DateTime = DateTime.Now;
                        audit.TableName = entry.Entity.GetType().Name;
                        audit.LoginID = UserId;
                        audit.HubID = cuUser.DefaultHub.HubID;
                        //TODO: fix this partion id
                        audit.PartitionID = 0;
                        // this means the value is changed
                        audit.OldValue = null;
                        audit.NewValue = entry.CurrentValues[propName.Name].ToString();
                        //Dispose the second context 
                        audit.Action = AuditActions.I.ToString();
                        audit.PrimaryKey = entry.EntityKey.EntityKeyValues[0].Value.ToString();
                        //assing collection of mismatched Columns name as serialized string 
                        audit.ColumnName = propName.Name;
                        audits.Add(audit);


                    }
                }
                else if (entry.State == EntityState.Deleted)
                {
                    BLL.Audit audit = new Audit();
                    audit.DateTime = DateTime.Now;
                    audit.TableName = entry.Entity.GetType().Name;
                    audit.LoginID = UserId;
                    audit.HubID = cuUser.DefaultHub.HubID;
                    //TODO: fix this partion id
                    audit.PartitionID = 0;
                    //Dispose the second context 
                    audit.Action = AuditActions.D.ToString();
                    audit.PrimaryKey = entry.EntityKey.EntityKeyValues[0].Value.ToString();
                    //assing collection of mismatched Columns name as serialized string 
                    audit.ColumnName = null;
                    audits.Add(audit);
                }
                else
                {
                    //entry is modified
                    DRMFSSEntities1 entities = new DRMFSSEntities1();
                    object orgentry = entities.GetObjectByKey(entry.EntityKey);
                    if (orgentry != null)
                    {
                        EntityObject obj = (orgentry as EntityObject);
                        ObjectStateEntry oldEntry =
                            entities.ObjectStateManager.GetObjectStateEntry(((IEntityWithKey) obj).EntityKey);

                        // ChangedValues values = GetChangedValues(stateEntry, entry);
                        foreach (string propName in entry.GetModifiedProperties())
                        {
                            if (
                                !oldEntry.CurrentValues[propName].ToString().Equals(
                                    entry.CurrentValues[propName].ToString()))
                            {
                                BLL.Audit audit = new Audit();
                                audit.DateTime = DateTime.Now;
                                audit.TableName = entry.Entity.GetType().Name;
                                audit.LoginID = UserId;
                                audit.HubID = cuUser.DefaultHub.HubID;
                                //TODO: fix this partion id
                                audit.PartitionID = 0;
                                // this means the value is changed
                                audit.OldValue = oldEntry.CurrentValues[propName].ToString();
                                audit.NewValue = entry.CurrentValues[propName].ToString();
                                //Dispose the second context 
                                audit.Action = AuditActions.U.ToString();
                                audit.PrimaryKey = entry.EntityKey.EntityKeyValues[0].Value.ToString();
                                //assing collection of mismatched Columns name as serialized string 
                                audit.ColumnName = propName;
                                audits.Add(audit);
                            }

                        }
                        entities.Dispose(true);

                    }
                }

                return audits;
            }
            catch
            {
                return audits;
            }
        }

    }
}
