using System.ComponentModel.DataAnnotations;
using System.Linq;
using DRMFSS.BLL.MetaModels;

namespace DRMFSS.BLL
{
   
    partial class Role
    {

        public static void AddRole(Role role)
        {
            DRMFSSEntities1 entities = new DRMFSSEntities1();
            entities.AddToRoles(role);
            entities.SaveChanges();
        }

        public void RemoveRole(string roleName, string userName)
        {
            BLL.DRMFSSEntities1 entities = new DRMFSSEntities1();
            UserRole role = entities.UserRoles.Where(r => r.Role.Name == roleName && r.UserProfile.UserName == userName).SingleOrDefault();
            if (role != null)
            {
                entities.UserRoles.DeleteObject(role); 
                entities.SaveChanges();
            } 
        }

        public void AddUserToRole(int roleId, string userName)
        {
            BLL.DRMFSSEntities1 entities = new DRMFSSEntities1();
            UserProfile user = entities.UserProfiles.Where(p => p.UserName == userName).FirstOrDefault();
            if (user != null)
            {
                UserRole role = new UserRole();
                role.RoleID = roleId;
                role.UserProfileID = user.UserProfileID;
                entities.UserRoles.AddObject(role);
                entities.SaveChanges();
            }
            
        }

        public static Role GetRole(string name)
        {
            DRMFSSEntities1 entities = new DRMFSSEntities1();
            return entities.Roles.Where(p => p.Name == name).SingleOrDefault();
            ;
        }


        public static bool RoleExists(string name)
        {
            DRMFSSEntities1 entities = new DRMFSSEntities1();
            var count = entities.Roles.Where(p => p.Name == name).Count();
            return (count > 0);
        }
                
        public enum RoleEnum
        {
            Admin,
            DataEntry,
        }


    }
  
}
