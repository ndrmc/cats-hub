using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DRMFSS.BLL
{
    
    partial class UserProfile
    {
        
        public void ChangeLanguage(string lang)
        {
            BLL.CTSContext context = new BLL.CTSContext();         

            UserProfile profile = context.UserProfiles.Where(p=>p.UserName == this.UserName).SingleOrDefault();
            if(profile != null)
            {
                profile.LanguageCode = lang;
                context.SaveChanges();
            }
        }

        public void ChangeHub(int warehouseId)
        {
            BLL.CTSContext entities = new BLL.CTSContext();
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


         [NotMapped]
        public List<BLL.Hub> UserAllowedHubs
        {
            get
            {
                CTSContext entities = new CTSContext();
                return (from w in entities.UserHubs
                                                  where w.UserProfileID == this.UserProfileID
                                                  select w.Hub).ToList();
            }
        }

        public static UserProfile GetUserById(int p)
        {
            BLL.CTSContext entities = new BLL.CTSContext();
            return (from u in entities.UserProfiles
                    where u.UserProfileID == p && !u.LockedInInd && u.ActiveInd
                    select u).FirstOrDefault();
        }
        [NotMapped]
        public Hub DefaultHub
        {
            get
            {
                
                CTSContext entities = new CTSContext();
                var hub = (from w in entities.UserHubs
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
