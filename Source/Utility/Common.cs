using System;
using System.Globalization;

namespace Utility
{
    public static class Common
    {
        public static string TimeLeft(DateTime utcDate)
        {
            TimeSpan timeLeft = DateTime.Now - utcDate;
            string timeLeftString = "";
            if (timeLeft.Days > 0)
            {
                timeLeftString += timeLeft.Days == 1 ? timeLeft.Days + " day ago" : timeLeft.Days + " days ago";
            }
            else if (timeLeft.Hours > 0)
            {
                timeLeftString += timeLeft.Hours == 1 ? timeLeft.Hours + " hour ago" : timeLeft.Hours + " hours ago";
            }
            else if (timeLeft.Minutes > 0)
            {
                timeLeftString += timeLeft.Minutes == 1 ? timeLeft.Minutes + " minute ago" : timeLeft.Minutes + " minutes ago";
            }
            else
            {
                timeLeftString += timeLeft.Seconds == 1 ? timeLeft.Seconds + " second ago" : timeLeft.Seconds + " seconds ago";
            }
            return timeLeftString;
        }

        public static string TimeLeftHindi(DateTime utcDate)
        {
            TimeSpan timeLeft = DateTime.Now - utcDate;
            string timeLeftString = "";
            if (timeLeft.Days > 0)
            {
                timeLeftString += timeLeft.Days == 1 ? timeLeft.Days + " दिन पहले प्रकाशित" : timeLeft.Days + " दिन पहले प्रकाशित";
            }
            else if (timeLeft.Hours > 0)
            {
                timeLeftString += timeLeft.Hours == 1 ? timeLeft.Hours + " घंटे पहले प्रकाशित" : timeLeft.Hours + " घंटे पहले प्रकाशित";
            }
            else if (timeLeft.Minutes > 0)
            {
                timeLeftString += timeLeft.Minutes == 1 ? timeLeft.Minutes + " मिनट पहले प्रकाशित" : timeLeft.Minutes + " मिनट पहले प्रकाशित";
            }
            else
            {
                timeLeftString += timeLeft.Seconds == 1 ? timeLeft.Seconds + " सेकंड पहले प्रकाशित" : timeLeft.Seconds + " सेकंड पहले प्रकाशित";
            }
            return timeLeftString;
        }


        public static string ChatBoxTime(DateTime utcDate)
        {
            TimeSpan timeLeft = DateTime.Now - utcDate;
            string timeLeftString = "";
            if (timeLeft.Days > 0)
            {
                timeLeftString += timeLeft.Days == 1 ? timeLeft.Days + " day ago" : timeLeft.Days + " days ago";
            }
            else if (timeLeft.Hours > 0)
            {
                timeLeftString += timeLeft.Hours == 1 ? timeLeft.Hours + " hour ago" : timeLeft.Hours + " hours ago";
            }
            else if (timeLeft.Minutes > 0)
            {
                timeLeftString += timeLeft.Minutes == 1 ? timeLeft.Minutes + " minute ago" : timeLeft.Minutes + " minutes ago";
            }
            else
            {
                timeLeftString += timeLeft.Seconds == 1 ? timeLeft.Seconds + " second ago" : timeLeft.Seconds + " seconds ago";
            }
            return timeLeftString;
        } 
    }
}
