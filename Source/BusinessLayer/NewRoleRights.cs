// Simple Permissions Manager (https://github.com/raste/PermissionsManager)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

namespace BusinessLayer
{
    public class NewRoleRights
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

        public bool SearchForUsersEnabled
        {
            get { return searchForUsersEnabled; }
            set { searchForUsersEnabled = value; }
        }

        public bool SearchForUsersVisible
        {
            get { return searchForUsersVisible; }
            set { searchForUsersVisible = value; }
        }

        public bool AddReportVisible
        {
            get { return addReportVisible; }
            set { addReportVisible = value; }
        }

        public bool AddReportEnabled
        {
            get { return addReportEnabled; }
            set { addReportEnabled = value; }
        }

        public bool SeeVisibleReportsEnabled
        {
            get { return seeVisibleReportsEnabled; }
            set { seeVisibleReportsEnabled = value; }
        }

        public bool SeeVisibleReportsVisible
        {
            get { return seeVisibleReportsVisible; }
            set { seeVisibleReportsVisible = value; }
        }

        public bool SeeDeletedReportsEnabled
        {
            get { return seeDeletedReportsEnabled; }
            set { seeDeletedReportsEnabled = value; }
        }

        public bool SeeDeletedReportVisible
        {
            get { return seeDeletedReportsVisible; }
            set { seeDeletedReportsVisible = value; }
        }

        public bool MarkReportAsResolvedEnabled
        {
            get { return markReportAsResolvedEnabled; }
            set { markReportAsResolvedEnabled = value; }
        }

        public bool MarkReportAsResolvedVisible
        {
            get { return markReportAsResolvedVisible; }
            set { markReportAsResolvedVisible = value; }
        }

        public bool DeleteReportEnabled
        {
            get { return deleteReportEnabled; }
            set { deleteReportEnabled = value; }
        }

        public bool DeleteReportVisible
        {
            get { return deleteReportVisible; }
            set { deleteReportVisible = value; }
        }


    }
}
