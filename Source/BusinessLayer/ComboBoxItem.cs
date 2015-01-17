// Simple Permissions Manager (https://github.com/raste/PermissionsManager)
// Copyright (c) 2015 Georgi Kolev. 
// Licensed under Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0).

namespace BusinessLayer
{
    public class ComboBoxItem
    {
        private string dataName;
        private string dataValue;

        public ComboBoxItem(string dataName, string dataValue)
        {
            DataName = dataName;
            DataValue = dataValue;
        }

        public string DataName
        {
            get { return dataName; }
            set { dataName = value; }
        }

        public string DataValue
        {
            get { return dataValue; }
            set { dataValue = value; }
        }

        public override string ToString()
        {
            return dataName;
        }

    }
}
