// Simple Permissions Manager (https://github.com/raste/PermissionsManager)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess;

namespace BusinessLayer
{

    public class BReports
    {

        public void AddReport(Entities objectContext, User currUser, string about, string description)
        {
            Tools.CheckObjectContext(objectContext);

            if (currUser == null)
            {
                throw new ArgumentNullException("currUser");
            }

            if (string.IsNullOrEmpty(about))
            {
                throw new ArgumentNullException("about");
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException("description");
            }

            Report newReport = new Report();

            newReport.User = currUser;
            newReport.about = about;
            newReport.description = description;
            newReport.dateAdded = DateTime.Now;
            newReport.solved = false;
            newReport.visible = true;

            objectContext.AddToReportSet(newReport);
            Tools.Save(objectContext);

        }

        public void DeleteReport(Entities objectContext, Report report)
        {
            Tools.CheckObjectContext(objectContext);

            if (report == null)
            {
                throw new ArgumentNullException("report");
            }

            if (report.visible == false)
            {
                throw new InvalidOperationException("report is already deleted.");
            }

            report.visible = false;
            Tools.Save(objectContext);
        }

        public void SolveReport(Entities objectContext, Report report)
        {
            Tools.CheckObjectContext(objectContext);

            if (report == null)
            {
                throw new ArgumentNullException("report");
            }

            if (report.visible == false)
            {
                throw new InvalidOperationException("report is already deleted.");
            }

            if (report.solved == true)
            {
                throw new InvalidOperationException("report is already solved.");
            }

            report.solved = true;
            Tools.Save(objectContext);
        }

        public List<Report> GetUserReports(Entities objectContext, User currUser, bool onlyVisible)
        {
            Tools.CheckObjectContext(objectContext);

            if (currUser == null)
            {
                throw new ArgumentNullException("currUser");
            }

            List<Report> reports = new List<Report>();

            if (onlyVisible == true)
            {
                reports = objectContext.ReportSet.Where(rp => rp.User.ID == currUser.ID && rp.visible == true).ToList();
            }
            else
            {
                reports = objectContext.ReportSet.Where(rp => rp.User.ID == currUser.ID && rp.visible == false).ToList();
            }

            return reports;
        }

        public List<Report> GetAllReports(Entities objectContext, bool onlyVisible)
        {
            Tools.CheckObjectContext(objectContext);

            List<Report> reports = new List<Report>();

            if (onlyVisible == true)
            {
                reports = objectContext.ReportSet.Where(rp => rp.visible == true).ToList();
            }
            else
            {
                reports = objectContext.ReportSet.Where(rp => rp.visible == false).ToList();
            }

            return reports;
        }





    }
}
