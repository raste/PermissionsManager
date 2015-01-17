// Simple Permissions Manager (https://github.com/raste/PermissionsManager)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DataAccess;
using BusinessLayer;

namespace CustomControls
{
    public partial class SearchUserResultControl : UserControl
    {
        public event ReportEventHandler SeeReportsClick = null;
        public event ReportEventHandler SeeDeletedReportsClick = null;
        public event ReportEventHandler ChangeRoleClick = null;

        public User user { get; set; }

        public SearchUserResultControl()
        {
            InitializeComponent();
        }

        private void btnSeeReports_Click(object sender, EventArgs e)
        {
            ReportEventArgs args = new ReportEventArgs();
            args.user = user;
            if (SeeReportsClick != null)
            {
                SeeReportsClick(this, args);
            }
        }

        private void btnChangeRole_Click(object sender, EventArgs e)
        {
            ReportEventArgs args = new ReportEventArgs();
            args.user = user;
            if (ChangeRoleClick != null)
            {
                ChangeRoleClick(this, args);
            }
        }

        private void btnSeeDeletedReports_Click(object sender, EventArgs e)
        {
            ReportEventArgs args = new ReportEventArgs();
            args.user = user;
            if (SeeDeletedReportsClick != null)
            {
                SeeDeletedReportsClick(this, args);
            }
        }

        public void SetData(Entities objectContext, User searchUser, UserOrRoleRights currUserRights)
        {
            Tools.CheckObjectContext(objectContext);

            if (searchUser == null)
            {
                throw new ArgumentNullException("searchUser");
            }
            if (currUserRights == null)
            {
                throw new ArgumentNullException("currUserRights");
            }

            BRoles bRoles = new BRoles();

            user = searchUser;

            lblUserName.Text = searchUser.name;

            searchUser.Reports.Load();
            lblReports.Text = string.Format("Доклади: {0}", searchUser.Reports.Count);

            if (searchUser.isAdmin == true)
            {
                lblRole.Text = "Администратор";
                lblRole.ForeColor = Color.Red;
            }
            else
            {
                Role userRole = bRoles.GetUserRole(objectContext, searchUser);

                lblRole.Text = string.Format("Роля: {0}", userRole.name);
            }

            btnSeeReports.Visible = currUserRights.SeeVisibleReportsVisible;
            btnSeeReports.Enabled = currUserRights.SeeVisibleReportsEnabled;

            btnSeeDeletedReports.Visible = currUserRights.SeeDeletedReportVisible;
            btnSeeDeletedReports.Enabled = currUserRights.SeeDeletedReportsEnabled;

            if (currUserRights.IsAdmin == true && searchUser.isAdmin == false)
            {
                btnChangeRole.Visible = true;

                List<Role> allRoles = bRoles.GetRoles(objectContext);
                if (allRoles.Count > 1)
                {
                    btnChangeRole.Enabled = true;
                }
                else
                {
                    btnChangeRole.Enabled = false;
                }
            }
            else
            {
                btnChangeRole.Visible = false;
            }

        }

       

    }
}
