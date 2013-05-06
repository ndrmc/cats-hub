using System.Collections.Generic;
using System.Linq;

namespace DRMFSS.BLL
{
    
    partial class UserProfile
    {
        public void ChangeLanguage(string lang)
        {
            BLL.DRMFSSEntities1 context = new BLL.DRMFSSEntities1();         

            UserProfile profile = context.UserProfiles.Where(p=>p.UserName == this.UserName).SingleOrDefault();
            if(profile != null)
            {
                profile.LanguageCode = lang;
                context.SaveChanges();
            }
        }

        public void ChangeHub(int warehouseId)
        {
            BLL.DRMFSSEntities1 entities = new BLL.DRMFSSEntities1();
            var newDefault = (from w in entities.UserHubs
                              where w.HubID == warehouseId && w.UserProfileID == this.UserProfileID
                              select w).Single();
            var prevDefaults = (from w in entities.UserHubs
                                where w.HubID != warehouseId && w.UserProfileID == this.UserProfileID
                                && w.IsDefault.Trim().Equals("1")
                                select w).ToList();
            newDefault.IsDefault = "1";
            foreach (BLL.UserHub uw in prevDefaults)
            {
                uw.IsDefault = "0";
            }
            entities.SaveChanges();
        }

       

        public List<BLL.Hub> UserAllowedHubs
        {
            get
            {
                DRMFSSEntities1 entities = new DRMFSSEntities1();
                return (from w in entities.UserHubs
                                                  where w.UserProfileID == this.UserProfileID
                                                  select w.Hub).ToList();
            }
        }

        public static UserProfile GetUserById(int p)
        {
            BLL.DRMFSSEntities1 entities = new BLL.DRMFSSEntities1();
            return (from u in entities.UserProfiles
                    where u.UserProfileID == p && !u.LockedInInd && u.ActiveInd
                    select u).FirstOrDefault();
        }

        public Hub DefaultHub
        {
            get
            {
                DRMFSSEntities1 entities = new DRMFSSEntities1();
                var hub =  (from w in entities.UserHubs
                        where w.UserProfileID == this.UserProfileID && w.IsDefault.Trim().Equals("1")
                        select w.Hub).FirstOrDefault();
                if (hub == null)
                {
                    hub = (from w in entities.UserHubs
                     where w.UserProfileID == this.UserProfileID
                     select w.Hub).FirstOrDefault();
                }
                return hub;
            }
        }

        public string GetFullName()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
