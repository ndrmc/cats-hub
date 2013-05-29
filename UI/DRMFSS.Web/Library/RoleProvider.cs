using System;
using System.Linq;


namespace DRMFSS.Web
{
    public class RoleProvider :System.Web.Security.RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
         
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            BLL.CTSContext entities = new BLL.CTSContext();
            var roles = from role in entities.UserRoles
                        where role.UserProfile.UserName == username
                        select role.Role.Name;

            return roles.ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            return new string[1];
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            BLL.CTSContext entities = new BLL.CTSContext();
            var count = entities.UserRoles.Where(u => u.UserProfile.UserName == username && u.Role.Name == roleName).Count();
            return (count > 0);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        
    }
}