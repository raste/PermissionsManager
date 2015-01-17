// Simple Permissions Manager (https://github.com/raste/PermissionsManager)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using System.Windows.Forms;
using DataAccess;
using BusinessLayer;

namespace CustomControls
{
    public partial class ReportControl : UserControl
    {
        public event ReportEventHandler DeleteButtonClick = null;
        public event ReportEventHandler SolveButtonClick = null;

        public Report report { get; set; }

        public ReportControl()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ReportEventArgs args = new ReportEventArgs();
            args.report = report;
            if (DeleteButtonClick != null)
            {
                DeleteButtonClick(this, args);
            }
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            ReportEventArgs args = new ReportEventArgs();
            args.report = report;
            if (SolveButtonClick != null)
            {
                SolveButtonClick(this, args);
            }
        }
        

        public void SetData(Report currReport, UserOrRoleRights userRights)
        {
            if (currReport == null)
            {
                throw new ArgumentNullException("currReport");
            }

            if (userRights == null)
            {
                throw new ArgumentNullException("userRights");
            }

            if (!currReport.UserReference.IsLoaded)
            {
                currReport.UserReference.Load();
            }

            report = currReport;

            if (currReport.visible == true)
            {
                btnDelete.Visible = userRights.DeleteReportVisible;
                btnDelete.Enabled = userRights.DeleteReportEnabled;
            }
            else
            {
                btnDelete.Visible = false;
            }

            if (currReport.visible == true)
            {
                if (currReport.solved == false)
                {
                    btnSolve.Visible = userRights.MarkReportAsResolvedVisible;
                    btnSolve.Enabled = userRights.MarkReportAsResolvedEnabled;
                }
                else
                {
                    btnSolve.Visible = false;
                }
            }
            else
            {
                btnSolve.Visible = false;
            }

            lblDateAdded.Text = report.dateAdded.ToLocalTime().ToString();
            lblUser.Text = currReport.User.name;

            lblAbout.Text = string.Format("Относно: {0}", currReport.about);

            if (currReport.solved == true)
            {
                lblStatus.Text = "Статус: решен";
            }
            else
            {
                lblStatus.Text = "Статус: не решен";
            }

            lblDescription.Text = currReport.description;

        }
    }
}
