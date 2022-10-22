using System;

namespace TeamGosuWebApp.Utility {
    public class DateDisplayHelper {
        private static String[] MonthStrings = new String[] {
            "Jan.",
            "Feb.",
            "Mar.",
            "April",
            "May",
            "June",
            "July",
            "Aug",
            "Sept.",
            "Oct.",
            "Nov.",
            "Dec."
        };

        public static String GetDisplayedDateString(DateTime utcDate) {            
            DateTime date = utcDate.ToLocalTime();
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            return MonthStrings[month - 1] + " " + day + GetDayOfMonthSuffix(day) + " " + year;
        }

        public static String GetDayOfMonthSuffix(int day) {
            if (day == 1 || day == 21 || day == 31)
                return "st";
            else if (day == 2 || day == 22)
                return "nd";
            else if (day == 3 || day == 23)
                return "rd";
            else
                return "th";
        }
    }
}
