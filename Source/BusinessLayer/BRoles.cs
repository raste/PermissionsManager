// Simple Permissions Manager (https://github.com/raste/PermissionsManager)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;

namespace BusinessLayer
{

    public class BRoles
    {

        object addingRoleSync = new object();
        object editingRoleSync = new object();

        public void AddRole(Entities objectContext, string name, NewRoleRights roleRights)
        {
            Tools.CheckObjectContext(objectContext);

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (roleRights == null)
            {
                throw new ArgumentNullException("roleRights");
            }

            lock (addingRoleSync)
            {
                if (GetRole(objectContext, name, false) != null)
                {
                    throw new InvalidOperationException("There is already role with same name");
                }

                Role newRole = new Role();

                newRole.name = name;

                objectContext.AddToRoleSet(newRole);
                Tools.Save(objectContext);

                AddRoleRights(objectContext, newRole, roleRights);
            }

        }


        private void AddRoleRights(Entities objectContext, Role newRole, NewRoleRights roleRights)
        {
            Tools.CheckObjectContext(objectContext);

            if (roleRights == null)
            {
                throw new ArgumentNullException("roleRights");
            }

            if (newRole == null)
            {
                throw new ArgumentNullException("newRole");
            }

            newRole.Role_Rights.Load();

            if (newRole.Role_Rights.Count > 0)
            {
                throw new InvalidOperationException("newRole already have rights attached.");
            }

            List<Right> allRights = GetAllRights(objectContext);
    

            Role_Right rSearchUsers = new Role_Right();
            rSearchUsers.Role = newRole;
            rSearchUsers.Right = allRights[0];
            rSearchUsers.enabled = roleRights.SearchForUsersEnabled;
            rSearchUsers.visible = roleRights.SearchForUsersVisible;
            objectContext.AddToRole_RightSet(rSearchUsers);

            Role_Right rAddReport = new Role_Right();
            rAddReport.Role = newRole;
            rAddReport.Right = allRights[1];
            rAddReport.enabled = roleRights.AddReportEnabled;
            rAddReport.visible = roleRights.AddReportVisible;
            objectContext.AddToRole_RightSet(rAddReport);

            Role_Right rSeeVisibleReports = new Role_Right();
            rSeeVisibleReports.Role = newRole;
            rSeeVisibleReports.Right = allRights[2];
            rSeeVisibleReports.enabled = roleRights.SeeVisibleReportsEnabled;
            rSeeVisibleReports.visible = roleRights.SeeVisibleReportsVisible;
            objectContext.AddToRole_RightSet(rSeeVisibleReports);

            Role_Right rSeeDeletedReports = new Role_Right();
            rSeeDeletedReports.Role = newRole;
            rSeeDeletedReports.Right = allRights[3];
            rSeeDeletedReports.enabled = roleRights.SeeDeletedReportsEnabled;
            rSeeDeletedReports.visible = roleRights.SeeDeletedReportVisible;
            objectContext.AddToRole_RightSet(rSeeDeletedReports);

            Role_Right rMarkReportResolved = new Role_Right();
            rMarkReportResolved.Role = newRole;
            rMarkReportResolved.Right = allRights[4];
            rMarkReportResolved.enabled = roleRights.MarkReportAsResolvedEnabled;
            rMarkReportResolved.visible = roleRights.MarkReportAsResolvedVisible;
            objectContext.AddToRole_RightSet(rMarkReportResolved);

            Role_Right rDeleteReport = new Role_Right();
            rDeleteReport.Role = newRole;
            rDeleteReport.Right = allRights[5];
            rDeleteReport.enabled = roleRights.DeleteReportEnabled;
            rDeleteReport.visible = roleRights.DeleteReportVisible;
            objectContext.AddToRole_RightSet(rDeleteReport);

            Tools.Save(objectContext);
        }

        private void ChangeRoleRights(Entities objectContext, Role currRole, NewRoleRights newRights)
        {
            Tools.CheckObjectContext(objectContext);

            if (currRole == null)
            {
                throw new ArgumentNullException("currRole");
            }

            if (newRights == null)
            {
                throw new ArgumentNullException("newRights");
            }

            Role_Right currRight = objectContext.Role_RightSet.FirstOrDefault(rr => rr.Right.ID == UserRights.SearchForUsers && rr.Role.ID == currRole.ID);
            if (currRight == null)
            {
                throw new InvalidOperationException("role doesn't have set right ID : 1");
            }

            currRight.enabled = newRights.SearchForUsersEnabled;
            currRight.visible = newRights.SearchForUsersVisible;

            Tools.Save(objectContext);

            currRight = objectContext.Role_RightSet.FirstOrDefault(rr => rr.Right.ID == UserRights.AddReport && rr.Role.ID == currRole.ID);
            if (currRight == null)
            {
                throw new InvalidOperationException("role doesn't have set right ID : 2");
            }

            currRight.enabled = newRights.AddReportEnabled;
            currRight.visible = newRights.AddReportVisible;

            Tools.Save(objectContext);

            currRight = objectContext.Role_RightSet.FirstOrDefault(rr => rr.Right.ID == UserRights.SeeVisibleReports && rr.Role.ID == currRole.ID);
            if (currRight == null)
            {
                throw new InvalidOperationException("role doesn't have set right ID : 3");
            }

            currRight.enabled = newRights.SeeVisibleReportsEnabled;
            currRight.visible = newRights.SeeVisibleReportsVisible;

            Tools.Save(objectContext);

            currRight = objectContext.Role_RightSet.FirstOrDefault(rr => rr.Right.ID == UserRights.SeeDeletedReports && rr.Role.ID == currRole.ID);
            if (currRight == null)
            {
                throw new InvalidOperationException("role doesn't have set right ID : 4");
            }

            currRight.enabled = newRights.SeeDeletedReportsEnabled;
            currRight.visible = newRights.SeeDeletedReportVisible;

            Tools.Save(objectContext);

            currRight = objectContext.Role_RightSet.FirstOrDefault(rr => rr.Right.ID == UserRights.MarkReportAsSolved && rr.Role.ID == currRole.ID);
            if (currRight == null)
            {
                throw new InvalidOperationException("role doesn't have set right ID : 5");
            }

            currRight.enabled = newRights.MarkReportAsResolvedEnabled;
            currRight.visible = newRights.MarkReportAsResolvedVisible;

            Tools.Save(objectContext);

            currRight = objectContext.Role_RightSet.FirstOrDefault(rr => rr.Right.ID == UserRights.DeleteReports && rr.Role.ID == currRole.ID);
            if (currRight == null)
            {
                throw new InvalidOperationException("role doesn't have set right ID : 6");
            }

            currRight.enabled = newRights.DeleteReportEnabled;
            currRight.visible = newRights.DeleteReportVisible;

            Tools.Save(objectContext);
        }

        public Role GetRole(Entities objectContext, string name, bool throwExcIfNull)
        {
            Tools.CheckObjectContext(objectContext);

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            Role role = objectContext.RoleSet.FirstOrDefault(rle => rle.name == name);

            if (role == null && throwExcIfNull == true)
            {
                throw new ArgumentOutOfRangeException(string.Format("No role with name = {0}", name));
            }

            return role;
        }

        public Role GetRole(Entities objectContext, long id, bool throwExcIfNull)
        {
            Tools.CheckObjectContext(objectContext);

            Role role = objectContext.RoleSet.FirstOrDefault(rle => rle.ID == id);

            if (role == null && throwExcIfNull == true)
            {
                throw new ArgumentOutOfRangeException(string.Format("No role with id = {0}", id));
            }

            return role;
        }

        public Role GetUserRole(Entities objectContext, User user)
        {
            Tools.CheckObjectContext(objectContext);

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if(user.isAdmin == true)
            {
                throw new InvalidOperationException("user is admin");
            }

            user.User_Roles.Load();

            User_Role userRole = user.User_Roles.FirstOrDefault(ur => ur.active == true);
            if(userRole == null)
            {
                throw new ArgumentNullException("user doesnt have role set.");
            }

            userRole.RoleReference.Load();

            Role role = userRole.Role;

            return role;
        }


        private List<Right> GetAllRights(Entities objectContext)
        {
            Tools.CheckObjectContext(objectContext);

            List<Right> allRights = objectContext.RightSet.ToList();

            if (allRights == null)
            {
                throw new ArgumentNullException("There are no rights (controls) in database");
            }

            if (allRights.Count != UserRights.RightsCount)
            {
                throw new ArgumentOutOfRangeException("The rights (controls) in database are not 6");
            }

            return allRights;
        }

        public void AddRoleToUser(Entities objectContext, Role role, User user)
        {
            Tools.CheckObjectContext(objectContext);

            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.isAdmin == true)
            {
                throw new InvalidOperationException("Roles cannot be added to admins");
            }

            user.User_Roles.Load();

            if (user.User_Roles.Count < 1)  // First role to new user is being given
            {
                User_Role newUserRole = new User_Role();

                newUserRole.User = user;
                newUserRole.Role = role;
                newUserRole.active = true;

                objectContext.AddToUser_RoleSet(newUserRole);
                Tools.Save(objectContext);

            }
            else                            // User role is being changed
            {                                
                User_Role currRole = user.User_Roles.FirstOrDefault(ur => ur.active == true && ur.Role.ID == role.ID);
                if (currRole != null)
                {
                    return; // New role same as current
                }

                List<User_Role> activeRoles = user.User_Roles.Where(ur => ur.active == true).ToList();
                if (activeRoles != null && activeRoles.Count > 0)
                {
                    foreach (User_Role activeRole in activeRoles)
                    {
                        activeRole.active = false;
                    }

                    Tools.Save(objectContext);
                }

                User_Role oldInactiveRole = user.User_Roles.FirstOrDefault(ur => ur.active == false && ur.Role.ID == role.ID);
                if (oldInactiveRole != null)
                {
                    oldInactiveRole.active = true;
                    Tools.Save(objectContext);
                }
                else
                {

                    User_Role newUserRole = new User_Role();

                    newUserRole.User = user;
                    newUserRole.Role = role;
                    newUserRole.active = true;

                    objectContext.AddToUser_RoleSet(newUserRole);
                    Tools.Save(objectContext);
                }
            }


        }

        public void EditRole(Entities objectContext, Role currRole, string newName, NewRoleRights editedRights)
        {
            Tools.CheckObjectContext(objectContext);

            if (currRole == null)
            {
                throw new ArgumentNullException("currRole");
            }

            lock (editingRoleSync)
            {

                if (!string.IsNullOrEmpty(newName))
                {
                    currRole.name = newName;
                    Tools.Save(objectContext);
                }

                ChangeRoleRights(objectContext, currRole, editedRights);
            }
        }

        public List<Role> GetRoles(Entities objectContext)
        {
            Tools.CheckObjectContext(objectContext);

            List<Role> roles = objectContext.RoleSet.ToList();

            return roles;
        }

        /// <summary>
        /// Counts number of roles
        /// </summary>
        /// <param name="objectContext"></param>
        /// <returns></returns>
        public int CountRoles(Entities objectContext)
        {
            Tools.CheckObjectContext(objectContext);

            int result = 0;

            result = objectContext.RoleSet.Count();

            return result;
        }
       

    }
}
