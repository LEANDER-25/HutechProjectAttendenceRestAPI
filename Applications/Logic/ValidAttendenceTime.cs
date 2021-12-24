using System;

namespace RESTAPIRNSQLServer.Applications.Logic
{
    public class ValidAttendenceTime
    {
        public static bool IsScheduleAvailable(DateTime checkinTime, DateTime scheduleTime)
        {
            if (checkinTime != scheduleTime)
                return false;
            return true;
        }
        public static int IsCheckInAvaible(TimeSpan value, TimeSpan target, int acceptTimeInMinutes = 15)
        {
            int soon = -1;
            int inTime = 0;
            int late = 1;
            int hourValue = value.Hours;
            int hourTarget = target.Hours;
            int minuteValue = value.Minutes;
            int minuteTarget = target.Minutes;
            if (hourValue < hourTarget)
                return soon;
            else if (hourValue > hourTarget)
                return late;
            else
            {
                if (minuteValue < minuteTarget)
                    return soon;
                else if (minuteValue == minuteTarget)
                    return inTime;
                else
                {
                    if (minuteValue - minuteTarget <= acceptTimeInMinutes)
                    {
                        return inTime;
                    }
                    else
                    {
                        return late;
                    }
                }
            }
        }
    }
}