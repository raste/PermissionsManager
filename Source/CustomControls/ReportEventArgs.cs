// Simple Permissions Manager (https://github.com/raste/PermissionsManager)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

using System;
using DataAccess;

namespace CustomControls
{
    public class ReportEventArgs : EventArgs
    {
        public Report report { get; set; }
        public User user { get; set; }
    }
}
