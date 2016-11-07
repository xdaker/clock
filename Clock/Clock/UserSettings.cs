using System;
using System.Collections.Generic;

namespace Clock
{
    public static class UserSettings
    {
        public static bool Wifi = false;
        public static bool Shutdown = false;
        public static List<DateTime> ListAlarm = new List<DateTime>();
        public static List<DateTime> ListRemind = new List<DateTime>();
    }
}
