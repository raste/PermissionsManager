// Simple Permissions Manager (https://github.com/raste/PermissionsManager)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

namespace BusinessLayer
{
    public static class UserRights
    {
        private static long searchForUsers = 1;
        private static long addReport = 2;
        private static long seeVisibleReports = 3;
        private static long seeDeletedReports = 4;
        private static long markReportAsSolved = 5;
        private static long deleteReports = 6;

        private static int rightsCount = 6;

        public static long SearchForUsers
        {
            get { return searchForUsers; }
        }

        public static long AddReport
        {
            get { return addReport; }
        }

        public static long SeeVisibleReports
        {
            get { return seeVisibleReports; }
        }

        public static long SeeDeletedReports
        {
            get { return seeDeletedReports; }
        }

        public static long MarkReportAsSolved
        {
            get { return markReportAsSolved; }
        }

        public static long DeleteReports
        {
            get { return deleteReports; }
        }

        public static long RightsCount
        {
            get { return rightsCount; }
        }

    }
}
