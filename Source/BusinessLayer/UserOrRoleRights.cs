// Simple Permissions Manager (https://github.com/raste/PermissionsManager)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;

namespace BusinessLayer
{
    public class UserOrRoleRights
    {
        private bool searchForUsersEnabled = false;
        private bool searchForUsersVisible = false;

        private bool addReportEnabled = false;
        private bool addReportVisible = false;

        private bool seeVisibleReportsEnabled = false;
        private bool seeVisibleReportsVisible = false;

        private bool seeDeletedReportsEnabled = false;
        private bool seeDeletedReportsVisible = false;

        private bool markReportAsResolvedEnabled = false;
        private bool markReportAsResolvedVisible = false;

        private bool deleteReportEnabled = false;
        private bool deleteReportVisible = false;

        private bool isAdmin = false;

        public bool IsAdmin
        {
            get { return isAdmin; }
        }

        public bool SearchForUsersEnabled
        {
            get { return searchForUsersEnabled; }
        }

        public bool SearchForUsersVisible
        {
            get { return searchForUsersVisible; }
        }

        public bool AddReportVisible
        {
            get { return addReportVisible; }
        }

        public bool AddReportEnabled
        {
            get { return addReportEnabled; }
        }

        public bool SeeVisibleReportsEnabled
        {
            get { return seeVisibleReportsEnabled; }
        }

        public bool SeeVisibleReportsVisible
        {
            get { return seeVisibleReportsVisible; }
        }

        public bool SeeDeletedReportsEnabled
        {
            get { return seeDeletedReportsEnabled; }
        }

        public bool SeeDeletedReportVisible
        {
            get { return seeDeletedReportsVisible; }
        }

        public bool MarkReportAsResolvedEnabled
        {
            get { return markReportAsResolvedEnabled; }
        }

        public bool MarkReportAsResolvedVisible
        {
            get { return markReportAsResolvedVisible; }
        }

        public bool DeleteReportEnabled
        {
            get { return deleteReportEnabled; }
        }

        public bool DeleteReportVisible
        {
            get { return deleteReportVisible; }
        }

        /// <summary>
        /// All rights set to false
        /// </summary>
        public UserOrRoleRights(){ }

        /// <summary>
        /// Check user rights and sets them accordingly
        /// </summary>
        /// <param name="user"></param>
        public UserOrRoleRights(Entities objectContext, User user)
        {
            Tools.CheckObjectContext(objectContext);

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            if (user.isAdmin == true)
            {
                UserIsAdmin();
            }
            else
            {
                SetUserRights(objectContext, user);
            }

        }

        /// <summary>
        /// Check role rights and sets them accordingly
        /// </summary>
        /// <param name="objectContext"></param>
        /// <param name="user"></param>
        public UserOrRoleRights(Entities objectContext, Role role)
        {
            Tools.CheckObjectContext(objectContext);

            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            SetRights(objectContext, role);
        }

        private void UserIsAdmin()
        {
            isAdmin = true;

            searchForUsersEnabled = true;
            searchForUsersVisible = true;

            addReportEnabled = true;
            addReportVisible = true;

            seeVisibleReportsEnabled = true;
            seeVisibleReportsVisible = true;

            seeDeletedReportsEnabled = true;
            seeDeletedReportsVisible = true;

            markReportAsResolvedEnabled = true;
            markReportAsResolvedVisible = true;

            deleteReportEnabled = true;
            deleteReportVisible = true;
        }

        private void SetUserRights(Entities objectContext, User user)
        {
            Tools.CheckObjectContext(objectContext);

            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            user.User_Roles.Load();
            if (user.User_Roles.Count < 1)
            {
                throw new ArgumentOutOfRangeException("user doesnt have roles");
            }

            List<User_Role> activeRoles = user.User_Roles.Where(ur => ur.active == true).ToList();

            if (activeRoles == null)
            {
                throw new ArgumentOutOfRangeException("user doesnt have active roles.");
            }
            if(activeRoles.Count > 1)
            {
                throw new ArgumentOutOfRangeException("user have more than 1 active roles.");
            }

            activeRoles.First().RoleReference.Load();
            Role userRole = activeRoles.First().Role;

            SetRights(objectContext, userRole);
        }


        private void SetRights(Entities objectContext, Role currRole)
        {
            Tools.CheckObjectContext(objectContext);
            if (currRole == null)
            {
                throw new ArgumentNullException("currRole");
            }

            Role_Right currRight = objectContext.Role_RightSet.FirstOrDefault(rr => rr.Right.ID == UserRights.SearchForUsers && rr.Role.ID == currRole.ID);
            if (currRight == null)
            {
                throw new InvalidOperationException("role doesn't have set right ID : 1");
            }

            searchForUsersEnabled = currRight.enabled;
            searchForUsersVisible = currRight.visible;

            currRight = objectContext.Role_RightSet.FirstOrDefault(rr => rr.Right.ID == UserRights.AddReport && rr.Role.ID == currRole.ID);
            if (currRight == null)
            {
                throw new InvalidOperationException("role doesn't have set right ID : 2");
            }

            addReportEnabled = currRight.enabled;
            addReportVisible = currRight.visible;

            currRight = objectContext.Role_RightSet.FirstOrDefault(rr => rr.Right.ID == UserRights.SeeVisibleReports && rr.Role.ID == currRole.ID);
            if (currRight == null)
            {
                throw new InvalidOperationException("role doesn't have set right ID : 3");
            }

            seeVisibleReportsEnabled = currRight.enabled;
            seeVisibleReportsVisible = currRight.visible;

            currRight = objectContext.Role_RightSet.FirstOrDefault(rr => rr.Right.ID == UserRights.SeeDeletedReports && rr.Role.ID == currRole.ID);
            if (currRight == null)
            {
                throw new InvalidOperationException("role doesn't have set right ID : 4");
            }

            seeDeletedReportsEnabled = currRight.enabled;
            seeDeletedReportsVisible = currRight.visible;

            currRight = objectContext.Role_RightSet.FirstOrDefault(rr => rr.Right.ID == UserRights.MarkReportAsSolved && rr.Role.ID == currRole.ID);
            if (currRight == null)
            {
                throw new InvalidOperationException("role doesn't have set right ID : 5");
            }

            markReportAsResolvedEnabled = currRight.enabled;
            markReportAsResolvedVisible = currRight.visible;

            currRight = objectContext.Role_RightSet.FirstOrDefault(rr => rr.Right.ID == UserRights.DeleteReports && rr.Role.ID == currRole.ID);
            if (currRight == null)
            {
                throw new InvalidOperationException("role doesn't have set right ID : 6");
            }

            deleteReportEnabled = currRight.enabled;
            deleteReportVisible = currRight.visible;
        }

    }
}
